using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
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

        public ICollectionView FilteredResults { get; private set; }
        public ObservableCollection<string> FilterOptions { get; }
        private string _selectedFilterOption;
        public string SelectedFilterOption
        {
            get { return _selectedFilterOption; }
            set
            {
                _selectedFilterOption = value;
                OnPropertyChanged();
                ApplySort();
            }
        }

        public ICommand ResetFilterCommand { get; }

        public CheckHistoryViewModel(MainViewModel mainViewModel, Test test)
        {
            _mainViewModel = mainViewModel;
            this.currentTest = test;
            _results = new ObservableCollection<TestResult>(test.Solutions);

            FilteredResults = CollectionViewSource.GetDefaultView(_results);

            FilterOptions = new ObservableCollection<string> { "Data", "Punkty", "Suma" };
            SelectedFilterOption = FilterOptions.FirstOrDefault();

            ResetFilterCommand = new RelayCommand(param => ResetFilter());

            GoBackCommand = new RelayCommand(param => GoBack());
        }

        public ICommand GoBackCommand { get; }

        private void GoBack()
        {
            _mainViewModel.CurrentView = new SolvingTestInfoViewModel(_mainViewModel, currentTest.Name);
        }

        private void ApplySort()
        {
            FilteredResults.SortDescriptions.Clear();

            switch (SelectedFilterOption)
            {
                case "Data":
                    FilteredResults.SortDescriptions.Add(new SortDescription("dateStr", ListSortDirection.Ascending));
                    break;
                case "Punkty":
                    FilteredResults.SortDescriptions.Add(new SortDescription("Points", ListSortDirection.Ascending));
                    break;
                case "Suma":
                    FilteredResults.SortDescriptions.Add(new SortDescription("Total", ListSortDirection.Ascending));
                    break;
            }
        }

        private void ResetFilter()
        {
            SelectedFilterOption = FilterOptions.FirstOrDefault();
            ApplySort();
        }
    }
}
