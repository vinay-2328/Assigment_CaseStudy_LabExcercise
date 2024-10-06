using System;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.SubClass
{
    public class Movie : Event 
    {
        public string Genre { get; set; }
        public string Actor { get; set; }
        public string Actress { get; set; }

        public Movie() { }

        public Movie(string eventName, DateTime eventDate, TimeSpan eventTime, string venueName, int totalSeats,int availableSeats, decimal ticketPrice, string genre, string actor, string actress)
        {
            // Set properties using the base class
            EventName = eventName;
            EventDate = eventDate;
            EventTime = eventTime;
            VenueName = venueName;
            TotalSeats = totalSeats;
            AvailableSeats = availableSeats;
            TicketPrice = ticketPrice;
            eventType = EventType.Movie; 

            Genre = genre;
            Actor = actor;
            Actress = actress;
        }

        public void DisplayMovieDetails()
        {
            EventServices eventServices = new EventServices();  
            string eventDetails = eventServices.GetEventDetails(this);
            Console.WriteLine($"{eventDetails}\nGenre: {Genre}\nLead Actor: {Actor}\nLead Actress: {Actress}");
        }
    }
}
