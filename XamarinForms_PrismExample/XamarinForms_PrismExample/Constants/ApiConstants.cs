using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForms_PrismExample.Constants
{
    public class ApiConstants
    {
        public const string API_HOST = "api.themoviedb.org/3";

        public const string API_PROTOCOL = "https";

        public const string GetMoviesByReleaseAndLanguage = "discover/movie?primary_release_date.gte={0}&primary_release_date.lte={1}&language={2}";

        public const string ApiKey = "29048abbc8184fde73f24020bf3cfff9";

        public const string spanishCode = "es";

        public const string posterPathBaseUrl = "http://image.tmdb.org/t/p/w185";

    }
}
