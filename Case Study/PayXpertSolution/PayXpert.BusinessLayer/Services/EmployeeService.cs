using PayXpert.BusinessLayer.Repository;
using PayXpert.Entity;
using System;
using System.Collections.Generic;


namespace PayXpert.BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        readonly IEmployeeRepository employeeRepository;

        public EmployeeService()
        {
            employeeRepository = new EmployeeRepository();
        }


        public Employee GetEmployeeById(int employeeId)
        { 
            return employeeRepository.GetEmployeeById(employeeId);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return employeeRepository.GetAllEmployees();
        }

        public bool AddEmployee(Employee employee)
        {
            return employeeRepository.AddEmployee(employee);
        }
        public bool RemoveEmployee(int employeeId)
        { 
            return employeeRepository.RemoveEmployee(employeeId);
        }
        public bool UpdateEmployee(Employee employee, int employeeID)
        {
            return employeeRepository.UpdateEmployee(employee, employeeID);
        }
    }

}
