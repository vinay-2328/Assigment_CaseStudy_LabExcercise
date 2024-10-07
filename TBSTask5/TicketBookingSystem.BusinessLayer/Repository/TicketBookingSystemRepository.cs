using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;
using static TicketBookingSystem.Entity.Event;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    public class TicketBookingSystemRepository
    {
        public Event createEvent(String eventName,DateTime eventDate,TimeSpan eventTime,String venueName,int totalSeats,int availableSeats,Decimal ticketPrice,EventType type,params object[] additionalInfo)
        {
            Event eventObj;

            switch (type.ToString().ToLower()) 
            {
                case "movie":
                    Movie movie = new Movie();

                    movie.EventName = eventName;
                    movie.EventDate = eventDate;
                    movie.EventTime = eventTime;
                    movie.VenueName = venueName;
                    movie.TotalSeats = totalSeats;
                    movie.AvailableSeats = availableSeats;
                    movie.TicketPrice = ticketPrice;
                    movie.Type = EventType.Movie;
                    movie.Genre = Convert.ToString(additionalInfo[0]);
                    movie.Actor = Convert.ToString(additionalInfo[1]);  
                    movie.Actress = Convert.ToString(additionalInfo[2]);

                    eventObj = movie;
                    break;
                case "concert":
                    Concert concert = new Concert();

                    concert.EventName = eventName;
                    concert.EventDate = eventDate;
                    concert.EventTime = eventTime;
                    concert.VenueName = venueName;
                    concert.TotalSeats = totalSeats;
                    concert.AvailableSeats = availableSeats;
                    concert.TicketPrice = ticketPrice;
                    concert.Type = EventType.Movie;
                    concert.Artist = Convert.ToString(additionalInfo[0]);
                    concert.ConcertType = Convert.ToString(additionalInfo[1]);  
                    
                    eventObj = concert;
                    break;

                
            }

        }
    }
}
