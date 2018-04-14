using System;
using System.Threading.Tasks;
using XamarinForms_PrismExample.Models;

namespace XamarinForms_PrismExample.DataPersistence
{
    public interface IRepository
    {
        IObservable<TCollection> GetItems<T, TCollection>(string apiUri, string[] queryArgs) where T : EntityBase, new() where TCollection : EntityCollectionBase<T>, new();
        IObservable<T> GetById<T>(string apiUri, long id, string[] queryArgs) where T : EntityBase, new();
        Task<bool> Create<T>(T entity) where T : EntityBase, new();
        Task<bool> Delete<T>(T entity) where T : EntityBase, new();
        Task<bool> Update<T>(T entity) where T : EntityBase, new();
    }
}