using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    public class VenueRepository : IVenueRepository
    {
        public void DisplayVenueDetails(Venue venue)
        {
            Console.WriteLine("========== Displaying Venue Details ==========");
            Console.WriteLine($"Venue: {venue.VenueName}\nAddress: {venue.Address}");
        }
    }
}
