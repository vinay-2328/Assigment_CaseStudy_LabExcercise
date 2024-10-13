using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Services
{
    internal interface IVenueServices
    {
        void DisplayVenueDetails(Venue venue);
    }
}
