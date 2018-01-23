using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinForms_PrismExample.Services
{
    public interface IApiService
    {
        Task<T> GetAsync<T>(string apiUri, string[] queryArgs);
    }
}
