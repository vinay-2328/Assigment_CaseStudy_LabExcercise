using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.Util
{
    public  static class GetDBConn
    {
        public static SqlConnection GetConnection()
        {

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                return conn;
            }
            catch (SqlException ex)
            {
                
                Console.WriteLine($"SQL Exception: {ex.Message}");
                throw;
            }
        }

    }
}
