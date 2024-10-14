using PayXpert.Exception;
using System;
using System.Data.SqlClient;
using System.Configuration;
namespace PayXpert.Util
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            try
            {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
            SqlConnection conn =  new SqlConnection(connectionString);
            conn.Open();

            return conn;
            }catch(SqlException ex)
            {
                throw new DatabaseConnectionException("Failed to connect to the database.", ex);
            }
        }
    }
}
