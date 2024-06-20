using System;
using System.Collections.Generic;
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
    public class EditorTestImportViewModel : BaseViewModel
    {
        private readonly MainViewModel _mainViewModel;

        public String _importLocation { get; set; }

        public string importLocation
        {
            get => _importLocation;
            set
            {
                if (_importLocation != value)
                {
                    _importLocation = value;
                    OnPropertyChanged(nameof(importLocation));
                }
            }
        }

        public EditorTestImportViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            GoBackCommand = new RelayCommand(param => GoBack());
            BrowseJsonCommand = new RelayCommand(param => BrowseJson());
            ImportTestCommand = new RelayCommand(param => ImportTest());
        }

        public ICommand GoBackCommand { get; }
        public ICommand BrowseJsonCommand { get; }
        public ICommand ImportTestCommand { get; }

        private void GoBack()
        {
            _mainViewModel.CurrentView = new EditorTestListViewModel(_mainViewModel);
        }

        private void BrowseJson()
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".json"; // Default file extension
            dlg.Filter = "Json file (.json)|*.json"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                importLocation = dlg.FileName;
            }
        }

        public void ImportTest()
        {
            Test newTest;
            try
            {
                newTest = ImportExportService.ImportFromJson<Test>(_importLocation);
            }
            catch(Exception ex) 
            {
                System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            List<Test> testList = JsonFileService.LoadTests();
            testList.Add(newTest);
            JsonFileService.SaveTests(testList);

            _mainViewModel.CurrentView = new EditorTestListViewModel(_mainViewModel);
        }
    }
}
