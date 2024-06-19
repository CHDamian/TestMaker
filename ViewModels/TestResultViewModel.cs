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
            TestSolution.date = DateTime.Now;
            TestSolution.dateStr = String.Format("{0:g}", TestSolution.date);

            SelectedTest.Solutions.Add(new TestResult
            {
                Points= TestSolution.Points,
                Total= TestSolution.Total,
                date= DateTime.Now,
                dateStr= TestSolution.dateStr
            });
            JsonFileService.UpdateTest(SelectedTest);

            GoBackCommand = new RelayCommand(param => GoBack());
            
        }

        public ICommand GoBackCommand { get; set; }

        public void GoBack()
        {
            _mainViewModel.CurrentView = new SolvingTestChooseViewModel(_mainViewModel);
        }
    }
}
