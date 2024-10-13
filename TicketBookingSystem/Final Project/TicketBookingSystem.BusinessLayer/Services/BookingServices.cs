using System;
using System.Collections.Generic;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystem.Entity;


namespace TicketBookingSystem.BusinessLayer.Services
{
    public class BookingServices : IBookingServices
    {
        BookingRepository bookingRepository;
        public BookingServices()
        {
            bookingRepository = new BookingRepository();
        }

        public Booking GetBookingByID(int bookingID)
        {
            return bookingRepository.GetBookingByID(bookingID);
        }
        public IEnumerable<Booking> GetAllBooking(int customerID)
        {
            return bookingRepository.GetAllBooking(customerID);
        }
        public void CancelBooking(int numTickets, Booking booking)
        {
            bookingRepository.CancelBooking(numTickets, booking);
        }
        public void CreateBooking(Event eventDetails, Customer[] customers, int numTickets)
        {
            bookingRepository.CreateBooking(eventDetails, customers, numTickets);
        }


        public void DisplayBookingDetails(Booking booking)
        {
            bookingRepository.DisplayBookingDetails(booking);
        }
    }
}
