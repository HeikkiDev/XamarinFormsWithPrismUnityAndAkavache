using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinForms_PrismExample.Models;

namespace XamarinForms_PrismExample.DataPersistence
{
    public interface IRemoteDatabase
    {
        Task<T> GetItemAsync<T>(string apiUri, long id, string[] queryArgs) where T : EntityBase, new();
        Task<T> GetItemsAsync<T>(string apiUri, string[] queryArgs) where T : new();
        Task<int> CreateItemAsync<T>(T item) where T : EntityBase, new();
        Task<int> DeleteItemAsync<T>(T item) where T : EntityBase, new();
        Task<int> UpdateItemAsync<T>(T item) where T : EntityBase, new();
    }
}
