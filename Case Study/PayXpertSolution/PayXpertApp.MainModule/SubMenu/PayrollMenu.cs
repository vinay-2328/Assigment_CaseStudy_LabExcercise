using PayXpert.BusinessLayer.Services;
using PayXpert.Entity;
using PayXpert.Exception;
using PayXpert.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpertApp.MainModule.SubMenu
{
    internal class PayrollMenu
    {
        private static readonly IPayrollService _payrollService = new PayrollService();
        internal static void Menu()
        {
            while (true)
            {
                Console.Clear();
                ConsoleColorHelper.SetHeadingColor();
                Console.WriteLine("================================================");
                Console.WriteLine("               PAYROLL PROCESSING               ");
                Console.WriteLine("================================================");
                Console.WriteLine("1. Generate Payroll for an Employee");
                Console.WriteLine("2. View Payroll by Employee ID");
                Console.WriteLine("3. View Payroll for a Pay Period");
                Console.WriteLine("4. Exit");
                Console.WriteLine("------------------------------------------------");

                ConsoleColorHelper.SetSubheadingColor();
                Console.Write("Please select an option: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                ConsoleColorHelper.ResetColor();

                switch (choice)
                {
                    case 1:
                        GeneratePayroll();

                        break;
                    case 2:
                        ViewPayrollByEmployeeID();
                        break;
                    case 3:
                        ViewPayrollForPayPeriod();
                        break;
                    case 4:
                        ConsoleColorHelper.SetSuccessColor();
                        Console.WriteLine("Press Enter to exit Payroll Processing...");
                        ConsoleColorHelper.ResetColor();
                        Console.ReadKey();
                        return;
                    default:
                        ConsoleColorHelper.SetErrorColor();
                        Console.WriteLine("Invalid Choice!");
                        ConsoleColorHelper.ResetColor();
                        Console.WriteLine("Choice from 1-4 press enter to continue...");
                        Console.ReadKey();
                        break;

                }


            }
        }

        private static void GeneratePayroll()
        {
            Payroll payroll = new Payroll();

            Console.Clear();
            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("=======================================================");
            Console.WriteLine("               Enter new Payroll Details               ");
            Console.WriteLine("=======================================================");
            ConsoleColorHelper.ResetColor();

            Console.Write("Enter Employee ID: ");
            payroll.EmployeeID = int.Parse(Console.ReadLine());

            Console.Write("Enter Pay Period Start Date (YYYY-MM-DD): ");
            payroll.PayPeriodStartDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Pay Period End Date (YYYY-MM-DD): ");
            payroll.PayPeriodEndDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Basic Salary: ");
            payroll.BasicSalary = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Overtime Pay: ");
            payroll.OvertimePay = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Deductions: ");
            payroll.Deductions = decimal.Parse(Console.ReadLine());

            try
            {
                _payrollService.GeneratePayroll(payroll);
                ConsoleColorHelper.SetSuccessColor();
                Console.WriteLine($"Payroll generated successfully for Employee ID: {payroll.EmployeeID}.");
                ConsoleColorHelper.ResetColor();
            }
            catch (PayrollGenerationException ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine($"Error generating payroll: {ex.Message}");
                ConsoleColorHelper.ResetColor();
            }
            catch (Exception ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine($"Unexpected error: {ex.Message}");
                ConsoleColorHelper.ResetColor();
            }

            Console.WriteLine("\nPress Enter to go back...");
            Console.ReadKey();
        }

        private static void ViewPayrollByEmployeeID()
        {
            Console.Clear();
            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("=======================================================");
            Console.WriteLine("                 View Payroll by Employee ID          ");
            Console.WriteLine("=======================================================");
            ConsoleColorHelper.ResetColor();

            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine());

            try
            {
                IEnumerable<Payroll> payrolls = _payrollService.GetPayrollsForEmployee(employeeId);

                if (payrolls.Any())
                {
                    ConsoleColorHelper.SetSubheadingColor();
                    Console.WriteLine($"Payroll records for Employee ID: {employeeId}");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------");
                    Console.WriteLine("{0,-15} {1,-20} {2,-18} {3,-15} {4,-15} {5,-15}", "Payroll ID", "Pay Period Start", "Pay Period End", "Basic Salary", "Overtime Pay", "Deductions");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------");

                    foreach (var payroll in payrolls)
                    {
                        Console.WriteLine("{0,-15} {1,-20} {2,-18} {3,-15} {4,-15} {5,-15}",
                            payroll.PayrollID,
                            payroll.PayPeriodStartDate.ToShortDateString(),
                            payroll.PayPeriodEndDate.ToShortDateString(),
                            payroll.BasicSalary,
                            payroll.OvertimePay,
                            payroll.Deductions);
                    }
                }
                else
                {
                    ConsoleColorHelper.SetErrorColor();
                    Console.WriteLine("No payroll records found for this Employee ID.");
                    ConsoleColorHelper.ResetColor();
                }
            }
            catch (Exception ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine($"Error: {ex.Message}");
                ConsoleColorHelper.ResetColor();
            }

            Console.WriteLine("\nPress Enter to go back...");
            Console.ReadKey();
        }

        private static void ViewPayrollForPayPeriod()
        {
            Console.Clear();
            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("=======================================================");
            Console.WriteLine("                View Payroll for Pay Period          ");
            Console.WriteLine("=======================================================");
            ConsoleColorHelper.ResetColor();

            Console.Write("Enter Pay Period Start Date (YYYY-MM-DD): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Pay Period End Date (YYYY-MM-DD): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            try
            {
                IEnumerable<Payroll> payrolls = _payrollService.GetPayrollsForPeriod(startDate, endDate);

                if (payrolls.Any())
                {
                    ConsoleColorHelper.SetSubheadingColor();
                    Console.WriteLine($"Payroll records from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------");
                    Console.WriteLine("{0,-15} {1,-20} {2,-18} {3,-15} {4,-15} {5,-15}", "Payroll ID", "Employee ID", "Pay Period Start", "Pay Period End", "Basic Salary", "Overtime Pay");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------");

                    foreach (var payroll in payrolls)
                    {
                        Console.WriteLine("{0,-15} {1,-20} {2,-18} {3,-15} {4,-15} {5,-15}",
                            payroll.PayrollID,
                            payroll.EmployeeID,
                            payroll.PayPeriodStartDate.ToShortDateString(),
                            payroll.PayPeriodEndDate.ToShortDateString(),
                            payroll.BasicSalary,
                            payroll.OvertimePay);
                    }
                }
                else
                {
                    ConsoleColorHelper.SetErrorColor();
                    Console.WriteLine("No payroll records found for the given pay period.");
                    ConsoleColorHelper.ResetColor();
                }
            }
            catch (Exception ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine($"Error: {ex.Message}");
                ConsoleColorHelper.ResetColor();
            }

            Console.WriteLine("\nPress Enter to go back...");
            Console.ReadKey();
        }

    }
}
