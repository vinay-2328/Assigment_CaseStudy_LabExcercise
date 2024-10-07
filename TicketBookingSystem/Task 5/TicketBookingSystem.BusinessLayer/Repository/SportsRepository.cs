using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    public class SportsRepository : EventRepository,ISportsRepository
    {
        public override void DisplayEventDetails(Event eventObj)
        {
            base.DisplayEventDetails(eventObj);
            if (eventObj is Sports sports)
            {
                Console.WriteLine($"Type of Sports: {sports.SportName}\nTeams: {sports.Teams}");
            }
        }
    }
}
