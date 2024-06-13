using System.Windows;
using System.Windows.Input;
using TestMaker.Commands;
using TestMaker.Models;
using TestMaker.Services;

namespace TestMaker.ViewModels
{
    public class EditorTestInitViewModel : BaseViewModel
    {
        private readonly MainViewModel _mainViewModel;

        public EditorTestInitViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            GoBackCommand = new RelayCommand(param => GoBack());
            AddTestCommand = new RelayCommand(param => AddTest());

            Test = new Test();
        }

        // Komenda do powrotu
        public ICommand GoBackCommand { get; }

        // Komenda do dodawania testu
        public ICommand AddTestCommand { get; }

        // Właściwość dla obiektu Test
        private Test _test;
        public Test Test
        {
            get { return _test; }
            set
            {
                _test = value;
                OnPropertyChanged();
            }
        }

        // Metoda do powrotu
        private void GoBack()
        {
            _mainViewModel.CurrentView = new EditorTestListViewModel(_mainViewModel);
        }

        // Metoda do dodawania testu
        private void AddTest()
        {
            if (string.IsNullOrWhiteSpace(Test.Name) || string.IsNullOrWhiteSpace(Test.Category))
            {
                MessageBox.Show("Proszę wypełnić wszystkie pola!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var tests = JsonFileService.LoadTests();
                tests.Add(Test);
                JsonFileService.SaveTests(tests);
                _mainViewModel.CurrentView = new EditorTestListViewModel(_mainViewModel);
            }
            catch
            {
                MessageBox.Show("Nie udało się zapisać testu! Spróbuj później!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
