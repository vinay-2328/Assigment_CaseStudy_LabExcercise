using System;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly EventRepository eventRepository;
        private readonly CustomerRepository customerRepository;

        public BookingRepository()
        {
            eventRepository = new EventRepository();
            customerRepository = new CustomerRepository();
        }

        public void CreateBooking(Event eventDetails, Customer[] customers, int numTickets)
        {
            
            if (eventDetails.AvailableSeats < numTickets)
            {
                throw new Exception("Not enough available tickets for the selected event.");
            }

            var booking = new Booking
            {
                Event = eventDetails,
                Customers = customers,
                NumTickets = numTickets,
                TotalCost = eventDetails.TicketPrice * numTickets,
                BookingDate = DateTime.Now 
            };

            eventRepository.BookTickets(numTickets, eventDetails);
            DisplayBookingDetails(booking);
        }

        public void CancelBooking(int numTickets, Booking booking)
        {
            if (numTickets > booking.NumTickets)
            {
                throw new Exception("Cannot cancel more tickets than booked.");
            }
            eventRepository.CancelTickets(numTickets,booking.Event);
            booking.NumTickets -= numTickets;
            booking.TotalCost = booking.Event.TicketPrice * booking.NumTickets;
            Console.WriteLine($"Booking updated. Remaining Tickets: {booking.NumTickets}");
        }

        public void DisplayBookingDetails(Booking booking)
        {
            Console.WriteLine($"BookingID: {booking.BookingId}\nBooking Date: {booking.BookingDate}\nEvent Name: {booking.Event.EventName}\nNumber of Tickets: {booking.NumTickets}\nTotal cost: {booking.TotalCost}");
            foreach(var customer in booking.Customers)
            {
                Console.WriteLine($"Customer name: {customer.CustomerName}, Customer Email: {customer.Email}, Customer Phone no.: {customer.PhoneNumber}");
            }
        }
    }
}
