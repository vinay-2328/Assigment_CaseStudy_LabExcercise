using System;
using System.Collections.Generic;
using System.Linq;
using TicketBookingSystem.BusinessLayer;
using TicketBookingSystem.BusinessLayer.Services;
using TicketBookingSystem.Entity;
using TicketBookingSystem.Util;

namespace TicketBookingSystemApp.Sub_Menu
{
    internal class EventM
    {
        private readonly IEventServices _eventServices = new EventServices();

        public void ViewAllEventMenu()
        {
            Console.Clear();
            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("==============================================================================================");
            Console.WriteLine("                                         Available Events                                      ");
            Console.WriteLine("==============================================================================================");
            ConsoleColorHelper.ResetColor();

            IEnumerable<Event> events = _eventServices.GetAllEvents();

            if (!events.Any())
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine("No events found.");
                ConsoleColorHelper.ResetColor();
                return;
            }

           
            ConsoleColorHelper.SetSubheadingColor();
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"Event ID",-10} {"Event Name",-30} {"Date",-12} {"Time",-8} {"Venue",-30} {"Seats",-15} {"Price",-10} {"Type",-10}");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
            ConsoleColorHelper.ResetColor();

            foreach (var eventObj in events)
            {
                ConsoleColorHelper.SetInfoColor();
                Console.WriteLine(
                    $"{eventObj.EventID,-10} " +
                    $"{eventObj.EventName,-30} " +
                    $"{eventObj.EventDate.ToString("dd-MM-yyyy"),-12} " +
                    $"{eventObj.EventTime.ToString(@"hh\:mm"),-8} " +
                    $"{eventObj.Venue.VenueName,-30} " +
                    $"{eventObj.AvailableSeats}/{eventObj.TotalSeats,-10} " +
                    $"{eventObj.TicketPrice,-10} " +
                    $"{eventObj.Type,-10}"
                );
                ConsoleColorHelper.ResetColor();
            }

            ConsoleColorHelper.SetSubheadingColor();
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"Total Events: {events.Count()}");
            ConsoleColorHelper.ResetColor();
            Console.WriteLine("\nPress Enter to go back to the menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
