using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Repository
{
    internal interface ICustomerRepository
    {
        void DisplayCustomerDetails(Customer customer);
        Customer CheckUser(string email, string password);
        Customer GetCustomerById(int customerId);
        Customer AddCustomer(Customer customer);
    }
}
