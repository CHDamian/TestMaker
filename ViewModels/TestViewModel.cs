namespace TestMaker.ViewModels
{
    internal class TestViewModel
    {
        private Test selectedTest;

        public TestViewModel(Test selectedTest)
        {
            this.selectedTest = selectedTest;
        }
    }
}