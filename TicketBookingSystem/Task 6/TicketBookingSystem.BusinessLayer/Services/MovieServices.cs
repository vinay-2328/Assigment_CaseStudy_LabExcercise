using System;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Services
{
    public class MovieServices : IMovieServices
    {
        readonly MovieRepository repository = new MovieRepository();
        public void DisplayEventDetails(Event eventObj)
        {
            repository.DisplayEventDetails(eventObj);
        }
    }
}
