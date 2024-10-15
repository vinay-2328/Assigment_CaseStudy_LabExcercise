using PayXpert.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.BusinessLayer.Repository
{
    internal interface IEmployeeRepository
    {
        Employee GetEmployeeById(int employeeId);
        IEnumerable<Employee> GetAllEmployees();
        bool AddEmployee(Employee employee);
        bool RemoveEmployee(int employeeId);
        bool UpdateEmployee(Employee employee, int employeeID);
    }
}
