using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms_PrismExample.DataPersistence;
using XamarinForms_PrismExample.Models;
using XamarinForms_PrismExample.Services;

namespace XamarinForms_PrismExample.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private IMoviesService _moviesService;
        private IRepository<Movie> _movieRepository;

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
        public MainPageViewModel(INavigationService navigationService, IMoviesService moviesService, IRepository<Movie> movieRepository)
        {
            _navigationService = navigationService;
            _moviesService = moviesService;
            _movieRepository = movieRepository;

            IsBusy = true;
            Load();
        }

        public async Task Load()
        {
            try
            {
                // En esta request a la API Rest podemos definir excepciones de conexión o lo que sea, y leer las pelis de base de datos local en este caso
                var moviesCollectionTask = await _moviesService.GetMoviesByReleaseAndLanguage("2018-01-22", "2018-01-15", "es");

                // Si la request de pelis ha ido bien, se insertan/actualizan en base de datos local
                List<Task> taskList = new List<Task>();
                foreach (Movie item in moviesCollectionTask.results)
                {
                    var newTask = _movieRepository.Create(item); // Inserta o reemplaza la pelicula en su tabla
                    taskList.Add(newTask);
                }
                await Task.WhenAll(taskList.ToArray()); // Aquí ver cómo sacar el estado después de que las tareas se hayan completado, o cancelado, o lo que sea

                // Tras haber guardado las pelis en local, le damos la colección a la propiedad que usa la vista
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
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("detail", movie);

            await _navigationService.NavigateAsync("DetailPage", parameters);

            SelectedItem = null; // Para deshabilitar la selección del ListView...
        }

    }
}
