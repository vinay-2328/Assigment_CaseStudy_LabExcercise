using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.Entity
{
    public class Concert : Event
    {
        public string Artist { get; set; }
        public string ConcertType { get; set; }
    }
}
