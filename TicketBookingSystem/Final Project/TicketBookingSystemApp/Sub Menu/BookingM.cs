using System;
using System.Collections.Generic;
using System.Linq;
using TicketBookingSystem.BusinessLayer;
using TicketBookingSystem.BusinessLayer.Services;
using TicketBookingSystem.Entity;
using TicketBookingSystem.Util;

namespace TicketBookingSystemApp.Sub_Menu
{
     class BookingM
    {

        private readonly IBookingServices _bookingServices = new BookingServices();
        private readonly EventServices _eventServices = new EventServices();
        public void BookTicket(Customer customer)
        {
            Console.WriteLine("");
        }

        public void ViewAllBooking(Customer customer)
        {
            Console.Clear();
            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("==============================================================================================");
            Console.WriteLine("                                         Customer Bookings                                      ");
            Console.WriteLine("==============================================================================================");
            ConsoleColorHelper.ResetColor();

            IEnumerable<Booking> bookings = _bookingServices.GetAllBooking(customer.CustomerID);

            if (!bookings.Any())
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine("No bookings found for this customer.");
                ConsoleColorHelper.ResetColor();
                return;
            }

            ConsoleColorHelper.SetSubheadingColor();
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"Booking ID",-10} {"Event Name",-30} {"Event Date",-12} {"Event Time",-15} {"Num Tickets",-12} {"Total Cost",-12} {"Booking Date",-12}");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
            ConsoleColorHelper.ResetColor();

