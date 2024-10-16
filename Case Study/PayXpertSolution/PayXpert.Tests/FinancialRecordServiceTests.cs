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
    internal class FinancialRecordServiceTests
    {
        private Mock<IFinancialRecordRepository> _mockFinancialRecordRepository;
        private IFinancialRecordService _financialRecordService;

        [SetUp]
        public void SetUp()
        {
            _mockFinancialRecordRepository = new Mock<IFinancialRecordRepository>();
            _financialRecordService = new FinancialRecordService(_mockFinancialRecordRepository.Object);
        }

        [Test]
        public void AddFinancialRecord_Should_Return_True_If_Record_Added()
        {
            //Arrange
            var financialRecord = new FinancialRecord()
            {
                RecordID = 1,
                EmployeeID = 1,
                RecordDate = DateTime.Now,
                Description = "Test",
                Amount = 999,
                RecordType = "Income"
            };

            _mockFinancialRecordRepository.Setup(repo => repo.AddFinancialRecord(financialRecord)).Returns(true);

            //Act
            var result = _financialRecordService.AddFinancialRecord(financialRecord);

            //Assert
            Assert.IsTrue(result);
            
        }

        [Test]
        public void GetFinancialRecordById_Should_Return_FinancialRecord_If_Exists()
        {
            //Arrange
            int financialRecordId = 1;
            var financialRecord = new FinancialRecord()
            {
                RecordID = financialRecordId,
                EmployeeID = 1,
                RecordDate = DateTime.Now,
                Description = "Test",
                Amount = 999,
                RecordType = "Income"
            };

            _mockFinancialRecordRepository.Setup(repo => repo.GetFinancialRecordById(financialRecordId)).Returns(financialRecord);

            //Act
            var result = _financialRecordService.GetFinancialRecordById(financialRecordId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1,result.RecordID);
        }

        [Test]
        public void GetFinancialRecordForEmployee_Should_Return_List_Of_FinancialRecord_For_EmployeeID_If_Exists()
        {
            //Arrange
            int employeeID = 1;
            var financialRecords = new List<FinancialRecord>()
            {
                new FinancialRecord() { RecordID = 1, EmployeeID = employeeID, RecordDate = DateTime.Now, Description = "Test1", Amount = 999,RecordType = "Income"},
                new FinancialRecord(){RecordID = 2, EmployeeID = employeeID, RecordDate = DateTime.Now, Description = "Test2", Amount = 888,RecordType = "Expense"}

            };
            _mockFinancialRecordRepository.Setup(repo=>repo.GetFinancialRecordsForEmployee(employeeID)).Returns(financialRecords);

            //Act
            var result = _financialRecordService.GetFinancialRecordsForEmployee(employeeID);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<FinancialRecord>>(result);
            Assert.AreEqual(2, result.Count());

        }

        [Test]
        public void GetFinancialRecordForDate_Should_List_Of_FinancialRecord_For_Date_If_Exists()
        {
            //Arrange
            var recordDate = new DateTime(2024, 10, 10);
            var financialRecords = new List<FinancialRecord>()
            {
                new FinancialRecord() { RecordID = 1, EmployeeID = 1, RecordDate = recordDate, Description = "Test1", Amount = 999,RecordType = "Income"},
                new FinancialRecord(){RecordID = 2, EmployeeID = 2, RecordDate = recordDate, Description = "Test2", Amount = 888,RecordType = "Expense"}

            };
            _mockFinancialRecordRepository.Setup(repo=>repo.GetFinancialRecordForDate(recordDate)).Returns(financialRecords);

            //Act
            var result = _financialRecordService.GetFinancialRecordForDate(recordDate);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<FinancialRecord>>(result);
            Assert.AreEqual(2, result.Count());

        }
    }
}
