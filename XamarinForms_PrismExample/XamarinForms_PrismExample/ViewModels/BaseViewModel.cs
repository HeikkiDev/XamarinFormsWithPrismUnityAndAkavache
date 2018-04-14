using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace XamarinForms_PrismExample.ViewModels
{
    // Otra opción es heredar de BindableBase, que nos lo da Prism, y en los set de las propiedades en lugar de llamar a OnPropertyChanged() llamaríamos a SetProperty(ref _title, value)
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
