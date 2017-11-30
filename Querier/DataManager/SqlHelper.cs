using System;
using System.Data;
using System.Data.SqlClient;

namespace DataManager
{
    public static class SqlHelper
    {
        public const string DefaultConnection = "Data Source=querierapp.cd5fn2tgvi2d.us-west-2.rds.amazonaws.com;Initial Catalog=Querier;user id=zarlo265;password=querierpassword1;";

        public static SqlConnection GetConnection() { return new SqlConnection(DefaultConnection); }

        public static DataTable TableExecute(SqlCommand sqlCmd)
        {
            DataTable dt = new DataTable();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            //System.InvalidOperationException: 'Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached.'
            sqlCmd.Connection.Open();

            foreach (SqlParameter parameter in sqlCmd.Parameters)
            {
                if (parameter == null)
                    parameter.Value = DBNull.Value;
            }

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
            adapter.Fill(dt);

            if (sqlCmd.Connection.State != ConnectionState.Closed)
                sqlCmd.Connection.Close();

            return dt;
        }

        public static int ScalarExecute(SqlCommand sqlCmd)
        {
            DataTable dt = new DataTable();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Connection.Open();

            foreach (SqlParameter parameter in sqlCmd.Parameters)
            {
                if (parameter == null)
                    parameter.Value = DBNull.Value;
            }

            object result = sqlCmd.ExecuteScalar();
            
            if (sqlCmd.Connection.State != ConnectionState.Closed)
                sqlCmd.Connection.Close();

            if (result != null)
                return (int)result;
            return -1;
        }

        public static string StringExecute(SqlCommand sqlCmd)
        {
            DataTable dt = new DataTable();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Connection.Open();

            foreach (SqlParameter parameter in sqlCmd.Parameters)
            {
                if (parameter == null)
                    parameter.Value = DBNull.Value;
            }

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
            adapter.Fill(dt);

            if (sqlCmd.Connection.State != ConnectionState.Closed)
                sqlCmd.Connection.Close();

            return dt.Rows[0][0].ToString();
        }
        
        public static void Execute(SqlCommand sqlCmd)
        {
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Connection.Open();

            foreach (SqlParameter parameter in sqlCmd.Parameters)
            {
                if (parameter == null)
                    parameter.Value = DBNull.Value;
            }
            //System.Data.SqlClient.SqlException: 'Procedure or function 'QuestionUpdate' expects parameter '@Ordinality', which was not supplied.'

            //System.Data.SqlClient.SqlException: 'Cannot insert the value NULL into column 'ordinality', table 'Querier.dbo.question'; column does not allow nulls. INSERT fails. The statement has been terminated.'
            //System.Data.SqlClient.SqlException: 'Cannot insert the value NULL into column 'number', table 'Querier.dbo.question'; column does not allow nulls. INSERT fails. The statement has been terminated.'

            sqlCmd.ExecuteNonQuery();

            if (sqlCmd.Connection.State != ConnectionState.Closed)
                sqlCmd.Connection.Close();
        }
    }
}
