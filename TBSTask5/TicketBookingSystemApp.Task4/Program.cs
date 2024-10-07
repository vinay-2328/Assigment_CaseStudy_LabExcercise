using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TicketBookingSystem.BusinessLayer;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystem.Entity;

namespace TicketBookingSystemApp.Task4
{
    internal class Program
    {
        static List<Event> events = new List<Event>();
        static void Main(string[] args)
        {


            EventRepository eventRepository = new EventRepository();
            EventServices eventServices = new EventServices();

            Event eventObj=null;


            Console.WriteLine("**********Welcome to the Ticket Booking System!**********");

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Create Event");
                Console.WriteLine("2. Display Event Details");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        // Get event details from user
                        Console.Write("Enter event name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter event date (yyyy-mm-dd): ");
                        DateTime eventDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Enter event time (HH:mm): ");
                        TimeSpan eventTime = TimeSpan.Parse(Console.ReadLine());
                        Console.Write("Enter venue name: ");
                        string venueName = Console.ReadLine();
                        Console.Write("Enter total seats: ");
                        int totalSeats = int.Parse(Console.ReadLine());
                        Console.Write("Enter available seats: ");
                        int availableSeats = int.Parse(Console.ReadLine());
                        Console.Write("Enter ticket price: ");
                        decimal ticketPrice = decimal.Parse(Console.ReadLine());
                        Console.Write("Enter event type (movie, sport, concert): ");
                        string eventTypeStr = Console.ReadLine();
                        Event.EventType eventType;

                        // Parse the event type
                        if (!Enum.TryParse(eventTypeStr, true, out eventType))
                        {
                            Console.WriteLine("Invalid event type. Please use 'movie', 'sport', or 'concert'.");
                            break;
                        }

                        // Create the event
                        CreateEvent(name, eventDate, eventTime, venueName, totalSeats, availableSeats, ticketPrice, eventType);
                        Console.WriteLine("Event created successfully.");
                        break;

                    case "2":
                        
                        Console.Write("Enter event name to view details: ");
                        string eventNameToDisplay = Console.ReadLine();
                        Event eventToDisplay = events.Find(e => e.EventName.Equals(eventNameToDisplay, StringComparison.OrdinalIgnoreCase));
                        DisplayEventDetails(eventToDisplay);
                        break;

                    case "3":
                        Console.WriteLine("Exiting the system.");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }





        }
    }
}
