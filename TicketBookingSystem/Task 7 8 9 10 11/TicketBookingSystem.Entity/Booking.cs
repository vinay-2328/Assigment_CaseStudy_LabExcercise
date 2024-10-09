using System;

namespace TicketBookingSystem.Entity
{
    public class Booking
    {
        public int BookingId { get; set; }
        public Customer[] Customers { get; set; }
        public Event Event { get; set; }
        public int NumTickets { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime BookingDate { get; set; }



    }
}
