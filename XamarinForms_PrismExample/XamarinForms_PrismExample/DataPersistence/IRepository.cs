using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinForms_PrismExample.Models;

namespace XamarinForms_PrismExample.DataPersistence
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<List<T>> GetItems();
        Task<T> GetById(long id);
        Task<int> Create(T entity);
        void Delete(T entity);
        Task<int> Update(T entity);
    }
}
