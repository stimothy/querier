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

        public static void AddAnswer(Question question, int order = 0, string name = "")
        {
            AnswerData.Add(question.UserID, 
                            question.QueryNumber, 
                            question.Number, 
                            (order == 0) ? question.Answers.Count + 1 : order, 
                            (name == "") ? "New Answer" : name);
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
            try
            {
                int number = query.Questions[0].Number;
                QuestionData.SetActive(number, query.Code);
            }
            catch(IndexOutOfRangeException ex)
            { }
        }
        public static void SetNextActive(Question currentQuestion, int number)
        {
            QuestionData.SetActive(number, currentQuestion.Code);
        }

        public static Question GetActive(int currentQuestionNumber, string code, bool hasAnswered)
        {
            Question activeQuestion = QuestionData.GetActive(code);

            if (activeQuestion != null && activeQuestion.Number == currentQuestionNumber)
                activeQuestion.IsAnswered = hasAnswered;

            return activeQuestion;
        }
    }
}
