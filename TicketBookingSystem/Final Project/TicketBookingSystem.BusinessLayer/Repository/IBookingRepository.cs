using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    internal interface IBookingRepository
    {
        IEnumerable<Booking> GetAllBooking(int customerID);
        void CreateBooking(Event eventDetails, Customer[] customers, int numTickets);
        void CancelBooking(int numTickets, Booking booking);
        void DisplayBookingDetails(Booking booking);
        Booking GetBookingByID(int bookingID);


    }
}
