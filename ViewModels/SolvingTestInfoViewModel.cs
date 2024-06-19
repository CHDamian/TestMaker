using System.Windows.Input;
using TestMaker.Commands;
using TestMaker.Models;
using TestMaker.Services;

namespace TestMaker.ViewModels
{
    internal class SolvingTestInfoViewModel : BaseViewModel
    {
        private readonly MainViewModel _mainViewModel;
        private Test currentTest;

        public Test CurrentTest
        {
            get => currentTest;
            set { currentTest = value; OnPropertyChanged(nameof(CurrentTest)); }
        }

        public string Name => CurrentTest?.Name;
        public string Category => CurrentTest?.Category;
        public int QuestionsToAnswer => CurrentTest?.QuestionsToAnswer ?? 0;

        public SolvingTestInfoViewModel(MainViewModel mainViewModel, string testName)
        {
            _mainViewModel = mainViewModel;
            GoBackCommand = new RelayCommand(param => GoBack());
            StartTestCommand = new RelayCommand(param => StartTest());
            CheckHistoryCommand = new RelayCommand(param => CheckHistory());

            LoadTest(testName);
        }

        public ICommand GoBackCommand { get; }
        public ICommand StartTestCommand { get; }

        public ICommand CheckHistoryCommand { get; }

        private void LoadTest(string testName)
        {
            var tests = JsonFileService.LoadTests();
            CurrentTest = tests.Find(test => test.Name == testName);
        }

        private void GoBack()
        {
            _mainViewModel.CurrentView = new SolvingTestChooseViewModel(_mainViewModel);
        }

        private void StartTest()
        {
            _mainViewModel.CurrentView = new TestViewModel(_mainViewModel, currentTest, new TestSolution(currentTest), 0);
        }

        private void CheckHistory()
        {
            _mainViewModel.CurrentView = new CheckHistoryViewModel(_mainViewModel, CurrentTest);
        }
    }
}
