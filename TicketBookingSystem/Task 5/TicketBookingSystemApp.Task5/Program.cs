using System;
using System.Collections.Generic;
using TicketBookingSystem.BusinessLayer;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystem.BusinessLayer.Services;
using TicketBookingSystem.Entity;

namespace TicketBookingSystemApp.Task5
{
    internal class Program
    {
        static List<Event> events = new List<Event>();

        static void Main(string[] args)
        {
            EventServices eventServices = new EventServices();
            TicketBookingSystemServices tbsServices = new TicketBookingSystemServices();

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
                        DateTime eventDate;
                        while (!DateTime.TryParse(Console.ReadLine(), out eventDate))
                        {
                            Console.Write("Invalid date. Please enter again (yyyy-mm-dd): ");
                        }

                        Console.Write("Enter event time (HH:mm): ");
                        TimeSpan eventTime;
                        while (!TimeSpan.TryParse(Console.ReadLine(), out eventTime))
                        {
                            Console.Write("Invalid time. Please enter again (HH:mm): ");
                        }

                        Console.Write("Enter venue name: ");
                        string venueName = Console.ReadLine();


                        Console.Write("Enter total seats: ");
                        int totalSeats;

                        while (!int.TryParse(Console.ReadLine(), out totalSeats) || totalSeats <= 0)
                        {
                            Console.Write("Invalid total seats. Please enter a positive number: ");
                        }

                        Console.Write("Enter available seats: ");
                        int availableSeats;
                        while (!int.TryParse(Console.ReadLine(), out availableSeats) || availableSeats < 0)
                        {
                            Console.Write("Invalid available seats. Please enter a non-negative number: ");
                        }

                        Console.Write("Enter ticket price: ");
                        decimal ticketPrice;
                        while (!decimal.TryParse(Console.ReadLine(), out ticketPrice) || ticketPrice <= 0)
                        {
                            Console.Write("Invalid price. Please enter a positive number: ");
                        }

                        Console.Write("Enter event type (movie, sport, concert): ");
                        string eventTypeStr = Console.ReadLine();
                        Event.EventType eventType;
                        if (!Enum.TryParse(eventTypeStr, true, out eventType))
                        {
                            Console.WriteLine("Invalid event type. Please use 'movie', 'sport', or 'concert'.");
                            break;
                        }
                        //input for addition info
                        object[] additionalInfo = null;

                        // Get additional information based on event type
                        switch (eventType)
                        {
                            case Event.EventType.Movie:
                                additionalInfo = new object[3];
                                Console.Write("Enter genre: ");
                                additionalInfo[0] = Console.ReadLine();
                                Console.Write("Enter actor: ");
                                additionalInfo[1] = Console.ReadLine();
                                Console.Write("Enter actress: ");
                                additionalInfo[2] = Console.ReadLine();
                                break;

                            case Event.EventType.Concert:
                                additionalInfo = new object[2];
                                Console.Write("Enter artist: ");
                                additionalInfo[0] = Console.ReadLine();
                                Console.Write("Enter concert type: ");
                                additionalInfo[1] = Console.ReadLine();
                                break;

                            case Event.EventType.Sports:
                                additionalInfo = new object[2];
                                Console.Write("Enter sport name: ");
                                additionalInfo[0] = Console.ReadLine();
                                Console.Write("Enter teams participating: ");
                                additionalInfo[1] = Console.ReadLine();
                                break;
                        }



                                // Parse the event type
                                if (!Enum.TryParse(eventTypeStr, true, out eventType))
                        {
                            Console.WriteLine("Invalid event type. Please use 'movie', 'sport', or 'concert'.");
                            break;
                        }

                        // Create the event and adding it to the list
                        Event createdEvent = tbsServices.createEvent(name, eventDate, eventTime, venueName, totalSeats, availableSeats, ticketPrice, eventType,additionalInfo);
                        // Storing the created event
                        events.Add(createdEvent);
                        Console.WriteLine("Event created successfully.");
                        break;

                    case "2":
                        Console.Write("Enter event name to view details: ");
                        string eventNameToDisplay = Console.ReadLine();
                        Console.WriteLine();
                        Event eventToDisplay = events.Find(e => e.EventName.Equals(eventNameToDisplay, StringComparison.OrdinalIgnoreCase));

                        if (eventToDisplay != null)
                        {
                            tbsServices.DisplayEventDetails(eventToDisplay);
                        }
                        else
                        {
                            Console.WriteLine("Event not found.");
                        }
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
