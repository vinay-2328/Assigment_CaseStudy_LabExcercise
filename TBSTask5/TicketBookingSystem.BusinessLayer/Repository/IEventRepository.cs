using System;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    public interface IEventRepository
    {
        void DisplayEventDetails(Event eventObj);
        decimal CalculateTotalRevenue(int bookedTickets, Event eventObj);
        int GetBookedNoOfTickets(Event eventObj);
        void BookTickets(int numTickets, Event eventObj);
        void CancelTickets(int numTickets, Event eventObj);
    }
}
