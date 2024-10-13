using System;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Services
{
    public class CustomerServices : ICustomerServices
    {
        readonly CustomerRepository repository;
        public CustomerServices()
        {
            repository = new CustomerRepository();
        }
        public void DisplayCustomerDetails(Customer customer)
        {
            repository.DisplayCustomerDetails(customer);   
        }

        public Customer CheckUser(string email, string password)
        {
            return repository.CheckUser(email, password);
        }

        public Customer GetCustomerById(int customerId)
        {
            return repository.GetCustomerById(customerId);
        }

        public Customer AddCustomer(Customer customer)
        {
            return repository.AddCustomer(customer);
        }
    }
}