            foreach (var booking in bookings)
            {
                ConsoleColorHelper.SetInfoColor();
                Console.WriteLine(
                    $"{booking.BookingId,-10} " +
                    $"{booking.Event.EventName,-30} " +
                    $"{booking.Event.EventDate.ToString("dd-MM-yyyy"),-12} " +
                    $"{booking.Event.EventTime.ToString(@"hh\:mm"),-15} " +
                    $"{booking.NumTickets,-12} " +
                    $"{booking.TotalCost,-12}" + 
                    $"{booking.BookingDate.ToString("dd-MM-yyyy"),-12}"
                );
                ConsoleColorHelper.ResetColor();
            }

            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"Total Bookings: {bookings.Count()}");
            ConsoleColorHelper.ResetColor();

            Console.WriteLine("\nPress Enter to go back to the menu...");
            Console.ReadKey();
            Console.Clear();
        }


        public void BookTickets(Customer customer)
        {
            int choice = 0;
            while (choice != 4)
            {
                Console.Clear();
                ConsoleColorHelper.SetHeadingColor();
                Console.WriteLine("===============================================================");
                Console.WriteLine("                          BOOKING                                ");
                Console.WriteLine("===============================================================");

                Console.WriteLine("Select the Event type");
                Console.WriteLine("1. Movie");
                Console.WriteLine("2. Concert");
                Console.WriteLine("3. Sports");
                Console.WriteLine("4. Exit");
                Console.WriteLine("----------------------------------------------------------------");
                ConsoleColorHelper.ResetColor();

                ConsoleColorHelper.SetSubheadingColor();
                Console.Write("Enter your choice: ");
                ConsoleColorHelper.ResetColor();
                
                choice = Convert.ToInt32(Console.ReadLine());
                

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        ConsoleColorHelper.SetHeadingColor();
                        Console.WriteLine("================================================================");
                        Console.WriteLine("                     AVAILABLE MOVIE EVENTS                     ");
                        Console.WriteLine("================================================================");
                        ConsoleColorHelper.ResetColor();

                        IEnumerable<Event> events = _eventServices.GetEventDetailByEventType(Event.EventType.Movie);

                        if (!events.Any())
                        {
                            ConsoleColorHelper.SetErrorColor();
                            Console.WriteLine("No movie events found.");
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

                        ConsoleColorHelper.SetSubheadingColor();
                        Console.WriteLine("\nSelect the Event for booking tickets");
                        ConsoleColorHelper.ResetColor();

                        Console.Write("Enter the Event Id: ");
                        int eventID = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter the Number of Tickets to book: ");
                        int noOfTickets = Convert.ToInt32(Console.ReadLine());

                        var customers = new[] { customer };

                        Event selectedEvent = _eventServices.GetEventDetails(eventID);
                        
                        _bookingServices.CreateBooking(selectedEvent, customers, noOfTickets); 

                        Console.WriteLine("\nPress Enter to go back to the menu");
                        Console.ReadKey();
                        Console.Clear();

                        break;

                    case 2:

                        Console.Clear();
                        ConsoleColorHelper.SetHeadingColor();
                        Console.WriteLine("=================================================================");
                        Console.WriteLine("                    AVAILABLE CONCERTS EVENTS                    ");
                        Console.WriteLine("=================================================================");
                        ConsoleColorHelper.ResetColor();

                        IEnumerable<Event> concertEvents = _eventServices.GetEventDetailByEventType(Event.EventType.Concert);

                        if (!concertEvents.Any())
                        {
                            ConsoleColorHelper.SetErrorColor();
                            Console.WriteLine("No Concert events found.");
                            ConsoleColorHelper.ResetColor();
                            return;
                        }

                        ConsoleColorHelper.SetSubheadingColor();
                        Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine($"{"Event ID",-10} {"Event Name",-30} {"Date",-12} {"Time",-8} {"Venue",-30} {"Seats",-15} {"Price",-10} {"Type",-10}");
                        Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                        ConsoleColorHelper.ResetColor();

                        foreach (var eventObj in concertEvents)
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
                        Console.WriteLine($"Total Events: {concertEvents.Count()}");
                        ConsoleColorHelper.ResetColor();

                        ConsoleColorHelper.SetSubheadingColor();
                        Console.WriteLine("\nSelect the Event for booking tickets");
                        ConsoleColorHelper.ResetColor();

                        Console.Write("Enter the Event Id: ");
                        int concertEventID = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter the Number of Tickets to book: ");
                        int concertNoOfTickets = Convert.ToInt32(Console.ReadLine());

                        Event selectedConcertEvent = _eventServices.GetEventDetails(concertEventID);
                        var concertCustomers = new[] { customer };
                        _bookingServices.CreateBooking(selectedConcertEvent, concertCustomers, concertNoOfTickets);

                        Console.WriteLine("\nPress Enter to go back to the menu");
                        Console.ReadKey();
                        Console.Clear();

                        break;

                    case 3:


                        Console.Clear();
                        ConsoleColorHelper.SetHeadingColor();
                        Console.WriteLine("=================================================================");
                        Console.WriteLine("                    AVAILABLE SPORTS EVENTS                    ");
                        Console.WriteLine("=================================================================");
                        ConsoleColorHelper.ResetColor();

                        IEnumerable<Event> sportEvents = _eventServices.GetEventDetailByEventType(Event.EventType.Sports);

                        if (!sportEvents.Any())
                        {
                            ConsoleColorHelper.SetErrorColor();
                            Console.WriteLine("No Sport events found.");
                            ConsoleColorHelper.ResetColor();
                            return;
                        }

                        ConsoleColorHelper.SetSubheadingColor();
                        Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine($"{"Event ID",-10} {"Event Name",-30} {"Date",-12} {"Time",-8} {"Venue",-30} {"Seats",-15} {"Price",-10} {"Type",-10}");
                        Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                        ConsoleColorHelper.ResetColor();

                        foreach (var eventObj in sportEvents)
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
                        Console.WriteLine($"Total Events: {sportEvents.Count()}");
                        ConsoleColorHelper.ResetColor();

                        ConsoleColorHelper.SetSubheadingColor();
                        Console.WriteLine("\nSelect the Event for booking tickets");
                        ConsoleColorHelper.ResetColor();

                        Console.Write("Enter the Event Id: ");
                        int sportEventID = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter the Number of Tickets to book: ");
                        int SportNoOfTickets = Convert.ToInt32(Console.ReadLine());

                        Event selectSportEvent = _eventServices.GetEventDetails(sportEventID);
                        var sportCustomer = new[] { customer };
                        _bookingServices.CreateBooking(selectSportEvent, sportCustomer, SportNoOfTickets);

                        Console.WriteLine("\nPress Enter to go back to the menu");
                        Console.ReadKey();
                        Console.Clear();

                        break;
                    case 4:
                        ConsoleColorHelper.SetSuccessColor();
                        Console.WriteLine("Press Enter to exit Booking...");
                        ConsoleColorHelper.ResetColor();
                        Console.ReadKey();
                        return;
                    default:
                        ConsoleColorHelper.SetErrorColor();
                        Console.WriteLine("Invalid Choice!");
                        ConsoleColorHelper.ResetColor();
                        Console.WriteLine("Choice from 1-4 press enter to continue...");
                        Console.ReadKey();
                        break;
                }
            }

        }

        public void CancelTickets(Customer customer)
        {
            int choice = 0;
            while (choice != 3)
            {
                Console.Clear();
                ConsoleColorHelper.SetHeadingColor();
                Console.WriteLine("==============================================================");
                Console.WriteLine("                        CANCEL BOOKING                        ");
                Console.WriteLine("==============================================================");

                Console.WriteLine("Select your choice");
                Console.WriteLine("1. Cancel Entire Booking");
                Console.WriteLine("2. Cancel some number of Booking");
                Console.WriteLine("3. Exit");
                Console.WriteLine("---------------------------------------------------------------");
                ConsoleColorHelper.ResetColor();

                ConsoleColorHelper.SetSubheadingColor();
                Console.Write("Enter your choice: ");
                ConsoleColorHelper.ResetColor();

                choice = Convert.ToInt32(Console.ReadLine());


                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        ConsoleColorHelper.SetHeadingColor();
                        Console.WriteLine("==============================================================================================");
                        Console.WriteLine("                                         Customer Bookings                                      ");
                        Console.WriteLine("==============================================================================================");
                        ConsoleColorHelper.ResetColor();

                        IEnumerable<Booking> allBookings = _bookingServices.GetAllBooking(customer.CustomerID);

                        if (!allBookings.Any())
                        {
                            ConsoleColorHelper.SetErrorColor();
                            Console.WriteLine("No bookings found for this customer.");
                            ConsoleColorHelper.ResetColor();
                            return;
                        }

                        ConsoleColorHelper.SetSubheadingColor();
                        Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine($"{"Booking ID",-10} {"Event Name",-30} {"Event Date",-12} {"Event Time",-15} {"Num Tickets",-12} {"Total Cost",-12} {"Booking Date",-12}");
                        Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                        ConsoleColorHelper.ResetColor();

                        foreach (var booking in allBookings)
                        {
                            ConsoleColorHelper.SetInfoColor();
                            Console.WriteLine(
                                $"{booking.BookingId,-10} " +
                                $"{booking.Event.EventName,-30} " +
                                $"{booking.Event.EventDate.ToString("dd-MM-yyyy"),-12} " +
                                $"{booking.Event.EventTime.ToString(@"hh\:mm"),-15} " +
                                $"{booking.NumTickets,-12} " +
                                $"{booking.TotalCost,-12} " +
                                $"{booking.BookingDate.ToString("dd-MM-yyyy"),-12}"
                            );
                            ConsoleColorHelper.ResetColor();
                        }

                        ConsoleColorHelper.SetHeadingColor();
                        Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine($"Total Bookings: {allBookings.Count()}");
                        ConsoleColorHelper.ResetColor();

                        Console.Write("Enter the Booking ID for canceling the entire booking: ");
                        int entireBookingID = Convert.ToInt32(Console.ReadLine());
                        var entireBooking = _bookingServices.GetBookingByID(entireBookingID);

                        if (entireBooking != null)
                        {
                            // Cancel the entire booking
                            _bookingServices.CancelBooking(entireBooking.NumTickets, entireBooking);
                            ConsoleColorHelper.SetSuccessColor();
                            Console.WriteLine("Entire booking canceled successfully.");
                            ConsoleColorHelper.ResetColor();
                        }
                        else
                        {
                            ConsoleColorHelper.SetErrorColor();
                            Console.WriteLine("Invalid Booking ID!");
                            ConsoleColorHelper.ResetColor();
                        }

                        Console.ReadKey();
                        break;

                    case 2:
                        Console.Clear();
                        ConsoleColorHelper.SetHeadingColor();
                        Console.WriteLine("==============================================================================================");
                        Console.WriteLine("                                         Customer Bookings                                      ");
                        Console.WriteLine("==============================================================================================");
                        ConsoleColorHelper.ResetColor();

                        IEnumerable<Booking> partialBookings = _bookingServices.GetAllBooking(customer.CustomerID);

                        if (!partialBookings.Any())
                        {
                            ConsoleColorHelper.SetErrorColor();
                            Console.WriteLine("No bookings found for this customer.");
                            ConsoleColorHelper.ResetColor();
                            return;
                        }

                        ConsoleColorHelper.SetSubheadingColor();
                        Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine($"{"Booking ID",-10} {"Event Name",-30} {"Event Date",-12} {"Event Time",-15} {"Num Tickets",-12} {"Total Cost",-12} {"Booking Date",-12}");
                        Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                        ConsoleColorHelper.ResetColor();

                        foreach (var booking in partialBookings)
                        {
                            ConsoleColorHelper.SetInfoColor();
                            Console.WriteLine(
                                $"{booking.BookingId,-10} " +
                                $"{booking.Event.EventName,-30} " +
                                $"{booking.Event.EventDate.ToString("dd-MM-yyyy"),-12} " +
                                $"{booking.Event.EventTime.ToString(@"hh\:mm"),-15} " +
                                $"{booking.NumTickets,-12} " +
                                $"{booking.TotalCost,-12}" +
                                $"{booking.BookingDate.ToString("dd-MM-yyyy"),-12}"
                            );
                            ConsoleColorHelper.ResetColor();
                        }

                        ConsoleColorHelper.SetHeadingColor();
                        Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine($"Total Bookings: {partialBookings.Count()}");
                        ConsoleColorHelper.ResetColor();

                        Console.Write("Enter the Booking ID for canceling tickets: ");
                        int partialBookingID = Convert.ToInt32(Console.ReadLine());
                        var selectedPartialBooking = _bookingServices.GetBookingByID(partialBookingID);

                        if (selectedPartialBooking != null)
                        {
                            Console.Write("Enter the number of tickets to cancel: ");
                            int numTicketsToCancel = Convert.ToInt32(Console.ReadLine());

                            if (numTicketsToCancel > 0 && numTicketsToCancel <= selectedPartialBooking.NumTickets)
                            {
                                _bookingServices.CancelBooking(numTicketsToCancel, selectedPartialBooking);
                                ConsoleColorHelper.SetSuccessColor();
                                Console.WriteLine($"{numTicketsToCancel} tickets canceled successfully.");
                                ConsoleColorHelper.ResetColor();
                            }
                            else
                            {
                                ConsoleColorHelper.SetErrorColor();
                                Console.WriteLine("Invalid number of tickets.");
                                ConsoleColorHelper.ResetColor();
                            }
                        }
                        else
                        {
                            ConsoleColorHelper.SetErrorColor();
                            Console.WriteLine("Invalid Booking ID!");
                            ConsoleColorHelper.ResetColor();
                        }

                        Console.ReadKey();
                        break;
                    case 3:
                        ConsoleColorHelper.SetSuccessColor();
                        Console.WriteLine("Press Enter to exit cancel Booking...");
                        ConsoleColorHelper.ResetColor();
                        Console.ReadKey();
                        return;
                    default:
                        ConsoleColorHelper.SetErrorColor();
                        Console.WriteLine("Invalid Choice!");
                        ConsoleColorHelper.ResetColor();
                        Console.WriteLine("Choice from 1-3 press enter to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }

}
