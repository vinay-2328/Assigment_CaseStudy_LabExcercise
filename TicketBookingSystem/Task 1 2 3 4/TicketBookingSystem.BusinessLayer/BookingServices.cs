using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer
{
    public class BookingServices
    {
        public Booking CreateBooking(int bookingId,Customer customer, Event eventObj, int numTickets, EventServices eventServices)
        {
            if (numTickets <= eventObj.AvailableSeats)
            {
                Booking booking = new Booking
                {
                    BookingId=bookingId,
                    Customer = customer,
                    Event = eventObj,
                    NumTickets = numTickets,
                    TotalCost = eventObj.TicketPrice * numTickets,
                    BookingDate = DateTime.Now
                };

                eventServices.BookTickets(eventObj, numTickets);
                return booking;
            }
            else
            {
                throw new Exception("Not enough available tickets");
            }
        }


        public void CancelBooking(Booking booking, int numTickets, EventServices eventServices)
        {
            if (numTickets > booking.NumTickets)
            {
                throw new Exception("Cannot cancel more tickets than booked.");
            }

            eventServices.CancelBooking(booking.Event, numTickets);
        }



        public decimal CalculateBookingCost(decimal ticketPrice, int numTickets)
        {
            return ticketPrice * numTickets;
        }

        public int GetAvailableNoOfTickets(Event eventObj)
        {
            return eventObj.AvailableSeats;
        }

        public void GetEventDetails(Booking booking)
        {
            Console.WriteLine($"BookingID: {booking.BookingId}\nBooking Date: {booking.BookingDate}\nEvent Name: {booking.Event.EventName}\nCustomer Name: {booking.Customer.CustomerName}\nNumber of Tickets: {booking.NumTickets}\nTotal cost: {booking.TotalCost}");
        }



    }
}
