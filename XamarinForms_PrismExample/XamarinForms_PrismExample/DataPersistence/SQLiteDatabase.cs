using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinForms_PrismExample.Models;

namespace XamarinForms_PrismExample.DataPersistence
{
    public class SQLiteDatabase : ISQLiteDatabase
    {
        private SQLiteAsyncConnection database;

        public SQLiteDatabase()
        {
            database = Xamarin.Forms.DependencyService.Get<ISQLitePaltformSpecific>().GetConnection();
        }

        /**
         * Otras formas de implementar la depencia de código Platform-specific: Prism maneja automáticamente cualquier dependecia platform-specific de dos formas diferentes:
         * http://prismlibrary.github.io/docs/xamarin-forms/Dependency-Service.html
         *
         * 1 - Accediendo a una instancia en lugar de forma estática a DependencyService
        public SQLiteDatabase(IDependencyService dependencyService)
        {
            database = dependencyService.Get<ISQLitePaltformSpecific>().GetConnection();
        }
        
        2 - Accediendo directamente a una instancia de la interface platform-specific
        public SQLiteDatabase(ISQLitePaltformSpecific sQLitePaltformSpecific)
        {
            database = sQLitePaltformSpecific.GetConnection();
        }
        */

        public async void CreateTable<T>() where T : new()
        {
            try
            {
                var result = await database.CreateTableAsync<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            return database.Table<T>().ToListAsync();
        }

        //public Task<List<T>> GetItemsNotDoneAsync<T>(string tableName) where T : new()
        //{
        //    return database.QueryAsync<T>("SELECT * FROM [" + tableName + "] WHERE [Done] = 0");
        //}

        public Task<T> GetItemAsync<T>(long id) where T : EntityBase, new()
        {
            return database.Table<T>().Where(i => i.id == id).FirstOrDefaultAsync();
        }

        public Task<int> CreateItemAsync<T>(T item) where T : EntityBase, new()
        {
            return database.InsertOrReplaceAsync(item);
        }

        public Task<int> UpdateItemAsync<T>(T item) where T : EntityBase, new()
        {
            return database.UpdateAsync(item);
        }

        public Task<int> DeleteItemAsync<T>(T item) where T : new()
        {
            return database.DeleteAsync(item);
        }
    }
}
