using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.Exception
{
    public class EventNotFoundException : System.Exception
    {
        public EventNotFoundException(int eventId)
            : base($"Event with ID {eventId} was not found.")
        {

        }
    }
}
