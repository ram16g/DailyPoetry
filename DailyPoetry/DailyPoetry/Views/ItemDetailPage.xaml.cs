using DailyPoetry.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace DailyPoetry.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}