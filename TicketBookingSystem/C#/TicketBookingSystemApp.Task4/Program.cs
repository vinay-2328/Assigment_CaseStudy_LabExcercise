using System;
using System.Collections.Generic;
using TicketBookingSystem.BusinessLayer;
using TicketBookingSystem.Entity;
using TicketBookingSystem.BusinessLayer.SubClass;

namespace TicketBookingSystemApp.Task5
{
    public class TicketBookingSystem
    {
        private List<Event> events = new List<Event>(); // To store events
        private EventServices eventServices = new EventServices();

        // Method to create a new event
        public Event CreateEvent(
            string eventName,
            DateTime eventDate,
            TimeSpan eventTime,
            string venueName,
            int totalSeats,
            int availableSeats,
            decimal ticketPrice,
            Event.EventType type)
        {
            Event newEvent;

            switch (type)
            {
                case Event.EventType.Movie:
                    newEvent = CreateMovieEvent(eventName, eventDate, eventTime, venueName, totalSeats, availableSeats, ticketPrice);
                    break;

                case Event.EventType.Sports:
                    newEvent = CreateSportsEvent(eventName, eventDate, eventTime, venueName, totalSeats, availableSeats, ticketPrice);
                    break;

                case Event.EventType.Concert:
                    newEvent = CreateConcertEvent(eventName, eventDate, eventTime, venueName, totalSeats, availableSeats, ticketPrice);
                    break;

                default:
                    throw new ArgumentException("Invalid event type.");
            }

            // Add the newly created event to the list
            events.Add(newEvent);
            return newEvent;
        }

        // Method to create a Movie event
        private Movie CreateMovieEvent(string eventName, DateTime eventDate, TimeSpan eventTime, string venueName, int totalSeats, int availableSeats, decimal ticketPrice)
        {
            Console.Write("Enter genre: ");
            string genre = Console.ReadLine();

            Console.Write("Enter lead actor: ");
            string actor = Console.ReadLine();

            Console.Write("Enter lead actress: ");
            string actress = Console.ReadLine();

            return new Movie(eventName, eventDate, eventTime, venueName, totalSeats, availableSeats, ticketPrice, genre, actor, actress);
        }

        // Method to create a Sports event
        private Sports CreateSportsEvent(string eventName, DateTime eventDate, TimeSpan eventTime, string venueName, int totalSeats, int availableSeats, decimal ticketPrice)
        {
            Console.Write("Enter sport name: ");
            string sportName = Console.ReadLine();

            Console.Write("Enter teams: ");
            string teams = Console.ReadLine();

            return new Sports(eventName, eventDate, eventTime, venueName, totalSeats, availableSeats, ticketPrice, sportName, teams);
        }

        // Method to create a Concert event
        private Concert CreateConcertEvent(string eventName, DateTime eventDate, TimeSpan eventTime, string venueName, int totalSeats, int availableSeats, decimal ticketPrice)
        {
            Console.Write("Enter artist: ");
            string artist = Console.ReadLine();

            Console.Write("Enter type of concert: ");
            string concertType = Console.ReadLine();

            return new Concert(eventName, eventDate, eventTime, venueName, totalSeats, availableSeats, ticketPrice, artist, concertType);
        }

        public void DisplayEventDetails(Event eventObj)
        {
            if (eventObj != null)
            {
                // Call the GetEventDetails method to retrieve formatted details
                string eventDetails = eventServices.GetEventDetails(eventObj);
                Console.WriteLine(eventDetails);
            }
            else
            {
                Console.WriteLine("Event not found.");
            }
        }

        public void Main()
        {
            Console.WriteLine("Welcome to the Ticket Booking System!");

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
                        // Display event details
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

    internal class Program
    {
        static void Main(string[] args)
        {
            TicketBookingSystem ticketBookingSystem = new TicketBookingSystem();
            ticketBookingSystem.Main(); // Start the ticket booking system
        }
    }
}
