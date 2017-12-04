using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataManager
{
    public static class QuestionData
    {
        public static void Add(int userID, int queryNumber, string name = null, int order = 0)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.QueryQuestionInsert", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
            sqlCmd.Parameters.Add(new SqlParameter("@QueryNumber", SqlDbType.Int)).Value = queryNumber;
            if (name != null) sqlCmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar)).Value = name;

            if (name != null) sqlCmd.Parameters.Add(new SqlParameter("@Ordinality", SqlDbType.Int)).Value = order;

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

        public static Question GetActive(string activeCode)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.ActiveQuestionGet", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@Code", SqlDbType.VarChar)).Value = activeCode.ToLower();

            DataTable dt = SqlHelper.TableExecute(sqlCmd);

            if (dt == null) return null;
            return new Question(dt.Rows[0]);
        }

        public static void SetActive(int activeQuestion, string code)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.ActiveQuestionUpdate", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@ActiveQuestionNumber", SqlDbType.Int)).Value = activeQuestion;
            sqlCmd.Parameters.Add(new SqlParameter("@Code", SqlDbType.VarChar)).Value = code;

            SqlHelper.Execute(sqlCmd);
        }

        public static List<Answer> GetAnswers(int userID, int queryNumber, int number)
        {
            List<Answer> Answers = new List<Answer>();

            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.QuestionAnswersGet", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
            sqlCmd.Parameters.Add(new SqlParameter("@QueryNumber", SqlDbType.Int)).Value = queryNumber;
            sqlCmd.Parameters.Add(new SqlParameter("@QuestionNumber", SqlDbType.Int)).Value = number;

            DataTable dt = SqlHelper.TableExecute(sqlCmd);
            DataView dv = dt.DefaultView;
            dv.Sort = "Ordinality ASC";

            foreach(DataRow dr in dv.Table.Rows)
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
            sqlCmd.Parameters.Add(new SqlParameter("@Ordinality", SqlDbType.Int)).Value = question.Order;
            
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

