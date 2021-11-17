using DailyPoetry.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DailyPoetry.Services
{
    public class PoetryStorage : IPoetryStorage
    {
        private const string DbName = "poetrydb.sqlite3";

        private SQLiteAsyncConnection _connection;

        public SQLiteAsyncConnection Connection => _connection ?? (_connection = new SQLiteAsyncConnection(PoetryDbPath));

        //数据库文件路径
        private static readonly string PoetryDbPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DbName);

        public async Task<Poetry> GetPoetryAsync(int id) =>
            await _connection.Table<Poetry>().FirstOrDefaultAsync(p => p.Id == id);
        

        public async Task<IList<Poetry>> GetPoetryListAsync(Expression<Func<Poetry, bool>> where, int skip, int take)=>
            await _connection.Table<Poetry>().Where(where).Skip(skip).Take(take).ToListAsync();

        public async Task InitializeAsync()
        {
            using (var dbFileStream = new FileStream(PoetryDbPath, FileMode.Create))
            {
                using (var dbAssertStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(DbName))
                {
                    await dbAssertStream.CopyToAsync(dbFileStream);
                }
            };

            Preferences.Set(PoetryStorageConstants.VersionKey, PoetryStorageConstants.Version);
        }

  

        public bool IsInitialized() => 
            Preferences.Get(PoetryStorageConstants.VersionKey, -1) == PoetryStorageConstants.Version;

    }

    public static class PoetryStorageConstants
    {

        public const int Version = 1;
        public const string VersionKey = nameof(PoetryStorageConstants) + "." + nameof(Version);

    }

}
