using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.BusinessLayer;
using TicketBookingSystem.Entity;

namespace TicketBookingSystemApp.Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n----------TASK 4----------\n");

            //Event methods
            Console.WriteLine("\n*****EVENT METHODS*****");
            Console.WriteLine("\n1. Displaying Event using EventServices");
            EventServices eventServices = new EventServices();
            //creating the event using Event methods
            Event coldPlay = eventServices.CreateEvent("ColdPlay", new DateTime(2024, 10, 19), new TimeSpan(11, 00, 00), "Balewadi Stadium", 1000, 1000, 2999,Event.EventType.Concert);
            //displaying Event
            eventServices.GetEventDetails(coldPlay);

            //Booking Tickets using EventServices
            Console.WriteLine("\n2. Booking Ticket using EventServices");
            eventServices.BookTickets(coldPlay,50);

            //Cancel Tickets using EventServices
            Console.WriteLine("\n3. Cancel the book tickets using EventServices");
            eventServices.CancelBooking(coldPlay, 10);

            //Calculation TotalRevenue using EventServices
            Console.WriteLine("\n4. Calculating Total Revenue using EventServices");
            Console.WriteLine("Total Revenue: {0}",eventServices.CalculateTotalRevenue(coldPlay, 40));


            //Get the number of booked Tickets
            Console.WriteLine("\n5. Number of Tickets Booked");
            Console.WriteLine("No. of tickets Book: {0}",eventServices.GetBookedNoOfTickets(coldPlay));


            //Venue methods
            //Intilizating Venue
            Console.WriteLine("\n*****VENUE METHODS*****");
            VenueServices venueServices = new VenueServices();
            Venue venue = venueServices.CreateVenue("Balewadi Stadium", "Pune");
            //displaying venue details
            Console.WriteLine("\n1. Displaying Venue Details");
            venueServices.DisplayVenueDetails(venue);


            //Customer methods
            Console.WriteLine("\n*****CUSTOMER METHODS*****");
            CustomerServices customerServices = new CustomerServices();
            //displaying customer
            Customer vinay = customerServices.CreateCustomer("Vinay Solanki", "vinay@gmail.com", "1234567890");
            Console.WriteLine("\n1. Displaying Customer Details");
            customerServices.DisplayCustomerDetails(vinay);

            //Booking methods
            Console.WriteLine("\n*****BOOKING METHODS*****");
            BookingServices bookingServices = new BookingServices();
            Console.WriteLine("\n1. Booking Ticket using BookingServices");
            Booking vinayBooking = bookingServices.CreateBooking(1,vinay,coldPlay,50,eventServices);

            //cancel tickets
            Console.WriteLine("\n2.Cancel ticket using BookingServices");
            bookingServices.CancelBooking(vinayBooking, 10,eventServices);

            //displaying booking
            Console.WriteLine("\n3. Displaying Booking details");
            bookingServices.GetEventDetails(vinayBooking);

            //get available number of tickets
            Console.WriteLine("\n4. Get Available number of Tickets using BookingServices");
            Console.WriteLine("Available number of tickets is: {0}",bookingServices.GetAvailableNoOfTickets(coldPlay));

            //caculating booking cost
            Console.WriteLine("\n5. Calculating Booking Cost using BookingServices");
            Console.WriteLine($"Total Cost of booking {(coldPlay.TotalSeats - coldPlay.AvailableSeats)} Tickets is : {bookingServices.CalculateBookingCost(coldPlay.TicketPrice, (coldPlay.TotalSeats - coldPlay.AvailableSeats))}");

            
            Console.ReadKey();
        }
    }
}
