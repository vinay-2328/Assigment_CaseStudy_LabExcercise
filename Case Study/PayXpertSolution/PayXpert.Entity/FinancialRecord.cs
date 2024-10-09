using System;

namespace PayXpert.Entity
{
    public class FinancialRecord
    {
        public int RecordID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime RecordDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string RecordType { get; set; }
    }
}
