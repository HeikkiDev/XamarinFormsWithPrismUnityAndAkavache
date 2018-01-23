using Prism.Navigation;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms_PrismExample.Models;
using XamarinForms_PrismExample.Services;

namespace XamarinForms_PrismExample.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private IMoviesService _moviesService;

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
        public MainPageViewModel(INavigationService navigationService, IMoviesService moviesService)
        {
            _navigationService = navigationService;
            _moviesService = moviesService;

            IsBusy = true;
            Load();
        }

        public async Task Load()
        {
            try
            {
                var moviesCollectionTask = await _moviesService.GetMoviesByReleaseAndLanguage("2018-01-22", "2018-01-15", "es"); //hardcode ahí xd
                MoviesCollection = moviesCollectionTask;
                IsBusy = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICommand GetMoviesCommand => (new Command(
          async () =>
          {
              IsBusy = true;
              await Load();
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
            NavigationParameters par = new NavigationParameters();
            par.Add("detail", movie);

            await _navigationService.NavigateAsync("DetailPage", par);
        }

    }
}
