using DailyPoetry.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DailyPoetry.Services
{
    public class PoetryStorage : IPoetryStorage
    {
        private const string DbName = "poetrydb.sqlite3";
        
        //数据库文件路径
        private static readonly string PoetryDbPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DbName);

        public Task<Poetry> GetPoetryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Poetry>> GetPoetryListAsync(Expression<Func<Poetry, bool>> where, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public async Task InitializeAsync()
        {
            using (var dbFileStream = new FileStream(PoetryDbPath, FileMode.Create))
            {
                using (var dbAssertStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(DbName))
                {
                    await dbAssertStream.CopyToAsync(dbFileStream);
                }     
            };
        }

        public bool IsInitialized()
        {
            throw new NotImplementedException();
        }
    }
}
