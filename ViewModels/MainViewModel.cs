using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMaker.Commands;

namespace TestMaker.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object _currentView;

        public MainViewModel()
        {
            // Ustawienie widoku początkowego na MainPageViewModel
            CurrentView = new MainPageViewModel(this);
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
    }
}
