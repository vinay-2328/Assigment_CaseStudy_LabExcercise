using System;
using TicketBookingSystem.BusinessLayer;
using TicketBookingSystem.BusinessLayer.Services;
using TicketBookingSystem.Entity;
using TicketBookingSystem.Util;
using TicketBookingSystemApp.Sub_Menu;

namespace TicketBookingSystemApp
{
    internal class Menu
    {
       
        private readonly CustomerServices _customerServices = new CustomerServices();
        private readonly IEventServices _eventServices = new EventServices();
        private readonly EventM _eventM = new EventM();
        private readonly BookingM _bookingM = new BookingM();
        public Menu()
        {
            Console.Title = "Ticket Booking System";
        }
        public void MainMenu(Customer customer)
        {
            Console.Clear();
            int choice = 0;
            while(choice != 5) {
                Console.Clear();
                ConsoleColorHelper.SetHeadingColor();
                Console.WriteLine("===============================================================");
                Console.Write($"   Welcome");

                ConsoleColorHelper.SetSubheadingColor();
                Console.Write($" \"{customer.CustomerName}\" ");
                ConsoleColorHelper.ResetColor();

                ConsoleColorHelper.SetHeadingColor();
                Console.Write("to Ticket Booking System   \n");
                Console.WriteLine("===============================================================");
                
                Console.WriteLine("1. View Events");
                Console.WriteLine("2. Book Tickets");
                Console.WriteLine("3. Cancel Booking");
                Console.WriteLine("4. View my Booking");
                Console.WriteLine("5. Exit");
                Console.WriteLine("----------------------------------------------------------------");
                ConsoleColorHelper.ResetColor();

                ConsoleColorHelper.SetSubheadingColor();
                Console.Write("Enter your choice: ");
                ConsoleColorHelper.ResetColor();
                choice =Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                        case 1:
                            _eventM.ViewAllEventMenu();                   
                            break;
                        case 2:
                            _bookingM.BookTickets(customer);
                            break;
                        case 3:
                            _bookingM.CancelTickets(customer);
                            break;
                        case 4:
                            _bookingM.ViewAllBooking(customer);
                            break;
                }
            }

        }

        public void Login()
        {
            Console.Clear();
            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("===============================================================");
            Console.WriteLine("                          LOGIN                                ");
            Console.WriteLine("===============================================================");
            ConsoleColorHelper.ResetColor();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            
            Customer customer = _customerServices.CheckUser(email, password);

            if (customer != null)
            {
                ConsoleColorHelper.SetSuccessColor();
                Console.WriteLine("\nLogin successful!\n");
                Console.WriteLine("Press Enter to continue...");
                ConsoleColorHelper.ResetColor();
                Console.ReadKey();
                MainMenu(customer);



            }
            else
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine("Login failed. Please check your email and password.");
                ConsoleColorHelper.ResetColor();
                
            }
        }

        public void Register()
        {
            Customer customerRegistration = new Customer();

            Console.Clear();
            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("===============================================================");
            Console.WriteLine("                          REGISTRATION                                ");
            Console.WriteLine("===============================================================");
            ConsoleColorHelper.ResetColor();

            Console.Write("Enter Customer Name: ");
            customerRegistration.CustomerName = Console.ReadLine();
            Console.Write("Enter Email: ");
            customerRegistration.Email = Console.ReadLine();
            Console.Write("Enter Phone no.: ");
            customerRegistration.PhoneNumber = Console.ReadLine();
            Console.Write("New Password: ");
            customerRegistration.Password = Console.ReadLine();

            Customer customer = _customerServices.AddCustomer(customerRegistration);

            if(customer != null)
            {
                ConsoleColorHelper.SetSuccessColor();
                Console.WriteLine("\nRegistration successful!\n");
                Console.WriteLine("Press Enter to continue...");
                ConsoleColorHelper.ResetColor();
                Console.ReadKey();
                MainMenu(customer);
            }
            else
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine("Registration Failed");
                ConsoleColorHelper.ResetColor();
            }
        }
    }
}
