using Moq;
using NUnit.Framework;
using PayXpert.BusinessLayer.Repository;
using PayXpert.BusinessLayer.Services;
using PayXpert.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Tests
{
    [TestFixture]
    internal class PayrollServiceTests
    {
        private Mock<IPayrollRepository> _mockPayrollRepository;
        private IPayrollService _payrollService;

        [SetUp]
        public void SetUp()
        {
            _mockPayrollRepository = new Mock<IPayrollRepository>();
            _payrollService = new PayrollService(_mockPayrollRepository.Object);
        }

        [Test]
        public void GetPayrollById_Should_Return_Payroll_If_Exists()
        {
            //Arrange
            int payrollId = 1;
            var payroll = new Payroll()
            {
                PayrollID = payrollId,
                PayPeriodStartDate = DateTime.Now,
                PayPeriodEndDate = DateTime.Now,
                BasicSalary = 1234,
                Deductions = 10,
                OvertimePay = 12222
            };

            _mockPayrollRepository.Setup(repo=>repo.GetPayrollById(payrollId)).Returns(payroll);

            //Act
            var result = _payrollService.GetPayrollById(payrollId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1,result.PayrollID);
            Assert.IsInstanceOf(typeof(Payroll), result);
        }


        [Test]
        public void GetPayrollForEmployee_Should_Return_List_Of_Payrolls_For_EmployeeID()
        {
            //Arrange
            int employeeID = 1;
            var payrolls = new List<Payroll>()
            {
                new Payroll(){ PayrollID = 1, EmployeeID=employeeID ,PayPeriodStartDate = DateTime.Now, PayPeriodEndDate = DateTime.Now, BasicSalary = 1234, Deductions = 10, OvertimePay = 12222},
                new Payroll(){ PayrollID = 2, EmployeeID=employeeID ,PayPeriodStartDate = DateTime.Now, PayPeriodEndDate = DateTime.Now, BasicSalary = 2344, Deductions = 12, OvertimePay = 21231}
            };

            _mockPayrollRepository.Setup(repo=>repo.GetPayrollsForEmployee(employeeID)).Returns(payrolls);
            
            //Act
            var result = _payrollService.GetPayrollsForEmployee(employeeID);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }


        [Test]
        public void GeneratePayroll_Should_Return_True_For_Successfull_Insert_In_Database()
        {
            //Arrange
            var payroll = new Payroll()
            {
                PayrollID = 1,
                PayPeriodStartDate = DateTime.Now,
                PayPeriodEndDate = DateTime.Now,
                BasicSalary = 1234,
                Deductions = 10,
                OvertimePay = 12222
            };

            _mockPayrollRepository.Setup(repo=>repo.GeneratePayroll(payroll)).Returns(true);

            //Act
            var result = _payrollService.GeneratePayroll(payroll);

            //Assert
            Assert.IsTrue(result);
        }


        [Test]
        public void GetPayrollForPeriods_Should_Return_List_Of_Payroll_Which_Is_Between_Given_Periods()
        {
            //Arrange
            var startDate = new DateTime(2024, 09, 01);
            var endDate = new DateTime(2024, 10, 01);

            var payrolls = new List<Payroll>()
            {
                new Payroll(){ PayrollID = 1, EmployeeID=1 ,PayPeriodStartDate = new DateTime(2024,09,11), PayPeriodEndDate = new DateTime(2024,09,20), BasicSalary = 1234, Deductions = 10, OvertimePay = 12222},
                new Payroll(){ PayrollID = 2, EmployeeID=2 ,PayPeriodStartDate =new DateTime(2024,09,21), PayPeriodEndDate = new DateTime(2024,09,30), BasicSalary = 2344, Deductions = 12, OvertimePay = 21231}
            };
            _mockPayrollRepository.Setup(repo=>repo.GetPayrollsForPeriod(startDate, endDate)).Returns(payrolls);

            //Act
            var result = _payrollService.GetPayrollsForPeriod(startDate,endDate);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2,result.Count());  
        }
    }
}
