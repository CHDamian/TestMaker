using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestMaker.Commands;
using TestMaker.Models;
using TestMaker.Services;

namespace TestMaker.ViewModels
{
    class SolvingTestChooseViewModel : BaseViewModel
    {
        private readonly MainViewModel _mainViewModel;

        public SolvingTestChooseViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            GoBackCommand = new RelayCommand(param => GoBack());
            ChooseTestCommand = new RelayCommand(param => ChooseTest(param as TestItem));
            LoadAndGroupTests();
        }

        public ICommand GoBackCommand { get; }

        public ICommand ChooseTestCommand { get; }

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

        private void GoBack()
        {
            _mainViewModel.CurrentView = new MainPageViewModel(_mainViewModel);
        }

        private void ChooseTest(TestItem test) 
        {
            _mainViewModel.CurrentView = new SolvingTestInfoViewModel(_mainViewModel, test.Test.Name);
        }

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

    }
}
