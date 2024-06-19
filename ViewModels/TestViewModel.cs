using System;
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

        public int Page;

        public int QuestionNum;

        public Boolean PreviousIsEnabled { get; set; }
        public Boolean NextIsEnabled { get; set; }

        public QuestionForm question { get; set; }


        public TestViewModel(MainViewModel mainViewModel, Test selectedTest, TestSolution solution, int page)
        {
            SelectedTest = selectedTest;
            _mainViewModel = mainViewModel;
            TestSolution = solution;
            Page = page;
            QuestionNum = Page + 1;
            NextQuestionCommand = new RelayCommand(param => NextQuestion());
            PreviousQuestionCommand = new RelayCommand(param => PreviousQuestion());
            StopTestCommand = new RelayCommand(param => StopTest());
            FinishTestCommand = new RelayCommand(param => FinishTest());

            question = new QuestionForm
            {
                Header = "Pytanie " + QuestionNum,
                Content = solution.Questions[Page].Content,
            };

            foreach(Answer answer in solution.Questions[Page].Answers)
            {
                question.Answers.Add(answer);
            }

            PreviousIsEnabled = Page != 0;
            NextIsEnabled = Page != (solution.Questions.Count - 1);
        }

        public ICommand NextQuestionCommand { get; }
        public ICommand PreviousQuestionCommand { get; }
        public ICommand StopTestCommand { get; }
        public ICommand FinishTestCommand { get; }

        public void NextQuestion()
        {
            if (Page < TestSolution.Questions.Count - 1)
            {
                _mainViewModel.CurrentView = new TestViewModel(_mainViewModel, SelectedTest, TestSolution, Page + 1);
            }
        }

        public void PreviousQuestion()
        {
            if (Page > 0)
            {
                _mainViewModel.CurrentView = new TestViewModel(_mainViewModel, SelectedTest, TestSolution, Page - 1);
            }
        }

        public void StopTest()
        {
            _mainViewModel.CurrentView = new SolvingTestInfoViewModel(_mainViewModel, SelectedTest.Name);
        }

        public void FinishTest()
        {
            _mainViewModel.CurrentView = new TestResultViewModel(_mainViewModel, SelectedTest, TestSolution);
        }
    }
}