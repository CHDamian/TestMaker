using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestMaker.Commands;
using TestMaker.Models;
using TestMaker.Services;

namespace TestMaker.ViewModels
{
    internal class TestResultViewModel : BaseViewModel
    {
        private readonly MainViewModel _mainViewModel;
        public Test SelectedTest { get; set; }
        public TestSolution TestSolution { get; set; }
        public int Points {  get; set; }

        public String PointsLabel { get; set; }


        public TestResultViewModel(MainViewModel mainViewModel,Test test, TestSolution testSolution)
        {
            _mainViewModel = mainViewModel;
            SelectedTest = test;
            TestSolution = testSolution;
            TestSolution.Points = GradeService.Grade(SelectedTest, TestSolution);
            Points = TestSolution.Points;
            PointsLabel = "" + Points + "/" + TestSolution.Total;
            GoBackCommand = new RelayCommand(param => GoBack());
            
        }

        public ICommand GoBackCommand { get; set; }

        public void GoBack()
        {
            _mainViewModel.CurrentView = new SolvingTestChooseViewModel(_mainViewModel);
        }
    }
}
