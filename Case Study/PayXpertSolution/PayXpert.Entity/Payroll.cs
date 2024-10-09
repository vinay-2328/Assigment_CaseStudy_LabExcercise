using System;


namespace PayXpert.Entity
{
    public class Payroll
    {
        public int PayrollID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime PayPeriodStartDate { get; set; }    
        public DateTime PayPeriodEndDate { get; set; }
        public decimal BasicSalary { get; set; }    
        public decimal OvertimePay {  get; set; }
        public decimal Deductions {  get; set; }
        public decimal NetSalary => BasicSalary + OvertimePay - Deductions;

    }
}
