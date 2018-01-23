using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinForms_PrismExample.Models;

namespace XamarinForms_PrismExample.ViewModels
{
    public class DetailPageViewModel : BaseViewModel, INavigationAware
    {
        private INavigationService _navigationService;

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
        public DetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("detail"))
            {
                Movie = (Movie)parameters["detail"];
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //
        }
    }
}
