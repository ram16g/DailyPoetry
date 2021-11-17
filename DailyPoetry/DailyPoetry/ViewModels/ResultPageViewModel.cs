using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using DailyPoetry.Models;
using DailyPoetry.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Xamarin.Forms.Extended;

namespace DailyPoetry.ViewModels
{
    public class ResultPageViewModel:ObservableObject
    {
       
        private const string Loading = "正在载入...";
        private const string NoResult = "没有满足条件的结果";
        private const string NoMoreResult = "没有更多结果";

        private const int PageSize = 20;

        private string _status;
        public  string Status {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        private bool _isNewQuery;
        private bool _canLoadMore;

        private RelayCommand _pageApperingCommand;
        public RelayCommand PageApperingCommand =>
            _pageApperingCommand ?? (_pageApperingCommand = new RelayCommand(async () =>
            {
                if (!_isNewQuery)
                {
                    return;
                }
                _isNewQuery = false;

                PoetryCollection.Clear();
                _canLoadMore = true;
                await PoetryCollection.LoadMoreAsync();

            }));

        private Expression<Func<Poetry,bool>> _where;
        public Expression<Func<Poetry, bool>> Where
        {
            get => _where;
            set => SetProperty(ref _where, value);
        }

        
        public ResultPageViewModel(IPoetryStorage poetryStorage)
        {
            PoetryCollection = new InfiniteScrollCollection<Poetry>
            {
                OnCanLoadMore = () => _canLoadMore,
                OnLoadMore = async ()=>
                {
                    Status = Loading;
                    var poetres = await poetryStorage.GetPoetryListAsync(_where, PoetryCollection.Count,PageSize);
                    if (poetres.Count < PageSize)
                    {
                        _canLoadMore = false;
                        Status = NoMoreResult;
                    }

                    if(poetres.Count== 0 && PoetryCollection.Count == 0)
                    {
                        Status = NoResult;
                    }
                    return poetres;
                }

            };
        }

        public InfiniteScrollCollection<Poetry> PoetryCollection { get; }
    }
}
