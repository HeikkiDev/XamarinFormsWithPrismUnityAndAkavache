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
using Akavache;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinForms_PrismExample
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            // Make sure you set the Akavache application name before doing any inserts or gets
            BlobCache.ApplicationName = "AkavacheMoviesApp";

            // Al inicializarse la app vamos a MainPage (que estará dentro de una NavigationPage)
            // Al añadir delante de MainPage NavigationPage, todas las páginas a la que naveguemos a partir de aquí estarán
            // también dentro de una NavigationPage sin necesidad de ponerlo explicitamente en el método NavigateAsync
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            /*
             * NAVEGACIÓN Y VISTAS
             */
             // Registramos NavigationPage de XamarinForms, que vamos a usar para embeber dentro nuestras Páginas para habilitarles así el control de navegación atrás
            containerRegistry.RegisterForNavigation<NavigationPage>();
            // Registramos el servicio de navegación de Xamarin.Forms
            containerRegistry.RegisterForNavigation<NavigationPage>();
            // Registramos nuestra vista principal (que lista las peliculas de estreno) y su correspondiente ViewModel
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            // Registramos nuestra vista detalle (que detalla info sobre cada peli que seleccionemos en la vista principal) y su correspondiente ViewModel
            containerRegistry.RegisterForNavigation<DetailPage, DetailPageViewModel>();

            /*
             * SERVICIOS
             */
            // Registramos el servicio base genérico de la API Rest que va a realizar las request
            containerRegistry.Register<IApiService, ApiService>();
            // Registramos el servicio de acceso a file system especifico de cada plataforma, gracias al plugin PCLStorage
            containerRegistry.Register<IFileSystemService, FileSystemService>();
            // Registramos el servicio de geolocalización especifico de cada plataforma, gracias al plugin de Xamarin GeoLocator
            containerRegistry.Register<IGeoLocationService, GeoLocationService>();

            /*
             * PERSISTENCIA DE DATOS
             */
            // Registramos la instancia de RemoteDatabase
            containerRegistry.RegisterSingleton<IRemoteDatabase, RemoteDatabase>();
            // Registramos el repositorio genérico para los Models
            containerRegistry.RegisterSingleton<IRepository, Repository>();

        }
    }
}
