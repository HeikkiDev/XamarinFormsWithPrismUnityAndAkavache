using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinForms_PrismExample.Models;

namespace XamarinForms_PrismExample.Services
{
    public class MoviesService : IMoviesService
    {
        private IApiService _apiService;

        public MoviesService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<MoviesCollection> GetMoviesByReleaseAndLanguage(string dateReleaseMin, string dateReleaseMax, string languageCode)
        {
            string apiUri = Constants.ApiConstants.GetMoviesByReleaseAndLanguage;
            string[] queryArgs = { dateReleaseMax, dateReleaseMin, languageCode };
            try
            {
                return await _apiService.GetAsync<MoviesCollection>(apiUri, queryArgs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
