using System;
using TicketBookingSystem.Entity;


namespace TicketBookingSystem.BusinessLayer.Repository
{
    public class CustomerRepository : ICustomerRepository
    { 
        public void DisplayCustomerDetails(Customer customer)
        {
            Console.WriteLine("========== Displaying Customer Details ==========");
            Console.WriteLine($"Customer Name: {customer.CustomerName}\nCustomer Email: {customer.Email}\nCustomer Phone-No: {customer.PhoneNumber}");
        }
    }
}
