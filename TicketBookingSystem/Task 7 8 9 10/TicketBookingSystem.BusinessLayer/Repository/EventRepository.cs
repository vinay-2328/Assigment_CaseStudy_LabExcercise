using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TicketBookingSystem.Entity;
using TicketBookingSystem.Util;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    public class EventRepository : IEventRepository
    {
        public virtual void DisplayEventDetails(Event eventObj)
        {
            List<Event> events = new List<Event>();

            using(SqlConnection conn = GetDBConn.GetConnection())
            {
                string query = "select * from Event";
                SqlCommand cmd = new SqlCommand(query, conn);

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        list.Add({
                            EventID = Convert.ToInt64(reader["EventID"]);
                        })
                    }
                }
            }
            Console.WriteLine($"Event Name: {eventObj.EventName}\nEvent Date: {eventObj.EventDate}\nEvent Time: {eventObj.EventTime}\nVenue Name: {eventObj.Venue.VenueName}\nTotal number of Seats: {eventObj.TotalSeats}\nAvailable Seats: {eventObj.AvailableSeats}\nPrice of Ticket: {eventObj.TicketPrice}\nType of the Event: {eventObj.Type}");
        }


        public decimal CalculateTotalRevenue(int bookedTickets,Event eventObj)
        {
            return bookedTickets * eventObj.TicketPrice;
        }


        public int GetBookedNoOfTickets(Event eventObj)
        {
            return eventObj.TotalSeats - eventObj.AvailableSeats;
        }


        public virtual void BookTickets(int numTickets, Event eventObj)
        {
            if (numTickets <= eventObj.AvailableSeats)
            {
                eventObj.AvailableSeats -= numTickets;
                Console.WriteLine($"{numTickets} tickets booked successfully for {eventObj.EventName}. Remaining tickets: {eventObj.AvailableSeats}");
            }
            else
            {
                throw new Exception("Not enough tickets available");
            }
        }
        public virtual void CancelTickets(int numTickets, Event eventObj)
        {
            if(numTickets <= eventObj.AvailableSeats)
            {
                eventObj.AvailableSeats += numTickets;
                Console.WriteLine($"{numTickets} Tickets cancelled. Updated available tickets are: {eventObj.AvailableSeats}");
            }
            else
            {
                throw new Exception("wrong entry of number of tickets");
            }
        }
    }
}
