using System;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    public interface IMovieRepository
    {
        void DisplayEventDetails(Event eventObj);
    }
}
