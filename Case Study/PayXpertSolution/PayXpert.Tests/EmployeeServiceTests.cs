using Moq;
using NUnit.Framework;
using PayXpert.BusinessLayer.Repository;
using PayXpert.BusinessLayer.Services;
using PayXpert.Entity;
using PayXpert.Exception;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayXpert.Tests
{
    [TestFixture]
    internal class EmployeeServiceTests
    {
        private Mock<IEmployeeRepository> _mockEmployeeRepository;
        private IEmployeeService _employeeService;

        [SetUp]
        public void SetUp()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();

            _employeeService = new EmployeeService(_mockEmployeeRepository.Object);

        }

        [Test]
        public void GetEmployeeById_Should_Return_Employee_When_Employee_Exists()
        {
            //Arrange
            var employeeId = 1;
            var employee = new Employee
            {
                EmployeeID = employeeId,
                FirstName = "Vinay",
                LastName = "Solanki",
                DateOfBirth = new DateTime(2002, 09, 28),
                Gender = 'M',
                Email = "vinay@gmail.com",
                PhoneNumber = "1234567890",
                Address = "Pune",
                Position = "Software Developer",
                JoiningDate = new DateTime(2024, 10, 16),
                TerminationDate = null
            };

            _mockEmployeeRepository.Setup(repo => repo.GetEmployeeById(employeeId)).Returns(employee);

            //act
            var result = _employeeService.GetEmployeeById(employeeId);

            //Assert 

            Assert.IsNotNull(result);  //checking for result is not null
            Assert.AreEqual(employeeId, result.EmployeeID);  // testing for employeeid is matched
            Assert.AreEqual("Vinay",result.FirstName);  //testing for firstname match
        }

        [Test]
        public void GetEmployeeById_Should_return_Null_When_employee_Not_found()
        {

            //Arrange
            var employeeId = 111;
            _mockEmployeeRepository.Setup(repo => repo.GetEmployeeById(employeeId)).Returns((Employee)null);

            //Act
            var result = _employeeService.GetEmployeeById(employeeId);

            //Assert
            Assert.IsNull(result);
        }


        [Test]
        public void GetEmployeeById_Should_Throw_EmployeeNotFoundException_When_Employee_Not_Found()
        {
            // Arrange
            var employeeId = 111;

            // Mock the repository to throw an EmployeeNotFoundException
            _mockEmployeeRepository
                .Setup(repo => repo.GetEmployeeById(employeeId))
                .Throws(new EmployeeNotFoundException($"Employee with ID {employeeId} not found."));

            // Act and Assert
            var exception = Assert.Throws<EmployeeNotFoundException>(() => _employeeService.GetEmployeeById(employeeId));

            // Optionally, check the exception message if necessary
            Assert.AreEqual($"Employee with ID {employeeId} not found.", exception.Message);
        }


        [Test]
        public void GetAllEmployees_Should_return_List_Of_Employee()
        {
            //Arrange
            var employees = new List<Employee>
            {
                new Employee { EmployeeID = 1, FirstName = "Vinay", LastName = "Solanki" },
                new Employee { EmployeeID = 2, FirstName = "Mrunali", LastName = "Rajkule" }
            };
            _mockEmployeeRepository.Setup(repo=> repo.GetAllEmployees()).Returns(employees);

            //Act
            var result = _employeeService.GetAllEmployees();

            //Assert
            Assert.IsNotNull(result); //testing for result is not null
            Assert.AreEqual(2, result.Count()); //testing for count of the records

        }


        [Test]
        public void AddEmployee_Should_Return_True_When_Employee_is_Added()
        {
            //Setup
            var employee = new Employee
            {
                FirstName = "Vinay",
                LastName = "Solanki"
            };

            _mockEmployeeRepository.Setup(repo => repo.AddEmployee(employee)).Returns(true);
            
            //Act
            var result = _employeeService.AddEmployee(employee);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void RemoveEmployee_Should_Return_True_When_Employee_Is_Removed()
        {
            // Arrange
            var employeeId = 1;

            _mockEmployeeRepository.Setup(repo => repo.RemoveEmployee(employeeId)).Returns(true);

            // Act
            var result = _employeeService.RemoveEmployee(employeeId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void RemoveEmployee_Should_Return_False_When_Employee_Not_Found()
        {
            // Arrange
            var employeeId = 999;

            _mockEmployeeRepository.Setup(repo => repo.RemoveEmployee(employeeId)).Returns(false);

            // Act
            var result = _employeeService.RemoveEmployee(employeeId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void UpdateEmployee_Should_Return_True_When_Employee_Is_Updated()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeID = 1,
                FirstName = "UpdatedName"
            };

            _mockEmployeeRepository.Setup(repo => repo.UpdateEmployee(employee, employee.EmployeeID)).Returns(true);

            // Act
            var result = _employeeService.UpdateEmployee(employee, employee.EmployeeID);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateEmployee_Should_Return_False_When_No_Employee_Updated()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeID = 999,
                FirstName = "UpdatedName"
            };

            _mockEmployeeRepository.Setup(repo => repo.UpdateEmployee(employee, employee.EmployeeID)).Returns(false);

            // Act
            var result = _employeeService.UpdateEmployee(employee, employee.EmployeeID);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
