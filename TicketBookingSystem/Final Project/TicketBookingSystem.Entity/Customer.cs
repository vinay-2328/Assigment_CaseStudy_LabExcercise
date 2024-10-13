using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.Entity
{
    public class Customer
    {
        public int CustomerID {  get; set; }
        public String CustomerName { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public int? BookingID { get; set; }
        public string Password { get; set; } = null; 


    }
}
