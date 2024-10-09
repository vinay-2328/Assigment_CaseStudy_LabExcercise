using PayXpert.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.BusinessLayer.Services
{
    internal interface IEmployeeService
    {
        Employee GetEmployeeById(int employeeId);
        IEnumerable<Employee> GetAllEmployees();
        void AddEmployee(Employee employee);
        void RemoveEmployee(int employeeId); 
        void UpdateEmployee(Employee employee);

    }
}
