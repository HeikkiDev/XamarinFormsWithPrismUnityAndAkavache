using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinForms_PrismExample.Models;

namespace XamarinForms_PrismExample.Services
{
    public interface IMoviesService
    {
        Task<MoviesCollection> GetMoviesByReleaseAndLanguage(string dateReleaseMax, string dateReleaseMin, string languageCode);
    }
}
