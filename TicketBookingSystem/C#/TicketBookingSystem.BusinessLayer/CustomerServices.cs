using System;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer
{
    public class CustomerServices
    {
        public Customer CreateCustomer(string customerName, string email, string phoneNumber)
        {
            return new Customer
            {
                CustomerName = customerName,
                Email = email,
                PhoneNumber = phoneNumber
            };
        }

        public void DisplayCustomerDetails(Customer customer)
        {
            Console.WriteLine($"Customer Name: {customer.CustomerName}\nCustomer Email: {customer.Email}\nCustomer Phone-No: {customer.PhoneNumber}");
        }
    }
}
