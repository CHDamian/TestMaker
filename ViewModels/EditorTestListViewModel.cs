using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TestMaker.Commands;
using TestMaker.Models;
using TestMaker.Services;

namespace TestMaker.ViewModels
{
    public class EditorTestListViewModel : BaseViewModel
    {
        private readonly MainViewModel _mainViewModel;

        public EditorTestListViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            GoBackCommand = new RelayCommand(param => GoBack());
            AddTestCommand = new RelayCommand(param => AddTest());
            RemoveTestCommand = new RelayCommand(param => RemoveTest(param as TestItem));
            EditTestCommand = new RelayCommand(param => EditTest(param as TestItem));
            ExportTestCommand = new RelayCommand(param => ExportTest(param as TestItem));

            // Wczytaj testy z pliku JSON i grupuj je według kategorii
            LoadAndGroupTests();
        }

        // Komenda do powrotu
        public ICommand GoBackCommand { get; }

        // Komenda do dodawania testu
        public ICommand AddTestCommand { get; }

        // Komenda do usuwania testu
        public ICommand RemoveTestCommand { get; }

        // Komenda do edytowania testu
        public ICommand EditTestCommand { get; }

        // Komenda do exportu testu
        public ICommand ExportTestCommand { get; }

        // Kolekcja kategorii do wyświetlenia w widoku
        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        // Metoda do powrotu
        private void GoBack()
        {
            _mainViewModel.CurrentView = new MainPageViewModel(_mainViewModel);
        }

        // Metoda do dodawania testu
        private void AddTest()
        {
            _mainViewModel.CurrentView = new EditorTestInitViewModel(_mainViewModel);
        }

        // Metoda do wczytywania i grupowania testów z pliku JSON
        private void LoadAndGroupTests()
        {
            try
            {
                var tests = JsonFileService.LoadTests();
                var groupedTests = tests
                    .GroupBy(t => t.Category)
                    .Select(g => new Category
                    {
                        Name = g.Key,
                        Tests = new ObservableCollection<TestItem>(g.Select(t => new TestItem { Name = t.Name, Test = t }))
                    });

                Categories = new ObservableCollection<Category>(groupedTests);
            }
            catch
            {
                MessageBox.Show("Nie udało się wczytać testów!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Metoda do usuwania testu
        private void RemoveTest(TestItem testItem)
        {
            try
            {
                // Usunięcie testu z pliku JSON
                JsonFileService.RemoveTest(testItem.Test);

                // Usunięcie testu z listy
                foreach (var category in Categories)
                {
                    var testToRemove = category.Tests.FirstOrDefault(t => t.Name == testItem.Name);
                    if (testToRemove != null)
                    {
                        category.Tests.Remove(testToRemove);

                        // Jeśli kategoria jest teraz pusta, usuń ją z listy
                        if (category.Tests.Count == 0)
                        {
                            Categories.Remove(category);
                        }

                        break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nie udało się usunąć testu!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditTest(TestItem testItem)
        {
            if (testItem != null)
            {
                _mainViewModel.CurrentView = new EditorTestQuestionsViewModel(_mainViewModel, testItem.Test);
            }
        }

        private void ExportTest(TestItem testItem)
        {
            if (testItem != null)
            {
                _mainViewModel.CurrentView = new EditorTestExportViewModel(_mainViewModel, testItem.Test);
            }
        }
    }
}
