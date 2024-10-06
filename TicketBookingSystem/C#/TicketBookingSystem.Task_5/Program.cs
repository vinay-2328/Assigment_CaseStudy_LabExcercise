using System;
using TicketBookingSystem.BusinessLayer;
using TicketBookingSystem.BusinessLayer.SubClass;

namespace TicketBookingSystem.Task_5_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n-------------------THIS IS TASK 5-------------------\n");
            //1. create movie event
            Console.WriteLine("----------1. DISPLAYING MOVIE DETAILS----------");
            EventServices eventServices = new EventServices();
            //creating new movie
            Movie animal = new Movie("Animal", new DateTime(2024, 10, 6), new TimeSpan(19, 0, 0), "Inox theatre", 200, 200,199M, "Action", "Ranbir Kapoor", "Rashmika Mandanna");
            animal.DisplayMovieDetails();
            

            //2. creating concert event
            Console.WriteLine("\n----------2. DISPLAYING CONCERT DETAILS----------");
            Concert coldPlay = new Concert("ColdPlay Live", new DateTime(2024, 10, 06), new TimeSpan(19, 0, 0), "Balewadi Stadium", 1000, 1000, 2300M, "Chris Martin", "Music");
            coldPlay.DisplayConcertDetails();

            //3. creating Sports event
            Console.WriteLine("\n----------3. DISPLAYING SPORTS DETAILS----------");
            Sports ipl = new Sports("IPL",new DateTime(2024,12,1),new TimeSpan(16,0,0),"Ghahunje Stadium",5000,5000,5999,"Cricket","CSK VS MI");
            ipl.DisplaySportDetails();


            Console.ReadKey();
        }
    }
}
