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
            AnswerData.Add(question.UserID, question.QueryNumber, question.Number, question.Answers.Count + 1, "New Answer");
            question.Answers = QuestionData.GetAnswers(question.UserID, question.QueryNumber, question.Number);
        }

        public static void DeleteAnswer(Question question, int number)
        {
            Answer answer = AnswerData.Get(question.UserID, question.QueryNumber, question.Number, number);
            AnswerData.Delete(answer);
            question.Answers = QuestionData.GetAnswers(question.UserID, question.QueryNumber, question.Number);
        }

        public static void SetFirstActive(Query query)
        {
            QuestionData.SetActive(1, query.Code);
        }
        public static void SetNextActive(Question currentQuestion)
        {
            QuestionData.SetActive(currentQuestion.Number + 1, currentQuestion.Code);
        }
        public static Question GetActive(int currentQuestionNumber, string code, bool hasAnswered)
        {
            Question activeQuestion = QuestionData.GetActive(code);

            if (activeQuestion.Number == currentQuestionNumber)
                activeQuestion.IsAnswered = hasAnswered;

            return activeQuestion;
        }
    }
}
