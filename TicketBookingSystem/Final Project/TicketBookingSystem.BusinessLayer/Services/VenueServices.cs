using System;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Services
{
    internal class VenueServices : IVenueServices
    {
        VenueRepository repository;

        public void DisplayVenueDetails(Venue venue) 
        {
            repository.DisplayVenueDetails(venue);        
        }
    }
}
