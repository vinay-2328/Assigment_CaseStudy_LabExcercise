using PayXpert.BusinessLayer.Repository;
using PayXpert.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.BusinessLayer.Services
{
    public class FinancialRecordService : IFinancialRecordService
    {
        readonly IFinancialRecordRepository repository;
        public FinancialRecordService()
        {
            repository = new FinancialRecordRepository();
        }

        //For testing Purpose
        public FinancialRecordService(IFinancialRecordRepository repository)
        {
            this.repository = repository;
        }   

        public bool AddFinancialRecord(FinancialRecord financialRecord)
        {
            return repository.AddFinancialRecord(financialRecord);
        }
        public FinancialRecord GetFinancialRecordById(int recordId)
        {
            return repository.GetFinancialRecordById(recordId);
        }
        public IEnumerable<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId)
        {
            return repository.GetFinancialRecordsForEmployee(employeeId);
        }
        public IEnumerable<FinancialRecord> GetFinancialRecordForDate(DateTime recordDate)
        {
            return repository.GetFinancialRecordForDate(recordDate);
        }
    }
}
