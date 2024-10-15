using PayXpert.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.BusinessLayer.Services
{
    public interface IFinancialRecordService
    {
        bool AddFinancialRecord(FinancialRecord financialRecord);
        FinancialRecord GetFinancialRecordById(int recordId);
        IEnumerable<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId);
        IEnumerable<FinancialRecord> GetFinancialRecordForDate(DateTime recordDate);
    }
}
