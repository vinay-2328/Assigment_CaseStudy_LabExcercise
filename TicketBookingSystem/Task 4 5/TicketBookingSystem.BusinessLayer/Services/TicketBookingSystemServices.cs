using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Services
{
    internal class TicketBookingSystemServices : ITicketBookingSystemServices
    {
        TicketBookingSystemRepository repository;
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
