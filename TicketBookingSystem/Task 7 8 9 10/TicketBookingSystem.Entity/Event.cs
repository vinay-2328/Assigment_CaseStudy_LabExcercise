using System;


namespace TicketBookingSystem.Entity
{
    public class Event
    {
        public int EventID {  get; set; }
        public String EventName { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set; }
        public Venue Venue { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public Decimal TicketPrice { get; set; }
        public EventType Type { get; set; }
        public enum EventType
        {
            Movie,
            Sports,
            Concert
        }
    }
}