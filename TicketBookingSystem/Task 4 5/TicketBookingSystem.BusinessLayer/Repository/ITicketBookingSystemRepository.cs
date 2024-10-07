using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    internal interface ITicketBookingSystemRepository
    {
        void DisplayEventDetails(Event eventObj);
        void BookTickets(int numTickets, Event eventObj);
        void CancelTickets(int numTickets, Event eventObj);

    }
}
