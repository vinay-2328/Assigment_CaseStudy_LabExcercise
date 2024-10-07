using System;

namespace TicketBookingSystem.task1_2_3
{
    internal class Program
    {

        //task 2 nested Condition statement
        internal static double CalculateTicketPrice(string ticketType,int numTickets)
        {
            double pricePerTicket = 0;

            switch (ticketType.ToLower()) 
            {
                case "silver":
                    pricePerTicket = 1000;
                    break;

                case "gold":
                    pricePerTicket = 2000;
                    break;

                case "diamond":
                    pricePerTicket = 3000;
                    break;
                default:
                    Console.WriteLine("Invalid Ticket Type");
                    return 0;
            }

            return pricePerTicket * numTickets;

        }
        static void Main(string[] args)
        {
            Console.WriteLine("\n-------------------THIS IS TASK 1 AND TASK 2 AND TASK 3-------------------\n");
            //task 1 Conditional statement
            Console.WriteLine("----------TASK 1 CONDITIONAL STATEMENT----------");
            int availableTickets = 100;
            Console.Write("Enter the number of Tickets to book: ");
            int noOfBookingTickets = Convert.ToInt32(Console.ReadLine());

            if (noOfBookingTickets <= availableTickets) 
            {
                availableTickets -= noOfBookingTickets;
                Console.WriteLine($"Tickets booked successfully! Remaining tickets: {availableTickets}");
            }
            else
            {
                Console.WriteLine($"Ticket unavailable. Only {availableTickets} tickets available.");
            }


            //task 2 nested conditional statement
            Console.WriteLine("\n----------TASK 2 NESTED CONDITIONAL STATEMENT----------");
            while (true)
            {
                
                Console.Write("Enter the Type of Ticket (gold,silver,diamond) or type 'exit' to quite: ");
                string ticketType = Console.ReadLine().ToLower();

                if (ticketType == "exit")
                {
                    break;
                }
                Console.Write("Enter the number of Tickets to calculate total price: ");
                int noOfTickets = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine($"Total cost of the {noOfTickets} is: "+CalculateTicketPrice(ticketType, noOfTickets));

            }


            //task 3 Looping
            Console.WriteLine("\n----------TASK 3 LOOPING----------");
            while (true)
            {
                Console.Write("Enter the number of tickets to book (or type 'exit' to quite): ");
                string input = Console.ReadLine();

                if(input.ToLower() == "exit")
                {
                    break;
                }

                int noBookingTickets;

                if (int.TryParse(input, out noBookingTickets))
                {
                    if (noBookingTickets <= availableTickets) 
                    {
                        availableTickets -= noBookingTickets;
                        Console.WriteLine($"Tickets booked successfully! Remaining tickets: {availableTickets}");
                    }
                    else
                    {
                        Console.WriteLine($"Ticket unavailable. Only {availableTickets} tickets available.");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid number of tickets.");
                }
            }
        }
    }
}
