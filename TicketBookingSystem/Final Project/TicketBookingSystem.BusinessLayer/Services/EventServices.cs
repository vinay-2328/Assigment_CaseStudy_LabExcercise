using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystem.BusinessLayer.Services;
using TicketBookingSystem.Entity;
using static TicketBookingSystem.Entity.Event;

namespace TicketBookingSystem.BusinessLayer
{
    public class EventServices : IEventServices
    {
        EventRepository eventRepository;
        
        public EventServices()
        {
            eventRepository = new EventRepository();
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return eventRepository.GetAllEvents();
        }

        public IEnumerable<Event> GetEventDetailByEventType(EventType eventType)
        {
            return eventRepository.GetEventDetailByEventType(eventType);
        }
        public Event CreateEvent(string eventName, DateTime date, TimeSpan time, int totalSeats, decimal ticketPrice, string eventType, Venue venue)
        {
            return eventRepository.CreateEvent(eventName,date,time,totalSeats,ticketPrice,eventType,venue);
        }
        public Event GetEventDetails(int eventID)
        {
            return eventRepository.GetEventDetails(eventID);
        }
        public void BookTickets(int eventID, int numTickets)
        {
            eventRepository.BookTickets(eventID, numTickets);
        }
        public void CancelTickets(int eventID, int numTickets)
        {
            eventRepository.CancelTickets(eventID, numTickets);
        }
    }
}
