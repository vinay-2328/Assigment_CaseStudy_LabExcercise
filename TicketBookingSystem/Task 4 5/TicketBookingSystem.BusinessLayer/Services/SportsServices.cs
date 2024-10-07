using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Services
{
    internal class SportsServices : ISportsServices
    {
        readonly SportsRepository repository;
        public void DisplayEventDetails(Event eventObj)
        {
            repository.DisplayEventDetails(eventObj);
        }
    }
}
