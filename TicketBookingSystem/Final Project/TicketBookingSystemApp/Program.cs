using System;
using TicketBookingSystem.Entity;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystemApp;
using TicketBookingSystem.Util;

namespace TicketBookingSystem
{
    public class TicketBookingSystem
    {
        private static EventRepository eventRepo = new EventRepository();

        public static void Main(string[] args)
        {
            

            Menu menu = new Menu();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                ConsoleColorHelper.SetHeadingColor();
                Console.WriteLine("===============================================================");
                Console.WriteLine("                         USER MENU                              ");
                Console.WriteLine("===============================================================");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");
                Console.WriteLine("---------------------------------------------------------------");

                ConsoleColorHelper.ResetColor();
                ConsoleColorHelper.SetSubheadingColor();
                Console.Write("Enter your choice: ");
                ConsoleColorHelper.ResetColor();

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ConsoleColorHelper.SetInfoColor();
                        Console.WriteLine("\nYou selected: Login\n");
                        ConsoleColorHelper.ResetColor();
                        menu.Login();
                        break;

                    case 2:
                        ConsoleColorHelper.SetInfoColor();
                        Console.WriteLine("\nYou selected: Register\n");
                        ConsoleColorHelper.ResetColor();
                        menu.Register();
                        break;

                    case 3:
                        exit = true;
                        ConsoleColorHelper.SetSuccessColor();
                        Console.Beep();
                        Console.WriteLine("Exiting the Application...");
                        ConsoleColorHelper.ResetColor();
                        break;

                    default:
                        ConsoleColorHelper.SetErrorColor();
                        Console.WriteLine("Invalid choice. Please try again.");
                        ConsoleColorHelper.ResetColor();
                        break;
                }
            }
        }

        private static void CreateEvent()
        {
            Console.Write("Enter Event Name: ");
            string eventName = Console.ReadLine();

            Console.Write("Enter Event Date (yyyy-MM-dd): ");
            DateTime eventDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Event Time (HH:mm): ");
            TimeSpan eventTime = TimeSpan.Parse(Console.ReadLine());

            Console.Write("Enter Total Seats: ");
            int totalSeats = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Ticket Price: ");
            decimal ticketPrice = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter Event Type (Movie, Sports, Concert): ");
            string eventType = Console.ReadLine();

            Venue venue = new Venue() { VenueID = 1 };  
            eventRepo.CreateEvent(eventName, eventDate, eventTime, totalSeats, ticketPrice, eventType, venue);
        }

        private static void BookTickets()
        {
            Console.Write("Enter Event ID: ");
            int eventID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Number of Tickets: ");
            int numTickets = Convert.ToInt32(Console.ReadLine());

            try
            {
                eventRepo.BookTickets(eventID, numTickets);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void CancelTickets()
        {
            Console.Write("Enter Event ID: ");
            int eventID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Number of Tickets to Cancel: ");
            int numTickets = Convert.ToInt32(Console.ReadLine());

            eventRepo.CancelTickets(eventID, numTickets);
        }

        private static void GetEventDetails()
        {
            Console.Write("Enter Event ID: ");
            int eventID = Convert.ToInt32(Console.ReadLine());

            Event eventObj = eventRepo.GetEventDetails(eventID);

            if (eventObj != null)
            {
                Console.WriteLine($"Event Name: {eventObj.EventName}");
                Console.WriteLine($"Event Date: {eventObj.EventDate}");
                Console.WriteLine($"Event Time: {eventObj.EventTime}");
                Console.WriteLine($"Available Seats: {eventObj.AvailableSeats}");
                Console.WriteLine($"Ticket Price: {eventObj.TicketPrice}");
            }
            else
            {
                Console.WriteLine("Event not found.");
            }
        }
    }
}
