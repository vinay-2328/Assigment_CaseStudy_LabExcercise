using PayXpert.Exception;
using System;
using System.Data.SqlClient;

namespace PayXpert.Util
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            try
            {
            string connectionString= "Data Source=DESKTOP-L8BCFL0\\SQLEXPRESS;Initial Catalog=payXpert;Integrated Security=True";
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
