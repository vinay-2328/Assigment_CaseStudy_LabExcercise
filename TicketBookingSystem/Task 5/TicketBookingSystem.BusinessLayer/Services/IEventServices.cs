using System;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Services
{
    public interface IEventServices
    {
        decimal CalculateTotalRevenue(int bookedTickets, Event eventObj);
        int GetBookedNoOfTickets(Event eventObj);
        void BookTickets(int numTickets,Event eventObj);
        void CancelBooking(int numTickets,Event eventObj);
        void DisplayEventDetails(Event eventObj);
    }
}
