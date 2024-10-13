using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketBookingSystem.Entity;
using TicketBookingSystem.Util;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly EventRepository eventRepository;
        private readonly CustomerRepository customerRepository;

        public BookingRepository()
        {
            eventRepository = new EventRepository();
            customerRepository = new CustomerRepository();
        }

        public void CreateBooking(Event eventDetails, Customer[] customers, int numTickets)
        {
            
            if (eventDetails.AvailableSeats < numTickets)
            {
                throw new System.Exception("Not enough available tickets for the selected event.");
            }

            var booking = new Booking
            {
                Event = eventDetails,
                Customers = customers,
                NumTickets = numTickets,
                TotalCost = eventDetails.TicketPrice * numTickets,
                BookingDate = DateTime.Now 
            };

            eventRepository.BookTickets(eventDetails.EventID, numTickets );
            SaveBookingToDatabase(booking);
            DisplayBookingDetails(booking);
        }

        private void SaveBookingToDatabase(Booking booking)
        {
            try
            {
                using (SqlConnection conn = GetDBConn.GetConnection())
                {
                     
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            
                            string query = @"insert into Booking (EventID, CustomerID, NumOfTickets, TotalCost, BookingDate) 
                                     values (@EventID, @CustomerID, @NumOfTickets, @TotalCost, @BookingDate);
                                     select SCOPE_IDENTITY();";

                            SqlCommand command = new SqlCommand(query, conn, transaction);

                            
                            int bookingID = 0;

                            
                            foreach (var customer in booking.Customers)
                            {
                                command.Parameters.Clear(); 

                                command.Parameters.AddWithValue("@EventID", booking.Event.EventID);
                                command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                                command.Parameters.AddWithValue("@NumOfTickets", booking.NumTickets);
                                command.Parameters.AddWithValue("@TotalCost", booking.TotalCost);
                                command.Parameters.AddWithValue("@BookingDate", booking.BookingDate);

                                
                                var result = command.ExecuteScalar();
                                if (result == null)
                                {
                                    throw new System.Exception("Booking not inserted in database.");
                                }

                                
                                bookingID = Convert.ToInt32(result);
                            }

                           
                            string queryForCustomer = "UPDATE Customer SET BookingID = @BookingID WHERE CustomerID = @CustomerID";
                            using (SqlCommand cmdForCustomer = new SqlCommand(queryForCustomer, conn, transaction))
                            {
                                foreach (Customer customer in booking.Customers)
                                {
                                    cmdForCustomer.Parameters.Clear(); 
                                    cmdForCustomer.Parameters.AddWithValue("@BookingID", bookingID);
                                    cmdForCustomer.Parameters.AddWithValue("@CustomerID", customer.CustomerID);

                                    
                                    cmdForCustomer.ExecuteNonQuery();
                                }
                            }

                            
                            transaction.Commit();
                        }
                        catch (System.Exception ex)
                        {
                            
                            transaction.Rollback();
                            throw new System.Exception("An error occurred while saving the booking: " + ex.Message);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                
                Console.WriteLine("Database error: " + ex.Message);
            }
        }

        public Booking GetBookingByID(int bookingID)
        {
            Booking booking = null;
            try
            {
                using(SqlConnection conn = GetDBConn.GetConnection())
                {
                    string query = "SELECT b.BookingID, b.EventID, b.NumOfTickets, b.TotalCost, b.BookingDate, e.EventName, e.EventDate, e.EventTime, v.VenueName FROM Booking b JOIN Event e ON b.EventID = e.EventID JOIN Venue v ON e.VenueID = v.VenueID WHERE b.BookingID = @BookingID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@BookingID", bookingID);

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            booking = new Booking()
                            {
                                BookingId = Convert.ToInt32(reader["BookingID"]),
                                Event = new Event
                                {
                                    EventID = Convert.ToInt32(reader["EventID"]),
                                    EventName = Convert.ToString(reader["EventName"]),
                                    EventDate = Convert.ToDateTime(reader["EventDate"]),
                                    EventTime = TimeSpan.Parse(reader["EventTime"].ToString()),
                                    Venue = new Venue
                                    {
                                        VenueName = Convert.ToString(reader["VenueName"])
                                    },
                                },
                                NumTickets = Convert.ToInt32(reader["NumOfTickets"]),
                                TotalCost = Convert.ToDecimal(reader["TotalCost"]),
                                BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                            };
                        }
                    }
                }
            }catch(System.Exception ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }
            return booking;
        }
        public void CancelBooking(int numTickets, Booking booking)
        {
            if (numTickets > booking.NumTickets)
            {
                throw new System.Exception("Cannot cancel more tickets than booked.");
            }

            
            if (numTickets == booking.NumTickets)
            {
                DeleteBookingFromDatabase(booking);
                Console.WriteLine($"Booking with ID {booking.BookingId} has been canceled and deleted from the system.");
            }
            else
            {
                
                eventRepository.CancelTickets(booking.Event.EventID, numTickets);

                
                booking.NumTickets -= numTickets;

                booking.TotalCost = eventRepository.GetTotalPrice(booking.Event.EventID,(booking.NumTickets));

                
                if (booking.TotalCost <= 0)
                {
                    throw new System.Exception("Total cost cannot be zero or negative after cancellation.");
                }

                
                UpdateBookingInDatabase(booking);
                Console.WriteLine($"Booking updated. Remaining Tickets: {booking.NumTickets}, Total Cost: {booking.TotalCost}");
            }
        }

        private void DeleteBookingFromDatabase(Booking booking)
        {
            try
            {
                using (SqlConnection conn = GetDBConn.GetConnection())
                {
                    string query = "DELETE FROM Booking WHERE BookingID = @BookingID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@BookingID", booking.BookingId);
                    cmd.ExecuteNonQuery();
                    eventRepository.CancelTickets(booking.Event.EventID, booking.NumTickets);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error while deleting booking from the database: " + ex.Message);
            }
        }


        private void UpdateBookingInDatabase(Booking booking)
        {
            try
            {
                using (SqlConnection conn = GetDBConn.GetConnection())
                {
                    string query = "UPDATE Booking SET NumOfTickets = @NumOfTickets, TotalCost = @TotalCost WHERE BookingID = @BookingID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NumOfTickets", booking.NumTickets);
                    cmd.Parameters.AddWithValue("@TotalCost", booking.TotalCost);
                    cmd.Parameters.AddWithValue("@BookingID", booking.BookingId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error while updating booking in the database: " + ex.Message);
            }
        }

        

        public void DisplayBookingDetails(Booking booking)
        {

            ConsoleColorHelper.SetSubheadingColor();

            Console.WriteLine($"BookingID: {booking.BookingId}\nBooking Date: {booking.BookingDate}\nEvent Name: {booking.Event.EventName}\nNumber of Tickets: {booking.NumTickets}\nTotal cost: {booking.TotalCost}");
            foreach(var customer in booking.Customers)
            {
                Console.WriteLine($"Customer name: {customer.CustomerName}, Customer Email: {customer.Email}, Customer Phone no.: {customer.PhoneNumber}");
            }
            ConsoleColorHelper.ResetColor();
        }

        public IEnumerable<Booking> GetAllBooking(int customerID)
        {
            List<Booking> bookings = new List<Booking>();

            try
            {
                using (SqlConnection conn = GetDBConn.GetConnection())
                {
                    string query = "SELECT b.BookingID, b.EventID, b.NumOfTickets, b.TotalCost, b.BookingDate, e.EventName, e.EventDate, e.EventTime, v.VenueName FROM Booking b JOIN Event e ON b.EventID = e.EventID JOIN Venue v ON e.VenueID = v.VenueID WHERE b.CustomerID = @CustomerID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Booking booking = new Booking
                            {
                                BookingId = Convert.ToInt32(reader["BookingID"]),
                                Event = new Event
                                {
                                    EventID = Convert.ToInt32(reader["EventID"]),
                                    EventName = Convert.ToString(reader["EventName"]),
                                    EventDate = Convert.ToDateTime(reader["EventDate"]),
                                    EventTime = TimeSpan.Parse(reader["EventTime"].ToString()),
                                    Venue = new Venue 
                                    {
                                        VenueName = Convert.ToString(reader["VenueName"])
                                    },
                                },
                                NumTickets = Convert.ToInt32(reader["NumOfTickets"]),
                                TotalCost = Convert.ToDecimal(reader["TotalCost"]),
                                BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                            };

                            bookings.Add(booking);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new System.Exception("An error occurred while retrieving the bookings: " + ex.Message);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("An error occurred: " + ex.Message);
            }

            return bookings;
        }

    }
}
