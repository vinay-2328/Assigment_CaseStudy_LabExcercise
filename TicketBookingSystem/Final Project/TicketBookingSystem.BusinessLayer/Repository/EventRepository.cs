using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using TicketBookingSystem.Entity;
using TicketBookingSystem.Exception;
using TicketBookingSystem.Util;
using static TicketBookingSystem.Entity.Event;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    public class EventRepository : IEventRepository
    {

        public Event CreateEvent(Event eventObj2)
        {

            Event eventObj = null;

            if (eventObj2 == null)
                throw new NullPointerException();

            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(eventObj2.EventName) || eventObj2.Venue == null)
                    throw new ArgumentException("Event name and venue cannot be null or empty.");

                

                using (SqlConnection conn = GetDBConn.GetConnection())
                {
                    string query = @"
            INSERT INTO Event (EventName, EventDate, EventTime, VenueID, TotalSeats, AvailableSeats, TicketPrice, EventType) 
            VALUES (@EventName, @EventDate, @EventTime, @VenueID, @TotalSeats, @AvailableSeats, @TicketPrice, @EventType);
            SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters using AddWithValue
                        cmd.Parameters.AddWithValue("@EventName", eventObj2.EventName);
                        cmd.Parameters.AddWithValue("@EventDate", eventObj2.EventDate);
                        cmd.Parameters.AddWithValue("@EventTime", eventObj2.EventTime);
                        cmd.Parameters.AddWithValue("@VenueID", eventObj2.Venue.VenueID);
                        cmd.Parameters.AddWithValue("@TotalSeats", eventObj2.TotalSeats);
                        cmd.Parameters.AddWithValue("@AvailableSeats", eventObj2.TotalSeats); // Initially available
                        cmd.Parameters.AddWithValue("@TicketPrice", eventObj2.TicketPrice);
                        cmd.Parameters.AddWithValue("@EventType", eventObj2.Type.ToString());

                        
                        int eventID = Convert.ToInt32(cmd.ExecuteScalar());

                        eventObj = new Event
                        {
                            EventID = eventID,
                            EventName = eventObj2.EventName,
                            EventDate = eventObj2.EventDate,
                            EventTime = eventObj2.EventTime,
                            Venue = eventObj2.Venue,
                            TotalSeats = eventObj2.TotalSeats,
                            AvailableSeats = eventObj2.TotalSeats,
                            TicketPrice = eventObj2.TicketPrice,
                            Type = eventObj2.Type
                        };

                        Console.WriteLine("Event Created Successfully with ID: " + eventID);
                    }
                }
            }
            catch (SqlException ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }
            catch (ArgumentException ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }
            catch (NullPointerException ex)
            {
                //customer exception catching
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }
            catch (System.Exception ex)
            {
                //if we get any unexpected
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
                
            }
            return eventObj;
        }



        public Event GetEventDetails(int eventID)
        {
            Event eventObj = null;
            try
            {
                if (eventID <= 0)
                    throw new InvalidBookingIDException(eventID);

                

                using (SqlConnection conn = GetDBConn.GetConnection())
                {
                    string query = "SELECT * FROM Event WHERE EventID = @EventID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@EventID", eventID);

                    

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            eventObj = new Event()
                            {
                                EventID = Convert.ToInt32(reader["EventID"]),
                                EventName = reader["EventName"].ToString(),
                                EventDate = Convert.ToDateTime(reader["EventDate"]),
                                EventTime = TimeSpan.Parse(reader["EventTime"].ToString()),
                                TotalSeats = Convert.ToInt32(reader["TotalSeats"]),
                                AvailableSeats = Convert.ToInt32(reader["AvailableSeats"]),
                                TicketPrice = Convert.ToDecimal(reader["TicketPrice"]),
                                Type = (Event.EventType)Enum.Parse(typeof(Event.EventType), reader["EventType"].ToString())
                            };
                        }
                        else
                        {
                            throw new EventNotFoundException(eventID);
                        }
                    }
                }

            }
            catch (InvalidBookingIDException ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }

            catch (EventNotFoundException ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }catch(System.Exception ex)
            {
                //unexpected error
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }
            
            return eventObj;
        }



        public IEnumerable<Event> GetEventDetailByEventType(EventType eventType)
        {
            List<Event> eventList = new List<Event>();

            try
            {
                using (SqlConnection conn = GetDBConn.GetConnection())
                {
                    string query = @"select e.EventID, e.EventName, e.EventDate, e.EventTime, e.TotalSeats, 
                             e.AvailableSeats, e.TicketPrice, e.EventType, v.VenueID, v.VenueName,v.Address
                             from Event e
                             join Venue v on e.VenueID = v.VenueID and e.EventType=@EventType";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@EventType", eventType.ToString()); 

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Event eventObj = new Event()
                            {
                                EventID = Convert.ToInt32(reader["EventID"]),
                                EventName = reader["EventName"].ToString(),
                                EventDate = Convert.ToDateTime(reader["EventDate"]),
                                EventTime = TimeSpan.Parse(reader["EventTime"].ToString()),
                                TotalSeats = Convert.ToInt32(reader["TotalSeats"]),
                                AvailableSeats = Convert.ToInt32(reader["AvailableSeats"]),
                                TicketPrice = Convert.ToDecimal(reader["TicketPrice"]),
                                Venue = new Venue
                                {
                                    VenueID = Convert.ToInt32(reader["VenueID"]),
                                    VenueName = reader["VenueName"].ToString(),
                                    Address = reader["Address"].ToString()
                                },
                                Type = (Event.EventType)Enum.Parse(typeof(Event.EventType), reader["EventType"].ToString())
                            };

                            if(eventObj == null)
                            {
                                throw new EventNotFoundException(eventObj.EventID);
                            }
                            else
                            {
                                eventList.Add(eventObj);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }
            catch(EventNotFoundException ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }
            catch (System.Exception ex)
            {
                //for any unexpected errors
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }

            return eventList;
        }

        public void BookTickets(int eventID, int numTickets)
        {
            try
            {
                if (eventID <= 0)
                    throw new InvalidBookingIDException(eventID);
                if (numTickets <= 0)
                    throw new ArgumentException("Number of tickets must be greater than zero.");

                using (SqlConnection conn = GetDBConn.GetConnection())
                {
                    string query = @"UPDATE Event SET AvailableSeats = AvailableSeats - @NumTickets WHERE EventID = @EventID AND AvailableSeats >= @NumTickets";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NumTickets", numTickets);
                    cmd.Parameters.AddWithValue("@EventID", eventID);

                    
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ConsoleColorHelper.SetSuccessColor();
                        Console.WriteLine($"\n{numTickets} tickets booked successfully.\n");
                        ConsoleColorHelper.ResetColor();
                    }
                    else
                    {
                        throw new System.Exception("Not enough tickets available.");
                    }
                }
            }
            catch(InvalidBookingIDException ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }
            catch(ArgumentException ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }
            catch (System.Exception ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }
        }


        public void CancelTickets(int eventID, int numTickets)
        {
            try
            {
                if (eventID <= 0)
                    throw new InvalidBookingIDException(eventID);
                if (numTickets <= 0)
                    throw new ArgumentException("Number of tickets must be greater than zero.");

                using (SqlConnection conn = GetDBConn.GetConnection())
                {
                    string query = "UPDATE Event SET AvailableSeats = AvailableSeats + @NumTickets WHERE EventID = @EventID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@NumTickets", numTickets);
                    cmd.Parameters.AddWithValue("@EventID", eventID);

                    
                    cmd.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("An error occurred while canceling tickets: " + ex.Message);
            }
        }

        public IEnumerable<Event> GetAllEvents()
        {
            List<Event> events = new List<Event>();

            try
            {
                using (SqlConnection conn = GetDBConn.GetConnection())
                {
                    string query = @"SELECT e.EventID, e.EventName, e.EventDate, e.EventTime, e.TotalSeats, 
                             e.AvailableSeats, e.TicketPrice, e.EventType, v.VenueID, v.VenueName,v.Address
                             FROM Event e
                             JOIN Venue v ON e.VenueID = v.VenueID";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Event eventObj = new Event
                            {
                                EventID = Convert.ToInt32(reader["EventID"]),
                                EventName = Convert.ToString(reader["EventName"]),
                                EventDate = Convert.ToDateTime(reader["EventDate"]),
                                EventTime = TimeSpan.Parse(Convert.ToString(reader["EventTime"])),
                                TotalSeats = Convert.ToInt32(reader["TotalSeats"]),
                                AvailableSeats = Convert.ToInt32(reader["AvailableSeats"]),
                                TicketPrice = Convert.ToDecimal(reader["TicketPrice"]),
                                Venue = new Venue
                                {
                                    VenueID = Convert.ToInt32(reader["VenueID"]),
                                    VenueName = Convert.ToString(reader["VenueName"]),
                                    Address = Convert.ToString(reader["Address"])
                                },
                                Type = (Event.EventType)Enum.Parse(typeof(Event.EventType), reader["EventType"].ToString())
                            };

                            events.Add(eventObj);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new System.Exception("An error occurred while retrieving the events: " + ex.Message);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("An error occurred: " + ex.Message);
            }

            return events;
        }

        public decimal GetTotalPrice(int eventId,int numOfTickets)
        {
            decimal totalPrice = 0;
            using(SqlConnection conn = GetDBConn.GetConnection())
            {
                string query = "select TicketPrice from Event where EventID = @EventID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EventID", eventId);

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        totalPrice = Convert.ToDecimal(reader["TicketPrice"]) * numOfTickets;
                    }
                }
            }
            return totalPrice;
        }

    }
}
