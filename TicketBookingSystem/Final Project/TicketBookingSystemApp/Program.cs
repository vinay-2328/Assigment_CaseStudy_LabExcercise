using System;
using TicketBookingSystem.Entity;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystemApp;
using TicketBookingSystem.Util;

namespace TicketBookingSystem
{
    public class TicketBookingSystem
    {
        private static EventRepository eventRepo = new EventRepository();

        public static void Main(string[] args)
        {
            

            Menu menu = new Menu();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                ConsoleColorHelper.SetHeadingColor();
                Console.WriteLine("===============================================================");
                Console.WriteLine("                         USER MENU                              ");
                Console.WriteLine("===============================================================");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");
                Console.WriteLine("---------------------------------------------------------------");

                ConsoleColorHelper.ResetColor();
                ConsoleColorHelper.SetSubheadingColor();
                Console.Write("Enter your choice: ");
                ConsoleColorHelper.ResetColor();

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ConsoleColorHelper.SetInfoColor();
                        Console.WriteLine("\nYou selected: Login\n");
                        ConsoleColorHelper.ResetColor();
                        menu.Login();
                        break;

                    case 2:
                        ConsoleColorHelper.SetInfoColor();
                        Console.WriteLine("\nYou selected: Register\n");
                        ConsoleColorHelper.ResetColor();
                        menu.Register();
                        break;

                    case 3:
                        exit = true;
                        ConsoleColorHelper.SetSuccessColor();
                        Console.Beep();
                        Console.WriteLine("Exiting the Application...");
                        ConsoleColorHelper.ResetColor();
                        break;

                    default:
                        ConsoleColorHelper.SetErrorColor();
                        Console.WriteLine("Invalid choice. Please try again.");
                        ConsoleColorHelper.ResetColor();
                        break;
                }
            }
        }
    }
}
