using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;
using static TicketBookingSystem.Entity.Event;

namespace TicketBookingSystem.BusinessLayer.Services
{
    internal interface ITicketBookingSystemServices
    {
        Event createEvent(String eventName, DateTime eventDate, TimeSpan eventTime, String venueName, int totalSeats, int availableSeats, Decimal ticketPrice, EventType type, params object[] additionalInfo);
        void DisplayEventDetails(Event eventObj);
        void BookTickets(int numTickets, Event eventObj);
        void CancelTickets(int numTickets, Event eventObj);
    }
}
