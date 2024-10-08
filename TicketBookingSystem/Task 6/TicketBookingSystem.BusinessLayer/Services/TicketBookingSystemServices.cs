using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystem.Entity;
using static TicketBookingSystem.Entity.Event;

namespace TicketBookingSystem.BusinessLayer.Services
{
    public class TicketBookingSystemServices : ITicketBookingSystemServices
    {
        private readonly TicketBookingSystemRepository repository;

        public TicketBookingSystemServices()
        {
            repository = new TicketBookingSystemRepository();
        }

        public Event createEvent(String eventName, DateTime eventDate, TimeSpan eventTime, String venueName, int totalSeats, int availableSeats, Decimal ticketPrice, EventType type, params object[] additionalInfo)
        {
            return repository.createEvent(eventName, eventDate, eventTime, venueName, totalSeats, availableSeats, ticketPrice, type, additionalInfo);
        }

        public void DisplayEventDetails(Event eventObj)
        {
            repository.DisplayEventDetails(eventObj);
        }

        public void BookTickets(int numTickets,Event eventObj)
        {
            repository.BookTickets(numTickets, eventObj);
        }

        public void CancelTickets(int numTickets, Event eventObj) 
        { 
            repository.CancelTickets(numTickets,eventObj);        
        }
    }
}
