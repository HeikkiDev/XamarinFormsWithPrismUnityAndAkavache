using Prism;
using Prism.Ioc;
using Prism.Unity;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinForms_PrismExample.ViewModels;
using XamarinForms_PrismExample.Views;
using XamarinForms_PrismExample.Services;
using XamarinForms_PrismExample.DataPersistence;
using XamarinForms_PrismExample.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinForms_PrismExample
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("MainPage"); // Al inicializarse la app vamos a MainPage
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            /*
             * NAVEGACIÓN Y VISTAS
             */
            // Registramos el servicio de navegación de Xamarin.Forms
            containerRegistry.RegisterForNavigation<NavigationPage>();
            // Registramos nuestra vista principal (que lista las peliculas de estreno) y su correspondiente ViewModel
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            // Registramos nuestra vista detalle (que detalla info sobre cada peli que seleccionemos en la vista principal) y su correspondiente ViewModel
            containerRegistry.RegisterForNavigation<DetailPage, DetailPageViewModel>();

            /*
             * SERVICIOS
             */
            // Registramos el servicio base genérico de la API Rest que van a usar los demás EntidadService...
            containerRegistry.Register<IApiService, ApiService>();
            // Registramos el servicio de la API Rest para obtener datos sobre Películas de Estreno!
            containerRegistry.Register<IMoviesService, MoviesService>();

            /*
             * PERSISTENCIA DE DATOS
             */
            // Registramos la instancia de SQLiteDatabase
            containerRegistry.RegisterSingleton<ISQLiteDatabase, SQLiteDatabase>();
            // Registramos la instancia de RemoteDatabase
            containerRegistry.RegisterSingleton<IRemoteDatabase, RemoteDatabase>();
            // Registramos el repositorio de Movie
            containerRegistry.RegisterSingleton<IRepository<Movie>, Repository<Movie>>();
            // === Aquí registraríamos los repositorios de los demás modelos... ===

        }
    }
}
