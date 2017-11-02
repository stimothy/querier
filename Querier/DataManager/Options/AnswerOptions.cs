using System;
using System.Collections.Generic;
using System.Text;

namespace DataManager.Options
{
    public static class AnswerOptions
    {
        public static Answer Load(Question question, int number)
        {
            return AnswerData.Get(question.UserID, question.QueryNumber, question.Number, number);
        }

        public static void Save(Answer answer)
        {
            AnswerData.Save(answer);
        }
    }
}
