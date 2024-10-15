using PayXpert.BusinessLayer.Repository;
using PayXpert.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.BusinessLayer.Services
{
    public class PayrollService : IPayrollService
    {
        readonly IPayrollRepository payrollRepository;
        public PayrollService() 
        {
            payrollRepository = new PayrollRepository();
        }
        public Payroll GetPayrollById(int payrollId)
        {
            return payrollRepository.GetPayrollById(payrollId);
        }
        public IEnumerable<Payroll> GetPayrollsForEmployee(int employeeId)
        {
            return payrollRepository.GetPayrollsForEmployee(employeeId);
        }
        public bool GeneratePayroll(Payroll payroll)
        {
            return  payrollRepository.GeneratePayroll(payroll);
        }
        public IEnumerable<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            return payrollRepository.GetPayrollsForPeriod(startDate, endDate);
        }
    }
}
