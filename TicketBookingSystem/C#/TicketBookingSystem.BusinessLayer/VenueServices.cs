using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer
{
    public class VenueServices
    {

        public Venue CreateVenue(string venueName, string address)
        {
            return new Venue
            {
                VenueName = venueName,
                Address = address
            };
        }

        public void DisplayVenueDetails(Venue venue)
        {
            Console.WriteLine($"Venue: {venue.VenueName}\nAddress: {venue.Address}");
        }
    }
}
