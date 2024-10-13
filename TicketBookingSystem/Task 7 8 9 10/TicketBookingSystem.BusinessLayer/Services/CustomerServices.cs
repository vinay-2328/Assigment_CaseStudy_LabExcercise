using System;
using TicketBookingSystem.BusinessLayer.Repository;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Services
{
    internal class CustomerServices : ICustomerServices
    {
        readonly CustomerRepository repository;
        public void DisplayCustomerDetails(Customer customer)
        {
            repository.DisplayCustomerDetails(customer);   
        }
    }
}
