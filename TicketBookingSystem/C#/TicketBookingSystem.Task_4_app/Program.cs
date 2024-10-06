using System;
using TicketBookingSystem.BusinessLayer;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.Task_4_App
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("\n-------------------THIS IS TASK 4-------------------\n");
            //intializing the services 
            EventServices eventServices = new EventServices();

            //creating the event
            Event Concert = eventServices.CreateEvent("Coldplay Live", new DateTime(2024, 10, 6), new TimeSpan(18, 30, 00), "Balewadi stadium", 1000, 1000, 1500M, Event.EventType.Concert);

            //booking 100 tickets
            Console.WriteLine("---------------1. BOOKING TICKETS USING EVENT SERVICES---------------");
            eventServices.BookTickets(Concert, 100);

            //calculating total revenue 
            Console.WriteLine("\n---------------2. CALCULATING TOTAL RVENUE USING EVENT SERVICES---------------");
            decimal totalRevenue = eventServices.CalculateTotalRevenue(Concert,eventServices.GetBookedNoOfTickets(Concert));
            Console.WriteLine($"Total Revenue of Concert: {totalRevenue}");

            //Display event Details
            Console.WriteLine("\n---------------3. DISPLAYING EVENT DETAILS USING EVENT SERVICES---------------");
            Console.WriteLine(eventServices.GetEventDetails(Concert));

            //display venue details
            Console.WriteLine("\n---------------4. DISPLAYING VENUE DETAILS USING VENUE SERVICES---------------");
            VenueServices venueServices = new VenueServices();
            Venue concertVenue = venueServices.CreateVenue("Balewadi Stadium", "Pune, Maharashtra");
            venueServices.DisplayVenueDetails(concertVenue);


            //display customer details
            Console.WriteLine("\n---------------5. DISPLAYING CUSTOMER DETAILS USING CUSTOMER SERVICES---------------");
            CustomerServices customerServices = new CustomerServices();
            Customer vinay = customerServices.CreateCustomer("Vinay Solanki", "vinay@gmail.com", "1234567890");
            customerServices.DisplayCustomerDetails(vinay);

            //calculating booking cost
            Console.WriteLine("\n---------------6. BOOKING TICKETS USING BOOKING SERVICES---------------");
            BookingServices bookingServices = new BookingServices();
            Booking booking = bookingServices.CreateBooking(vinay, Concert,50,eventServices);

            //calculating booking cost
            Console.WriteLine("\n---------------7. CALCULATING TOTAL BOOKING COST USING BOOKING SERVICES---------------");
            Console.WriteLine("Total booking cost: {0}",bookingServices.CalculateBookingCost(Concert.TicketPrice, 50));

            //cancel booking
            Console.WriteLine("\n---------------8. CANCEL BOOKING TICKETS USING BOOKING SERVICES---------------");
            bookingServices.CancelBooking(booking,10,eventServices);

            Console.ReadKey();




        }
    }
}
