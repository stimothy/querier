using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataManager
{
    public static class QueryData
    {
        public static int Add(int userID, string name = null)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.UserQueryInsert", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
            if (name != null) sqlCmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar)).Value = name;

            return SqlHelper.ScalarExecute(sqlCmd);
        }

        public static Query Get(int userID, int number)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.QueryGet", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
            sqlCmd.Parameters.Add(new SqlParameter("@QueryNumber", SqlDbType.Int)).Value = number;

            DataTable dt = SqlHelper.TableExecute(sqlCmd);

            return new Query(dt.Rows[0]);
        }

        public static List<Question> GetQuestions(int userID, int number)
        {
            List<Question> Questions = new List<Question>();

            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.QueryQuestionsGet", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;
            sqlCmd.Parameters.Add(new SqlParameter("@QueryNumber", SqlDbType.Int)).Value = number;

            DataTable dt = SqlHelper.TableExecute(sqlCmd);
            DataView dv = dt.DefaultView;
            dv.Sort = "Ordinality ASC";

            foreach (DataRow dr in dv.Table.Rows)
            {
                Questions.Add(new Question(dr));
            }

            return Questions;
        }

        public static void Save(Query query)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.QueryUpdate", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = query.UserID;
            sqlCmd.Parameters.Add(new SqlParameter("@QueryNumber", SqlDbType.Int)).Value = query.Number;
            sqlCmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar)).Value = query.Name;

            SqlHelper.Execute(sqlCmd);
        }

        public static void Delete(Query query)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.QueryDelete", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = query.UserID;
            sqlCmd.Parameters.Add(new SqlParameter("@QueryNumber", SqlDbType.Int)).Value = query.Number;

            SqlHelper.Execute(sqlCmd);
        }
    }
}
