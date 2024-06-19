using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.Models
{
    public class TestResult
    {
        public int Points { get; set; }

        public int Total { get; set; }

        public DateTime date { get; set; }

        public string dateStr { get; set; }
    }
}
