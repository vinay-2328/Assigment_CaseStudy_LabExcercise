using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Services
{
    public class ConcertServices : IConcertServices
    {
        readonly ConcertRepository concertRepository;
        public void DisplayEventDetails(Event eventObj)
        {
            concertRepository.DisplayEventDetails(eventObj);
        }
    }
}
