//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TicketBookingSystem.Entity;

//namespace TicketBookingSystem.BusinessLayer.Repository
//{
//    public class ConcertRepository : EventRepository, IConcertRepository
//    {
//        public override void GetEventDetails(Event eventObj)
//        {
//            base.GetEventDetails(eventObj);
//            if (eventObj is Concert concert)
//            {
//                Console.WriteLine($"Artist Name: {concert.Artist}\nConcert Type: {concert.ConcertType}");
//            }
//        }
//    }
//}
