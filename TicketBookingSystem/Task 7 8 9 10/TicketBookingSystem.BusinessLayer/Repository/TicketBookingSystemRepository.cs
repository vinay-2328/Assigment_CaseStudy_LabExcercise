using System;
using System.CodeDom;
using TicketBookingSystem.Entity;
using static TicketBookingSystem.Entity.Event;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    public class TicketBookingSystemRepository : EventRepository,ITicketBookingSystemRepository
    {
        public Event createEvent(String eventName,DateTime eventDate,TimeSpan eventTime,String venueName,int totalSeats,int availableSeats,Decimal ticketPrice,EventType type,params object[] additionalInfo)
        {
            Event eventObj;

            switch (type.ToString().ToLower()) 
            {

                case "movie":
                    if (additionalInfo.Length < 3)
                        throw new Exception("Incomplete additional info");
                    
                    Movie movie = new Movie
                    { 
                        EventName = eventName,
                        EventDate = eventDate,
                        EventTime = eventTime,
                        VenueName = venueName,
                        TotalSeats = totalSeats,
                        AvailableSeats = availableSeats,
                        TicketPrice = ticketPrice,
                        Type = EventType.Movie,
                        Genre = Convert.ToString(additionalInfo[0]),
                        Actor = Convert.ToString(additionalInfo[1]),
                        Actress = Convert.ToString(additionalInfo[2]),
                    };

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
                    concert.Type = EventType.Concert;
                    concert.Artist = Convert.ToString(additionalInfo[0]);
                    concert.ConcertType = Convert.ToString(additionalInfo[1]);  
                    
                    eventObj = concert;
                    break;

                case "sports":
                    Sports sports = new Sports();

                    sports.EventName = eventName;
                    sports.EventDate = eventDate;
                    sports.EventTime = eventTime;
                    sports.VenueName = venueName;
                    sports.TotalSeats = totalSeats;
                    sports.AvailableSeats = availableSeats;
                    sports.TicketPrice = ticketPrice;
                    sports.Type = EventType.Sports;
                    sports.SportName = Convert.ToString(additionalInfo[0]);
                    sports.Teams = Convert.ToString(additionalInfo[1]);

                    eventObj = sports;

                    break;

                default:
                    throw new Exception("Invalid Event type");
            }
            return eventObj;
        }

        public override void DisplayEventDetails(Event eventObj)
        {
            base.DisplayEventDetails(eventObj);
            if (eventObj is Movie movie)
            {
                MovieRepository movieRepository = new MovieRepository();
                movieRepository.DisplayEventDetails(movie);
            }
            else if (eventObj is Sports sports)
            {
                SportsRepository sportsRepository = new SportsRepository();
                sportsRepository.DisplayEventDetails(sports);

            }
            else if (eventObj is Concert concert) 
            {
                ConcertRepository concertRepository = new ConcertRepository();
                concertRepository.DisplayEventDetails(concert);
            }
            else
            {
                throw new Exception("Not valid Event");
            }

        }

        public override void BookTickets(int numTickets,Event eventObj)
        {
            base.BookTickets(numTickets, eventObj);
        }

        public override void CancelTickets(int numTickets,Event eventObj)
        {
            base.CancelTickets(numTickets, eventObj);
        }
    }
}
