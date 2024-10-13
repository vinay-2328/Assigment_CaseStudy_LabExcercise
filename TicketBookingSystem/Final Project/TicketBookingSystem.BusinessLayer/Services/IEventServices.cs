using System;
using System.Collections.Generic;
using TicketBookingSystem.Entity;
using static TicketBookingSystem.Entity.Event;

namespace TicketBookingSystem.BusinessLayer.Services
{
    public interface IEventServices
    {
        IEnumerable<Event> GetAllEvents();
        Event CreateEvent(string eventName, DateTime date, TimeSpan time, int totalSeats, decimal ticketPrice, string eventType, Venue venue);
        Event GetEventDetails(int eventID);
        IEnumerable<Event> GetEventDetailByEventType(EventType eventType);
        void BookTickets(int eventID, int numTickets);
        void CancelTickets(int eventID, int numTickets);
    }
}
