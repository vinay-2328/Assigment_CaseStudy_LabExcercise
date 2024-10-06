using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.SubClass
{
    public class Concert : Event
    {
        public string Artist {  get; set; }
        public string Type { get; set; }

        public Concert() { }

        public Concert(string eventName, DateTime eventDate, TimeSpan eventTime, string venueName, int totalSeats,int availableSeats ,decimal ticketPrice, string artist, string type)
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

            Artist = artist;
            Type = type;
        }

        public void DisplayConcertDetails() 
        {
            EventServices eventServices = new EventServices();
            string eventDetails = eventServices.GetEventDetails(this);
            Console.WriteLine($"{eventDetails}\nArtist of the concert: {Artist}\nType of the concert: {Type}");
        }

    }
}
