﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMaker.Services;

namespace TestMaker.Models
{
    public class TestSolution
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public List<Question> Questions { get; set; }
        public int Points { get; set; }

        public int Total { get; set; }

        public DateTime date { get; set; }

        public string dateStr { get; set; }

        public TestSolution(Test test)
        {
            Name = test.Name;
            Category = test.Category;
            Points = 0;
            Total = test.QuestionsToAnswer;
            dateStr = "";
            Questions = new List<Question>();
            foreach(Question question in test.Questions)
            {
                Question CopyQuestion = new Question();
                CopyQuestion.Content = question.Content;
                CopyQuestion.Number = question.Number;
                CopyQuestion.Answers = new List<Answer>();
                foreach(Answer answer in question.Answers)
                {
                    Answer CopyAnswer = new Answer();
                    CopyAnswer.Value = answer.Value;
                    CopyAnswer.IsCorrect = false;
                    CopyAnswer.Number = answer.Number;
                    CopyQuestion.Answers.Add(CopyAnswer);
                }
                ShufflerService.Shuffle(CopyQuestion.Answers);
                Questions.Add(CopyQuestion);
            }
            ShufflerService.Shuffle(Questions);
            while(Questions.Count > test.QuestionsToAnswer)
            {
                Questions.RemoveAt(Questions.Count - 1);
            }
        }
    }
}
