using Prism.Navigation;
using Prism.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms_PrismExample.DataPersistence;
using XamarinForms_PrismExample.Models;
using XamarinForms_PrismExample.Services;

namespace XamarinForms_PrismExample.ViewModels
{
    public class DetailPageViewModel : BaseViewModel, INavigationAware
    {
        private INavigationService _navigationService;
        private IRepository _repository;
        private IPageDialogService _dialogService;
        private IGeoLocationService _geoLocationService;

        private Movie _movie;
        public Movie Movie
        {
            get { return _movie; }
            set
            {
                _movie = value;
                OnPropertyChanged();
            }
        }

        // Constructor
        public DetailPageViewModel(INavigationService navigationService, IRepository repository, IPageDialogService dialogService, IGeoLocationService geoLocationService)
        {
            _navigationService = navigationService;
            _repository = repository;
            _dialogService = dialogService;
            _geoLocationService = geoLocationService;
        }

        public ICommand SaveRatingCommand => (new Command(
          async () =>
          {
              await _repository.Create(Movie); // Inserta o actualiza la pelicula

              try
              {
                  // Vamos a leer aquí las posición con geolocalización porque sí, y la mostramos en el dialog!
                  if (_geoLocationService.GeolocatorIsSupported())
                  {
                      Plugin.Geolocator.Abstractions.Position position = await _geoLocationService.GetCurrentPositionAsync(); // TODO: AQUÍ SE QUEDA COLGADO, REVISAR Y ARREGLAR
                      await _dialogService.DisplayAlertAsync("Alerta!", "Puntuación guardada :)\nY posición: {" + position.Latitude + ", " + position.Longitude + "}", "OK");
                  }
                  else
                  {
                      await _dialogService.DisplayAlertAsync("Alerta!", "Puntuación guardada :)", "OK");
                  }
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              
              
          }));

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.New && parameters.ContainsKey("detail"))
            {
                /*
                 * Esto sería lo normal, pero si por ejemplo el elemento que va a mostrarse en detalle no trae todo el detalle que necesitamos habrá que traerlo del servidor
                 */
                // Obtenemos y asignamos la Película sobre la que se ha pulsado en la lista de MainPage
                //Movie = (Movie)parameters["detail"];

                Movie movieSelected = (Movie)parameters["detail"];

                // Este .Subscribe se ejecutará dos veces, la primera con la info cacheada, y la segunda con la info que se traiga de la llamada a la API Rest
                // Aquí le he puesto inglés para ver cómo cambian los datos en la vista. Debería ser: Constants.ApiConstants.spanishCode
                _repository.GetById<Movie>(Constants.ApiConstants.GetMovieByID, movieSelected.id, new string[] { "en" }).Subscribe(
                    cachedThenUpdatedMovie => {

                        Movie = cachedThenUpdatedMovie;

                    });
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //
        }
    }
}
