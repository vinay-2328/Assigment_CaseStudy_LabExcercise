using System;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    internal class MovieRepository : EventRepository, IMovieRepository
    {
        public override void DisplayEventDetails(Event eventObj)
        {
            base.DisplayEventDetails(eventObj);
            if (eventObj is Movie movieObj) 
            {
                Console.WriteLine($"Genre: {movieObj.Genre}\nLead Actor: {movieObj.Actor}\nLead Actress: {movieObj.Actress}");
            }

        }
    }
}
