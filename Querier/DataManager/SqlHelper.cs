using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataManager
{
    public static class SqlHelper
    {
        public const string DefaultConnection = "Data Source=querierapp2.chx2d99q6lfi.us-west-1.rds.amazonaws.com;Initial Catalog=Querier;user id=zarlo265;password=querierpassword1;";

        public static SqlConnection GetConnection() { return new SqlConnection(DefaultConnection); }

        public static DataTable TableExecute(SqlCommand sqlCmd)
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

            sqlCmd.ExecuteNonQuery();

            if (sqlCmd.Connection.State != ConnectionState.Closed)
                sqlCmd.Connection.Close();
        }
    }
}
