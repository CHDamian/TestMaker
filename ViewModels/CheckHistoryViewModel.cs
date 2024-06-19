using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMaker.Commands;
using TestMaker.Models;

namespace TestMaker.ViewModels
{
    public class CheckHistoryViewModel : BaseViewModel
    {
        private readonly MainViewModel _mainViewModel;
        private Test currentTest;

        private ObservableCollection<TestResult> _results;
        public ObservableCollection<TestResult> Results
        {
            get { return _results; }
            set
            {
                _results = value;
                OnPropertyChanged();
            }
        }

        public CheckHistoryViewModel(MainViewModel mainViewModel, Test test)
        {
            _mainViewModel = mainViewModel;
            this.currentTest = test;
            _results= new ObservableCollection<TestResult>(test.Solutions);

            GoBackCommand = new RelayCommand(param => GoBack());
        }

        public ICommand GoBackCommand { get; }

        private void GoBack()
        {
            _mainViewModel.CurrentView = new SolvingTestInfoViewModel(_mainViewModel, currentTest.Name);
        }
    }
}
