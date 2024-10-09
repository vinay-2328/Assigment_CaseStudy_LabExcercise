using System;


namespace TicketBookingSystem.Entity
{
    public class Movie : Event
    {
        public string Genre { get; set; }
        public string Actor { get; set; }
        public string Actress { get; set; }
    }
}
