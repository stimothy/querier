using System;
using System.Collections.Generic;
using System.Text;

namespace DataManager
{
    public static class QuestionOptions
    {
        public static Question Load(Query query, int number)
        {
            return QuestionData.Get(query.UserID, query.Number, number);
        }

        public static void Save(Question question)
        {
            QuestionData.Save(question);
        }

        public static void AddAnswer(Question question)
        {
            AnswerData.Add(question.UserID, question.Number, question.Number);
            question.Answers = QuestionData.GetAnswers(question.UserID, question.QueryNumber, question.Number);
        }

        public static void DeleteAnswer(Question question, int number)
        {
            Answer answer = AnswerData.Get(question.UserID, question.QueryNumber, question.Number, number);
            AnswerData.Delete(answer);
            question.Answers = QuestionData.GetAnswers(question.UserID, question.QueryNumber, question.Number);
        }
    }
}
