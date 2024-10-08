using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    public class BookingRepository : IBookingRepository
    {
        readonly EventRepository eventRepository;
        public void CancelBooking(int numTickets,Booking booking) 
        {
            if (numTickets > booking.NumTickets)
            {
                throw new Exception("Cannot cancel more tickets than booked.");
            }
            eventRepository.CancelTickets(numTickets,booking.Event);
        }



        public decimal CalculateBookingCost(Event eventObj,int numTickets)
        { 
            return eventObj.TicketPrice * numTickets;
        }

        public int GetAvailableNoOfTickets(Booking booking)
        {
            return booking.Event.AvailableSeats;
        }

        public void GetBookingDetails(Booking booking)
        {
            Console.WriteLine($"BookingID: {booking.BookingId}\nBooking Date: {booking.BookingDate}\nEvent Name: {booking.Event.EventName}\nCustomer Name: {booking.Customer.CustomerName}\nNumber of Tickets: {booking.NumTickets}\nTotal cost: {booking.TotalCost}");
        }



    }
}
