﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataManager
{
    public static class QuestionData
    {
        public static void Add(int userID, int queryNumber)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.QueryQuestionInsert", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
            sqlCmd.Parameters.Add(new SqlParameter("@QueryNumber", SqlDbType.Int)).Value = queryNumber;

            SqlHelper.Execute(sqlCmd);
        }

        public static Question Get(int userID, int queryNumber, int number)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.QuestionGet", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
            sqlCmd.Parameters.Add(new SqlParameter("@QueryNumber", SqlDbType.Int)).Value = queryNumber;
            sqlCmd.Parameters.Add(new SqlParameter("@QuestionNumber", SqlDbType.Int)).Value = number;

            DataTable dt = SqlHelper.TableExecute(sqlCmd);

            return new Question(dt.Rows[0]);
        }

        public static List<Answer> GetAnswers(int userID, int queryNumber, int number)
        {
            List<Answer> Answers = new List<Answer>();

            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.QuestionAnswersGet", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
            sqlCmd.Parameters.Add(new SqlParameter("@QueryNumber", SqlDbType.Int)).Value = queryNumber;
            sqlCmd.Parameters.Add(new SqlParameter("@QuestionNumber", SqlDbType.Int)).Value = number;

            DataTable dt = SqlHelper.TableExecute(sqlCmd);

            foreach(DataRow dr in dt.Rows)
            {
                Answers.Add(new Answer(dr));
            }

            return Answers;
        }

        public static void Save(Question question)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.QuestionUpdate", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = question.UserID;
            sqlCmd.Parameters.Add(new SqlParameter("@QueryNumber", SqlDbType.Int)).Value = question.QueryNumber;
            sqlCmd.Parameters.Add(new SqlParameter("@QuestionNumber", SqlDbType.Int)).Value = question.Number;
            sqlCmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar)).Value = question.Name;

            SqlHelper.Execute(sqlCmd);
        }

        public static void Delete(Question question)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.QuestionDelete", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = question.UserID;
            sqlCmd.Parameters.Add(new SqlParameter("@QueryNumber", SqlDbType.Int)).Value = question.QueryNumber;
            sqlCmd.Parameters.Add(new SqlParameter("@QuestionNumber", SqlDbType.Int)).Value = question.Number;

            SqlHelper.Execute(sqlCmd);
        }
    }
}
