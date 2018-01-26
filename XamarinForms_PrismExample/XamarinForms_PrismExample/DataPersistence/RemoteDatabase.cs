using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinForms_PrismExample.Models;

namespace XamarinForms_PrismExample.DataPersistence
{
    public class RemoteDatabase : IRemoteDatabase
    {
        public RemoteDatabase()
        {
            //
        }

        public Task<int> CreateItemAsync<T>(T item) where T : EntityBase, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteItemAsync<T>(T item) where T : new()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetItemAsync<T>(long id) where T : EntityBase, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateItemAsync<T>(T item) where T : EntityBase, new()
        {
            throw new NotImplementedException();
        }
    }
}
