using PayXpert.BusinessLayer.Services;
using PayXpert.Entity;
using PayXpert.Exception;
using System;
using System.Collections.Generic;

namespace PayXpertApp.MainModule
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "PayXpert - Payroll Management System";
            Console.WriteLine("Welcome to PayXpert, The Payroll Management System\n");

            EmployeeService employeeService = new EmployeeService();
            PayrollService payrollService = new PayrollService();
            TaxService taxService = new TaxService();
            FinancialRecordService financialRecordService = new FinancialRecordService();

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. View All Employees");
                Console.WriteLine("2. Add Employee");
                Console.WriteLine("3. Generate Payroll");
                Console.WriteLine("4. Calculate Tax");
                Console.WriteLine("5. Add Financial Record");
                Console.WriteLine("6. View Financial Records");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ViewAllEmployees(employeeService);
                        break;
                    case "2":
                        AddEmployee(employeeService);
                        break;
                    case "3":
                        GeneratePayroll(payrollService);
                        break;
                    case "4":
                        CalculateTax(taxService);
                        break;
                    case "5":
                        AddFinancialRecord(financialRecordService);
                        break;
                    case "6":
                        ViewFinancialRecords(financialRecordService);
                        break;
                    case "7":
                        Console.WriteLine("Exiting the application...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private static void ViewAllEmployees(EmployeeService employeeService)
        {
            try
            {
                IEnumerable<Employee> employees = employeeService.GetAllEmployees();
                Console.WriteLine("Employee List:");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.EmployeeID}: {employee.FirstName} {employee.LastName}");
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AddEmployee(EmployeeService employeeService)
        {
            Employee newEmployee = new Employee();

            Console.Write("Enter First Name: ");
            newEmployee.FirstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            newEmployee.LastName = Console.ReadLine();

            Console.Write("Enter Date of Birth (YYYY-MM-DD): ");
            DateTime dateOfBirth;
            while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
            {
                Console.Write("Invalid date format. Please enter a valid Date of Birth (YYYY-MM-DD): ");
            }
            newEmployee.DateOfBirth = dateOfBirth;

            Console.Write("Enter Gender (M/F): ");
            char gender;
            while (!char.TryParse(Console.ReadLine(), out gender) || (gender != 'M' && gender != 'F'))
            {
                Console.Write("Invalid input. Please enter 'M' for Male or 'F' for Female: ");
            }
            newEmployee.Gender = gender;

            Console.Write("Enter Email: ");
            newEmployee.Email = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            newEmployee.PhoneNumber = Console.ReadLine();

            Console.Write("Enter Address: ");
            newEmployee.Address = Console.ReadLine();

            Console.Write("Enter Position: ");
            newEmployee.Position = Console.ReadLine();

            Console.Write("Enter Joining Date (YYYY-MM-DD): ");
            DateTime joiningDate;
            while (!DateTime.TryParse(Console.ReadLine(), out joiningDate))
            {
                Console.Write("Invalid date format. Please enter a valid Joining Date (YYYY-MM-DD): ");
            }
            newEmployee.JoiningDate = joiningDate;

            Console.Write("Enter Termination Date (leave blank if not applicable, format: YYYY-MM-DD): ");
            string terminationDateInput = Console.ReadLine();
            if (DateTime.TryParse(terminationDateInput, out DateTime terminationDate))
            {
                newEmployee.TerminationDate = terminationDate;
            }
            else
            {
                newEmployee.TerminationDate = null; 
            }

            try
            {
                employeeService.AddEmployee(newEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee: {ex.Message}");
            }
        }

        private static void GeneratePayroll(PayrollService payrollService)
        {
            Payroll payroll = new Payroll();

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
                payrollService.GeneratePayroll(payroll);
                Console.WriteLine($"Payroll generated successfully for Employee ID: {payroll.EmployeeID}.");
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine($"Error generating payroll: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }


        private static void CalculateTax(TaxService taxService)
        {
            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine());

            Console.Write("Enter Tax Year: ");
            int taxYear = int.Parse(Console.ReadLine());

            try
            {
                decimal taxAmount = taxService.CalculateTax(employeeId, taxYear);
                Console.WriteLine($"Tax calculated successfully for Employee ID: {employeeId}. Tax Amount: {taxAmount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating tax: {ex.Message}");
            }
        }

        private static void AddFinancialRecord(FinancialRecordService financialRecordService)
        {
            FinancialRecord record = new FinancialRecord();

            Console.Write("Enter Employee ID: ");
            record.EmployeeID = int.Parse(Console.ReadLine());

            Console.Write("Enter Record Date (YYYY-MM-DD): ");
            record.RecordDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Description: ");
            record.Description = Console.ReadLine();

            Console.Write("Enter Amount: ");
            record.Amount = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Record Type (income, expense, tax payment, etc.): ");
            record.RecordType = Console.ReadLine();

            try
            {
                financialRecordService.AddFinancialRecord(record);
                Console.WriteLine("Financial record added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding financial record: {ex.Message}");
            }
        }

        private static void ViewFinancialRecords(FinancialRecordService financialRecordService)
        {
            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine());

            try
            {
                var records = financialRecordService.GetFinancialRecordsForEmployee(employeeId);
                Console.WriteLine("Financial Records:");
                foreach (var record in records)
                {
                    Console.WriteLine($"{record.RecordID}: {record.Description} - {record.Amount} on {record.RecordDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving financial records: {ex.Message}");
            }
        }
    }
}
