using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystem.BusinessLayer.Services;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer
{
    public class EventServices : IEventServices
    {
        EventRepository eventRepository;
        public decimal CalculateTotalRevenue(int bookedTickets, Event eventObj)
        {
            return eventRepository.CalculateTotalRevenue(bookedTickets,eventObj);
        }
        public int GetBookedNoOfTickets(Event eventObj)
        {
            return eventRepository.GetBookedNoOfTickets(eventObj);
        }
        public void BookTickets(int numTickets, Event eventObj)
        {
            eventRepository.BookTickets(numTickets, eventObj);
        }
        public void CancelBooking(int numTickets, Event eventObj)
        {
            eventRepository.CancelTickets(numTickets, eventObj);
        }
        public void DisplayEventDetails(Event eventObj)
        {
            eventRepository.DisplayEventDetails(eventObj);
        }
    }
}
