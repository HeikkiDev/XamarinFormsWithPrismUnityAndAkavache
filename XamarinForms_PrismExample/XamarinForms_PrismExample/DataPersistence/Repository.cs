using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinForms_PrismExample.Models;

namespace XamarinForms_PrismExample.DataPersistence
{
    // Si queremos podemos tener una clase Repository por Entidad, que implemente IRepository como en esta
    public class Repository<T> : IRepository<T> where T : EntityBase, new()
    {
        private ISQLiteDatabase _sqliteDatabase;
        private IRemoteDatabase _remoteDatabase; // Falta implementar todo en RemoteDatabase... sería enviando y recibiendo los cambios a través de API Rest

        public Repository(ISQLiteDatabase sqliteDatabase, IRemoteDatabase remoteDatabase)
        {
            _sqliteDatabase = sqliteDatabase;
            _remoteDatabase = remoteDatabase;

            try
            {
                _sqliteDatabase.CreateTable<T>(); // Crea la tabla para la Entidad correspondiente
            }
            catch (Exception e)
            {
                // En vez de esto quizá sería mejor comprobar si existe la tabla antes...
            }
        }

        public async Task<List<T>> GetItems()
        {
            return await _sqliteDatabase.GetItemsAsync<T>();
        }

        public async Task<T> GetById(long id)
        {
            return await _sqliteDatabase.GetItemAsync<T>(id);
        }

        public Task<int> Create(T entity)
        {
            return _sqliteDatabase.CreateItemAsync<T>(entity);
        }

        public async void Delete(T entity)
        {
            await _sqliteDatabase.DeleteItemAsync<T>(entity);
        }

        public Task<int> Update(T entity)
        {
            return _sqliteDatabase.UpdateItemAsync<T>(entity);
        }
    }
}
