using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.SubClass
{
    public class Sports:Event
    {
        public string SportName { get; set; }
        public string Teams { get; set; }

        public Sports() { }
        public Sports(string eventName, DateTime eventDate, TimeSpan eventTime, string venueName, int totalSeats, int availableSeats, decimal ticketPrice, string sportName, string teams)
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

            SportName = sportName;
            Teams = teams;

        }

        public void DisplaySportDetails()
        {
            EventServices eventServices = new EventServices();
            string eventDetails = eventServices.GetEventDetails(this);

            Console.WriteLine($"{eventDetails}\nName of the Sport: {SportName}\nTeams: {Teams}");    
        }
    }
}
