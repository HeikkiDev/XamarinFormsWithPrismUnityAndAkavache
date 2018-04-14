using System.Collections.Generic;
using XamarinForms_PrismExample.Constants;

namespace XamarinForms_PrismExample.Models
{
    public class Movie : EntityBase
    {
        public int vote_count { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public string title { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public List<int> genre_ids { get; set; } // Ignoramos la lista de géneros, porque SQLite no puede meter un tipo List<>. Habría que crear una tabla para ello relacionada con la película
        public string backdrop_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }

        // Propiedad para leer la URL completa del poster de cada Película
        public string ImageURL
        {
            get { return ApiConstants.posterPathBaseUrl + poster_path; }
        }

        // Propiedad para que el usuario puntúe la película
        public int Rating { get; set; }
    }
}
