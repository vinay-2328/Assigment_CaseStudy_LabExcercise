using System;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystem.Entity;


namespace TicketBookingSystem.BusinessLayer.Services
{
    internal class BookingServices : IBookingServices
    {
        BookingRepository bookingRepository;
       
        public void CancelBooking(int numTickets, Booking booking)
        {
            bookingRepository.CancelBooking(numTickets,booking);    
        }
        public decimal CalculateBookingCost(Event eventObj,int numTickets)
        {
            return bookingRepository.CalculateBookingCost(eventObj,numTickets);
        }
        public int GetAvailableNoOfTickets(Booking booking)
        {
            return bookingRepository.GetAvailableNoOfTickets(booking);
        }
        public void GetBookingDetails(Booking booking)
        {
            bookingRepository.GetBookingDetails(booking);
        }
    }
}
