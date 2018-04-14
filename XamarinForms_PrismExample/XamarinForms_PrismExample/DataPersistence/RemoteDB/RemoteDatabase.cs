using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinForms_PrismExample.Models;
using XamarinForms_PrismExample.Services;

namespace XamarinForms_PrismExample.DataPersistence
{
    public class RemoteDatabase : IRemoteDatabase
    {
        private IApiService _apiService;

        public RemoteDatabase(IApiService apiService)
        {
            _apiService = apiService;
        }

        public Task<T> GetItemAsync<T>(string apiUri, long id, string[] queryArgs) where T : EntityBase, new()
        {
            List<string> argsList = queryArgs.OfType<string>().ToList();
            argsList.Insert(0, id.ToString()); // El ID irá siempre en primer lugar, antes del resto de parámetros
            return _apiService.GetAsync<T>(apiUri, argsList.ToArray());
        }

        public Task<T> GetItemsAsync<T>(string apiUri, string[] queryArgs) where T : new()
        {
            return _apiService.GetAsync<T>(apiUri, queryArgs);
        }

        public Task<int> CreateItemAsync<T>(T item) where T : EntityBase, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteItemAsync<T>(T item) where T : EntityBase, new()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateItemAsync<T>(T item) where T : EntityBase, new()
        {
            throw new NotImplementedException();
        }
    }
}
