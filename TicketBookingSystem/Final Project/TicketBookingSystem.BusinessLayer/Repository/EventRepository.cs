using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using TicketBookingSystem.Entity;
using TicketBookingSystem.Util;
using static TicketBookingSystem.Entity.Event;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    public class EventRepository : IEventRepository
    {
        
        public Event CreateEvent(string eventName, DateTime date, TimeSpan time, int totalSeats, decimal ticketPrice, string eventType, Venue venue)
        {
            Event eventObj = null;

            using (SqlConnection conn = GetDBConn.GetConnection())
            {
                string query = @"insert into Event (EventName, EventDate, EventTime, VenueID, TotalSeats, AvailableSeats, TicketPrice, EventType) 
                                 values (@EventName, @EventDate, @EventTime, @VenueID, @TotalSeats, @AvailableSeats, @TicketPrice, @EventType);
                                 select SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EventName", eventName);
                cmd.Parameters.AddWithValue("@EventDate", date);
                cmd.Parameters.AddWithValue("@EventTime", time);
                cmd.Parameters.AddWithValue("@VenueID", venue.VenueID);
                cmd.Parameters.AddWithValue("@TotalSeats", totalSeats);
                cmd.Parameters.AddWithValue("@AvailableSeats", totalSeats); 
                cmd.Parameters.AddWithValue("@TicketPrice", ticketPrice);
                cmd.Parameters.AddWithValue("@EventType", eventType);

                conn.Open();
                int eventID = Convert.ToInt32(cmd.ExecuteScalar());

                eventObj = new Event()
                {
                    EventID = eventID,
                    EventName = eventName,
                    EventDate = date,
                    EventTime = time,
                    Venue = venue,
                    TotalSeats = totalSeats,
                    AvailableSeats = totalSeats,
                    TicketPrice = ticketPrice,
                    Type = (Event.EventType)Enum.Parse(typeof(Event.EventType), eventType)
                };

                Console.WriteLine("Event Created Successfully with ID: " + eventID);
            }

            return eventObj;
        }

        
        public Event GetEventDetails(int eventID)
        {
            Event eventObj = null;

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
                }
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

                            eventList.Add(eventObj);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new System.Exception("An error occurred while retrieving events by type: " + sqlEx.Message);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("An unexpected error occurred: " + ex.Message);
            }

            return eventList;
        }

        public void BookTickets(int eventID, int numTickets)
        {
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


        public void CancelTickets(int eventID, int numTickets)
        {
            using (SqlConnection conn = GetDBConn.GetConnection())
            {
                
                string query = "UPDATE Event SET AvailableSeats = AvailableSeats + @NumTickets WHERE EventID = @EventID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NumTickets", numTickets);
                cmd.Parameters.AddWithValue("@EventID", eventID);
                cmd.ExecuteNonQuery();
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
