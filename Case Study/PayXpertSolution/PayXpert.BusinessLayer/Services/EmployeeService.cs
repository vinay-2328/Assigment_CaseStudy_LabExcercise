using PayXpert.BusinessLayer.Repository;
using PayXpert.Entity;
using System;
using System.Collections.Generic;


namespace PayXpert.BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();

        public Employee GetEmployeeById(int employeeId)
        { 
            return employeeRepository.GetEmployeeById(employeeId);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return employeeRepository.GetAllEmployees();
        }

        public void AddEmployee(Employee employee)
        {
            employeeRepository.AddEmployee(employee);
        }
        public void RemoveEmployee(int employeeId)
        { 
            employeeRepository.RemoveEmployee(employeeId);
        }
        public void UpdateEmployee(Employee employee)
        {
            employeeRepository.UpdateEmployee(employee);
        }
    }

}
