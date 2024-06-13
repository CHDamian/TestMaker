using System.Collections.ObjectModel;

namespace TestMaker.Models
{
    public class Category
    {
        public string Name { get; set; }
        public ObservableCollection<TestItem> Tests { get; set; }

        public Category()
        {
            Tests = new ObservableCollection<TestItem>();
        }
    }

    public class TestItem
    {
        public string Name { get; set; }
        public Test Test { get; set; }
    }
}
