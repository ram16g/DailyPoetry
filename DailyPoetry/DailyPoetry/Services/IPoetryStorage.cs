using DailyPoetry.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DailyPoetry.Services
{
    /// <summary>
    /// 诗词存储接口
    /// </summary>
    public interface IPoetryStorage
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();
        
        /// <summary>
        /// 判断是否初始化
        /// </summary>
        bool IsInitialized();

        /// <summary>
        /// 获取一首诗词
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Poetry> GetPoetryAsync(int id);

        /// <summary>
        /// 获取满足搜索条件的诗词集合
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <param name="skip">跳过数量</param>
        /// <param name="take">获取数量</param>
        /// <returns></returns>
        Task<IList<Poetry>> GetPoetryListAsync(Expression<Func<Poetry, bool>> where,int skip,int take);
    }
}
