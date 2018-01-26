using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinForms_PrismExample.Models;

namespace XamarinForms_PrismExample.DataPersistence
{
    public interface IRemoteDatabase
    {
        Task<T> GetItemAsync<T>(long id) where T : EntityBase, new();
        Task<List<T>> GetItemsAsync<T>() where T : new();
        Task<int> CreateItemAsync<T>(T item) where T : EntityBase, new();
        Task<int> DeleteItemAsync<T>(T item) where T : new();
        Task<int> UpdateItemAsync<T>(T item) where T : EntityBase, new();
    }
}
