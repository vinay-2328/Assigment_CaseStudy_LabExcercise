using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Services
{
    public interface IBookingServices
    {
        Booking GetBookingByID(int bookingID);
        IEnumerable<Booking> GetAllBooking(int customerID);
        void CreateBooking(Event eventDetails, Customer[] customers, int numTickets);
        void CancelBooking(int numTickets, Booking booking);
        void DisplayBookingDetails(Booking booking);
    }
}
