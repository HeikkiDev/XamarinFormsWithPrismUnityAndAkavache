using Prism.AppModel;
using Prism.Navigation;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms_PrismExample.DataPersistence;
using XamarinForms_PrismExample.Models;

namespace XamarinForms_PrismExample.ViewModels
{
    public class MainPageViewModel : BaseViewModel, INavigationAware, IApplicationLifecycleAware
    {
        private INavigationService _navigationService;
        private IRepository _repository;

        private Movie _selectedItem;
        public Movie SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private MoviesCollection _moviesCollection;
        public MoviesCollection MoviesCollection
        {
            get { return _moviesCollection; }
            set
            {
                _moviesCollection = value;
                OnPropertyChanged();
            }
        }

        // Constructor
        public MainPageViewModel(INavigationService navigationService, IRepository repository)
        {
            _navigationService = navigationService;
            _repository = repository;
        }

        public void Load()
        {
            try
            {
                string apiUri = Constants.ApiConstants.GetMoviesByReleaseAndLanguage;
                string[] queryArgs = { "2018-01-15", "2018-01-22", Constants.ApiConstants.spanishCode }; // dateReleaseMin, dateReleaseMax, languageCode

                // Este .Subscribe se ejecutará dos veces, la primera con la info cacheada, y la segunda con la info que se traiga de la llamada a la API Rest
                _repository.GetItems<Movie, MoviesCollection>(apiUri, queryArgs).Subscribe(
                    cachedThenUpdatedMoviesCollection => {

                        MoviesCollection = cachedThenUpdatedMoviesCollection;
                        if (IsBusy) IsBusy = false;

                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICommand GetMoviesCommand => (new Command(
          () =>
          {
              IsBusy = true;
              Load();
          }));


        private Command itemTappedCommand;

        public Command ItemTappedCommand
        {
            get
            {
                return itemTappedCommand ?? (itemTappedCommand = new Command<Movie>(ExecuteItemTappedCommand));
            }
        }

        private async void ExecuteItemTappedCommand(Movie movie)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("detail", movie);

            await _navigationService.NavigateAsync("DetailPage", parameters);
            // Otra opción sería:
            // await _navigationService.NavigateAsync("DetailPage?movieId=movie.id");
            // Y en la página DetailPage obtendríamos el ID y cogeríamos la Película del Repositorio...

            SelectedItem = null; // Para deshabilitar la selección del ListView...
        }

        #region Prism INavigationAware
        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            // Avoid Load in back navegation fromn Detail to MainPage
            if(parameters.GetNavigationMode() == NavigationMode.New)
            {
                IsBusy = true;
                Load();
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //
        }
        #endregion

        #region IApplicationLifecycleAware
        public void OnResume()
        {
            // Manage On Resume here!
        }

        public void OnSleep()
        {
            // Manage On Sleep here!
        }
        #endregion
    }
}
