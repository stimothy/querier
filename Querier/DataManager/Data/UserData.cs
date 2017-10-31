using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataManager
{
    public static class UserData
    {
        public static int GetUserID(string loginID)
        {
            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.UserIDGet", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@LoginID", SqlDbType.Int)).Value = loginID;

            return SqlHelper.ScalarExecute(sqlCmd);
        }

        public static List<Query> GetQueries(int userID)
        {
            List<Query> Queries = new List<Query>();

            SqlCommand sqlCmd = new SqlCommand("Querier.dbo.UserQueriesGet", SqlHelper.GetConnection());
            sqlCmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = userID;

            DataTable dt = SqlHelper.TableExecute(sqlCmd);

            foreach (DataRow dr in dt.Rows)
            {
                Queries.Add(new Query(dr));
            }

            return Queries;
        }
    }
}
