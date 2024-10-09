using PayXpert.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PayXpert.BusinessLayer.Services
{
    internal interface IPayrollService
    {
        Payroll GetPayrollById(int payrollId);
        IEnumerable<Payroll> GetPayrollsForEmployee(int employeeId);
        void GeneratePayroll(Payroll payroll);
        IEnumerable<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate);
    }
}
