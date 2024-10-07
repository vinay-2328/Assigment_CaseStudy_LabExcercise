using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.Entity
{
    public class Sports : Event
    {
        public string SportName { get; set; }
        public string Teams { get; set; }
    }
}
