using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms_PrismExample.DataPersistence;
using XamarinForms_PrismExample.Models;

namespace XamarinForms_PrismExample.ViewModels
{
    public class DetailPageViewModel : BaseViewModel, INavigationAware
    {
        private INavigationService _navigationService;
        private IRepository<Movie> _movieRepository;
        private IPageDialogService _dialogService;

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
        public DetailPageViewModel(INavigationService navigationService, IRepository<Movie> movieRepository, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _movieRepository = movieRepository;
            _dialogService = dialogService;
        }

        public ICommand SaveRatingCommand => (new Command(
          async () =>
          {
              // _movieRepository.Update(Movie);
              await _movieRepository.Create(Movie); // Inserta o actualiza la pelicula
              await _dialogService.DisplayAlertAsync("Alerta!", "Puntuación guardada :)", "OK");
          }));

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("detail"))
            {
                // Obtenemos y asignamos la Película sobre la que se ha pulsado en la lista de MainPage
                Movie = (Movie)parameters["detail"];
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //
        }
    }
}
