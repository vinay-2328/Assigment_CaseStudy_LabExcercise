using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Entity;

namespace TicketBookingSystem.BusinessLayer.Services
{
    internal interface ICustomerServices
    {
        void DisplayCustomerDetails(Customer customer);
        Customer CheckUser(string email, string password);
        Customer GetCustomerById(int customerId);
        Customer AddCustomer(Customer customer);
    }
}
