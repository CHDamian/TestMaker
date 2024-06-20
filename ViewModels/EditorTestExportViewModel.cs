using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMaker.Commands;
using TestMaker.Models;
using System.Windows.Forms;
using TestMaker.Services;
using System.Windows;

namespace TestMaker.ViewModels
{
    public class EditorTestExportViewModel : BaseViewModel
    {
        private readonly MainViewModel _mainViewModel;
        private Test currentTest;

        public String exportInfo { get; }

        public String _exportLocation { get; set; }
        public String exportName { get; set; }

        public string exportLocation
        {
            get => _exportLocation;
            set
            {
                if (_exportLocation != value)
                {
                    _exportLocation = value;
                    OnPropertyChanged(nameof(exportLocation));
                }
            }
        }

        public EditorTestExportViewModel(MainViewModel mainViewModel, Test test)
        {
            _mainViewModel = mainViewModel;
            currentTest = test;

            exportInfo = "Eksportuj test: " + currentTest.Name;

            GoBackCommand = new RelayCommand(param => GoBack());
            BrowseFolderCommand = new RelayCommand(param => BrowseFolder());
            ExportTestCommand = new RelayCommand(param => ExportTest());
        }

        public ICommand GoBackCommand { get; }
        public ICommand BrowseFolderCommand { get; }
        public ICommand ExportTestCommand { get; }

        private void GoBack()
        {
            _mainViewModel.CurrentView = new EditorTestListViewModel(_mainViewModel);
        }

        private void BrowseFolder()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    string selectedPath = dialog.SelectedPath;
                    exportLocation = selectedPath; // Display the path in the TextBox
                }
            }
        }

        private void ExportTest()
        {
            currentTest.Solutions = new List<TestResult>();

            try
            {
                ImportExportService.ExportToJson<Test>(_exportLocation, exportName + ".json", currentTest);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            System.Windows.MessageBox.Show("Wyeksportowano test", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            _mainViewModel.CurrentView = new EditorTestListViewModel(_mainViewModel);
        }
    }
}
