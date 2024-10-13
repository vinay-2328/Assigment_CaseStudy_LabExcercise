using System;
using System.Collections.Generic;
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

                string connectionString = "Data Source=DESKTOP-L8BCFL0\\SQLEXPRESS;Initial Catalog=TicketBookingSystem;Integrated Security=True";
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                return conn;
        }

    }
}
