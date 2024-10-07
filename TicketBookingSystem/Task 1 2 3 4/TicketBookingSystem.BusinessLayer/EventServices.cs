using System;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer
{
    public class EventServices
    {

        public Event CreateEvent(string eventName, DateTime eventDate, TimeSpan eventTime, string venueName, int totalSeats,int availableSeats, decimal ticketPrice, Event.EventType type)
        {
            return new Event
            {
                EventName = eventName,
                EventDate = eventDate,
                EventTime = eventTime,
                VenueName = venueName,
                TotalSeats = totalSeats,
                AvailableSeats = availableSeats,
                TicketPrice = ticketPrice,
                eventType = type
            };
        }
        public decimal CalculateTotalRevenue(Event eventObj,int bookedTickets)
        {
            return bookedTickets * eventObj.TicketPrice;
        }

        public int GetBookedNoOfTickets(Event eventObj) { 
            return eventObj.TotalSeats-eventObj.AvailableSeats;
        }

        public void BookTickets(Event eventObj, int numTickets) 
        {
            if (numTickets <= eventObj.AvailableSeats) 
            {
                eventObj.AvailableSeats -=numTickets;
                Console.WriteLine($"{numTickets} tickets booked successfully for {eventObj.EventName}. Remaining tickets: {eventObj.AvailableSeats}");
            }
            else
            {
                throw new Exception("Not enough tickets available");
            }
        }

        public void CancelBooking(Event eventObj, int numTickets)
        {
            eventObj.AvailableSeats += numTickets;
            Console.WriteLine($"{numTickets} Tickets cancelled. Updated available tickets are: {eventObj.AvailableSeats}"); 
        }

        public void GetEventDetails(Event eventObj) {
            Console.WriteLine($"Event Name: {eventObj.EventName}\nEvent Date: {eventObj.EventDate}\nEvent Time: {eventObj.EventTime}\nVenue Name: {eventObj.VenueName}\nTotal number of Seats: {eventObj.TotalSeats}\nAvailable Seats: {eventObj.AvailableSeats}\nPrice of Ticket: {eventObj.TicketPrice}\nType of the Event: {eventObj.eventType}");
        }
    }

}


