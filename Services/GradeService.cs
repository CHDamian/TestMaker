using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Models;

namespace TestMaker.Services
{
    public static class GradeService
    {
        public static int Grade(Test test, TestSolution solution)
        {
            int points = 0;
            foreach(Question question in solution.Questions)
            {
                bool isOK = true;
                int queNum = question.Number;
                foreach(Answer answer in question.Answers)
                {
                    int ansNum = answer.Number;
                    if(answer.IsCorrect != test.Questions[queNum].Answers[ansNum].IsCorrect)
                    {
                        isOK = false;
                        break;
                    }
                }
                if(isOK) 
                {
                    points++;
                }
            }
            return points;
        }
    }
}
