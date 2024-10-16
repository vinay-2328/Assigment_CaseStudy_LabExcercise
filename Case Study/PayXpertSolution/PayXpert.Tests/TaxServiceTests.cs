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
    internal class TaxServiceTests
    {
        private Mock<ITaxRepository> _mockTaxRepository;
        private ITaxService _taxService;

        [SetUp]
        public void SetUp()
        {
            _mockTaxRepository = new Mock<ITaxRepository>();
            _taxService = new TaxService(_mockTaxRepository.Object);
        }

        [Test]
        public void CalculateTax_Should_Return_Decimal_TaxAmount()
        {
            //Arrange
            var tax = new Tax()
            {
                EmployeeID = 1,
                TaxYear = 2024,
                TaxAmount = 120000
            };

            _mockTaxRepository.Setup(repo=>repo.CalculateTax(tax.EmployeeID, tax.TaxYear)).Returns(tax.TaxAmount);

            //Act
            var result = _taxService.CalculateTax(tax.EmployeeID, tax.TaxYear);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(120000, result);
        }

        [Test]
        public void GetTaxById_Should_Return_Tax_When_Exists()
        {
            //Arrange
            int taxId = 1;
            var tax = new Tax()
            {
                TaxID = taxId,
                EmployeeID = 1,
                TaxYear = 2024,
                TaxAmount = 120000,
                TaxableIncome = 12000
            };
            _mockTaxRepository.Setup(repo=>repo.GetTaxById(taxId)).Returns(tax);

            //Act
            var result = _taxService.GetTaxById(taxId);

            //Assert
            Assert.IsNotNull(result);  //testing result is not null
            Assert.IsInstanceOf(typeof(Tax), result);  //testing the type of result
        }


        [Test]
        public void GetTaxById_Should_Return_Null_When_Tax_Not_Exists()
        {
            //Arrange
            var taxId = 111;

            _mockTaxRepository.Setup(repo => repo.GetTaxById(taxId)).Returns((Tax)null);

            //act
            var result = _taxService.GetTaxById(taxId);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void GetTaxesForEmployee_Should_Return_List_Of_Taxes_Of_Given_Employee()
        {
            //Arrange
            int employeeId = 1;
            var taxes = new List<Tax>
            {
                new Tax() { TaxID = 1, EmployeeID = employeeId, TaxYear=2024,TaxAmount=1234,TaxableIncome=0987},
                new Tax() { TaxID = 2, EmployeeID = employeeId, TaxYear=2023,TaxAmount=12345,TaxableIncome=1987},
            };
            _mockTaxRepository.Setup(repo => repo.GetTaxesForEmployee(employeeId)).Returns(taxes);

            //Act
            var result = _taxService.GetTaxesForEmployee(employeeId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsInstanceOf<IEnumerable<Tax>>(result);
        }

        [Test]
        public void GetTaxesForYear_Should_Return_List_Of_Taxes_Of_Given_Year()
        {
            //Arrange
            int taxYear = 2024;
            var taxes = new List<Tax>
            {
                new Tax() { TaxID = 1, EmployeeID = 1, TaxYear=taxYear,TaxAmount=1234,TaxableIncome=0987},
                new Tax() { TaxID = 2, EmployeeID = 2, TaxYear=taxYear,TaxAmount=12345,TaxableIncome=1987},
            };
            _mockTaxRepository.Setup(repo=>repo.GetTaxesForYear(taxYear)).Returns(taxes);

            //Act
            var result = _taxService.GetTaxesForYear(taxYear);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsInstanceOf<IEnumerable<Tax>>(result);

        }
    }
}
