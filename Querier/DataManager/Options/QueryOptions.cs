﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataManager
{
    public static class QueryOptions
    {
        public static Query Load(User user, int number)
        { 
            return QueryData.Get(user.UserID, number);
        }

        public static void Save(Query query)
        {
            QueryData.Save(query);
        }

        public static void AddQuestion(Query query, int order = 0)
        {
            QuestionData.Add(query.UserID, query.Number, "New Question");
            query.Questions = QueryData.GetQuestions(query.UserID, query.Number);
        }

        public static void DeleteQuestion(Query query, int number)
        {
            Question question = QuestionData.Get(query.UserID, query.Number, number);
            QuestionData.Delete(question);
            query.Questions = QueryData.GetQuestions(query.UserID, query.Number);
        }

        public static bool ValidCode(string code)
        {
            return QueryData.IsValid(code);
        }

        public static void Open(Query query)
        {
            QueryData.Open(query);
        }

        public static void Close(Query query)
        {
            QueryData.Close(query);
        }
    }
}
