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
    class MainPageViewModel : BaseViewModel
    {
        private readonly MainViewModel _mainViewModel;

        public MainPageViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            SwitchToEditorTestListCommand = new RelayCommand(param => SwitchToEditorTestList());
        }

        public ICommand SwitchToEditorTestListCommand { get; }

        private void SwitchToEditorTestList()
        {
            _mainViewModel.CurrentView = new EditorTestListViewModel(_mainViewModel);
        }
    }
}
