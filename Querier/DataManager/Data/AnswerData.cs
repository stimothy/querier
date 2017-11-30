using System;
using System.Data;
using System.Data.SqlClient;

namespace DataManager
{
    public static class AnswerData
    {
        public static void Add(int userID, int queryNumber, int questionNumber, int order = 0, string name = null)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.QuestionAnswerInsert", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
            sqlCmd.Parameters.Add(new SqlParameter("@QueryNumber", SqlDbType.Int)).Value = queryNumber;
            sqlCmd.Parameters.Add(new SqlParameter("@QuestionNumber", SqlDbType.Int)).Value = questionNumber;
            if (name != null) sqlCmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar)).Value = name;
            if (order != 0) sqlCmd.Parameters.Add(new SqlParameter("@Ordinality", SqlDbType.Int)).Value = order;

            SqlHelper.Execute(sqlCmd);
        }

        public static Answer Get(int userID, int queryNumber, int questionNumber, int number)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.AnswerGet", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
            sqlCmd.Parameters.Add(new SqlParameter("@QueryNumber", SqlDbType.Int)).Value = queryNumber;
            sqlCmd.Parameters.Add(new SqlParameter("@QuestionNumber", SqlDbType.Int)).Value = questionNumber;
            sqlCmd.Parameters.Add(new SqlParameter("@AnswerNumber", SqlDbType.Int)).Value = number;

            DataTable dt = SqlHelper.TableExecute(sqlCmd);

            return new Answer(dt.Rows[0]);
        }

        public static void Save(Answer answer)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.AnswerUpdate", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = answer.UserID;
            sqlCmd.Parameters.Add(new SqlParameter("@QueryNumber", SqlDbType.Int)).Value = answer.QueryNumber;
            sqlCmd.Parameters.Add(new SqlParameter("@QuestionNumber", SqlDbType.Int)).Value = answer.QuestionNumber;
            sqlCmd.Parameters.Add(new SqlParameter("@AnswerNumber", SqlDbType.Int)).Value = answer.Number;
            sqlCmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar)).Value = answer.Name;
            sqlCmd.Parameters.Add(new SqlParameter("@Score", SqlDbType.Int)).Value = answer.Score;
            sqlCmd.Parameters.Add(new SqlParameter("@Ordinality", SqlDbType.Int)).Value = answer.Order;

            SqlHelper.Execute(sqlCmd);
        }

        public static void Delete(Answer answer)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.AnswerDelete", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = answer.UserID;
            sqlCmd.Parameters.Add(new SqlParameter("@QueryNumber", SqlDbType.Int)).Value = answer.QueryNumber;
            sqlCmd.Parameters.Add(new SqlParameter("@QuestionNumber", SqlDbType.Int)).Value = answer.QuestionNumber;
            sqlCmd.Parameters.Add(new SqlParameter("@AnswerNumber", SqlDbType.Int)).Value = answer.Number;

            SqlHelper.Execute(sqlCmd);
        }
    }
}
