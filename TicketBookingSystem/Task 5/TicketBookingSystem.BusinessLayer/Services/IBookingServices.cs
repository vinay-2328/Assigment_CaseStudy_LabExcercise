using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Services
{
    internal interface IBookingServices
    {
        void CancelBooking(int numTickets, Booking booking);
        decimal CalculateBookingCost(Event eventObj, int numTickets);
        int GetAvailableNoOfTickets(Booking booking);
        void GetBookingDetails(Booking booking);
    }
}
