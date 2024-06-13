using System.Collections.Generic;

namespace TestMaker.Models
{
    public class Test
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public List<Question> Questions { get; set; }

        public Test()
        {
            Questions = new List<Question>();
        }
    }
}
