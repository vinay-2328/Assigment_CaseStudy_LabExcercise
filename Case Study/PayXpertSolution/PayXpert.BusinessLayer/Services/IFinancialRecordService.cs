using PayXpert.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.BusinessLayer.Services
{
    internal interface IFinancialRecordService
    {
        void AddFinancialRecord(FinancialRecord financialRecord);
        FinancialRecord GetFinancialRecordById(int recordId);
        IEnumerable<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId);
        IEnumerable<FinancialRecord> GetFinancialRecordForDate(DateTime recordDate);
    }
}
