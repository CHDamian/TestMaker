using System.Windows.Input;
using TestMaker.Commands;
using TestMaker.Models;

namespace TestMaker.ViewModels
{
    internal class TestViewModel : BaseViewModel
    {
        public Test SelectedTest;
        private readonly MainViewModel _mainViewModel;
        public TestSolution TestSolution { get; set; }

        private int _page;
        public int Page
        {
            get { return _page; }
            set
            {
                if (_page != value)
                {
                    _page = value;
                    OnPropertyChanged(nameof(Page));
                    OnPropertyChanged(nameof(CurrentQuestion));
                }
            }
        }

        public Question CurrentQuestion
        {
            get
            {
                if (TestSolution != null && TestSolution.Questions != null && Page >= 0 && Page < TestSolution.Questions.Count)
                {
                    return TestSolution.Questions[Page];
                }
                return null;
            }
        }


        public TestViewModel(MainViewModel mainViewModel, Test selectedTest, TestSolution solution, int page)
        {
            SelectedTest = selectedTest;
            _mainViewModel = mainViewModel;
            TestSolution = solution;
            Page = page;
            NextQuestionCommand = new RelayCommand(param => NextQuestion());
            PreviousQuestionCommand = new RelayCommand(param => PreviousQuestion());
            StopTestCommand = new RelayCommand(param => StopTest());
            FinishTestCommand = new RelayCommand(param => FinishTest());
            CatchAnswerCommand = new RelayCommand(param => CatchAnswer());
        }

        public ICommand NextQuestionCommand { get; }
        public ICommand PreviousQuestionCommand { get; }
        public ICommand StopTestCommand { get; }
        public ICommand FinishTestCommand { get; }
        public ICommand CatchAnswerCommand { get; }



        public void NextQuestion()
        {
            if (Page < TestSolution.Questions.Count - 1)
            {
                Page++;
            }
        }

        public void PreviousQuestion()
        {

        }

        public void StopTest()
        {

        }

        public void FinishTest()
        {

        }

        public void CatchAnswer()
        {

        }
    }
}