using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testapp.Models;
using Testapp.Services;
using Xamarin.Forms;

namespace Testapp.Data
{
    public class FFDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public FFDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Module).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Module)).ConfigureAwait(false);
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Question).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Question)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        #region Module
        public Task<List<Module>> GetModulesAsync()
        {
            return Database.Table<Module>().ToListAsync();
        }

        public Task<Module> GetModuleAsync(int id)
        {
            return Database.Table<Module>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveModuleAsync(Module item)
        {
            return Database.InsertAsync(item);
        }
        #endregion

        #region Questions
        public Task<List<Question>> GetQuestionsAsync()
        {
            return Database.Table<Question>().ToListAsync();
        }

        public Task<Question> GetQuestionAsync(int id)
        {
            return Database.Table<Question>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Question>> GetQuestionsByModulAsync(int module_id)
        {
            return Database.Table<Question>().Where(i => i.Module_id == module_id).ToListAsync();
        }

        public Task<int> SaveQuestionAsync(Question item)
        {
            return Database.InsertAsync(item);
        }
        #endregion
    }
}
