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
    internal class EmployeeMenu
    {
        private static readonly EmployeeService _employeeService = new EmployeeService();
        internal static void Menu()
        {
           while(true)
           {
                Console.Clear();
                ConsoleColorHelper.SetHeadingColor();
                Console.WriteLine("===========================================");
                Console.WriteLine("               EMPLOYEE MENU               ");
                Console.WriteLine("===========================================");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Update Employee");
                Console.WriteLine("3. Remove Employee");
                Console.WriteLine("4. View All Employee");
                Console.WriteLine("5. Search Employee by ID");
                Console.WriteLine("6. Exit");
                Console.WriteLine("-------------------------------------------");

                ConsoleColorHelper.SetSubheadingColor();
                Console.Write("Please select an option: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                ConsoleColorHelper.ResetColor();

                switch(choice)
                {
                    case 1:
                        AddEmployee();
                        break;
                    case 2:
                        UpdateEmployee();
                        break;
                    case 3:
                        DeleteEmployee();
                        break;
                    case 4:
                        ViewAllEmployees();
                        break;
                    case 5:
                        SearchEmployeeByID();
                        break;
                    case 6:
                        ConsoleColorHelper.SetSuccessColor();
                        Console.WriteLine("Press Enter to exit Employee Management...");
                        ConsoleColorHelper.ResetColor();
                        Console.ReadKey();
                        return;
                    default:
                        ConsoleColorHelper.SetErrorColor();
                        Console.WriteLine("Invalid Choice!");
                        ConsoleColorHelper.ResetColor();
                        Console.WriteLine("Choice from 1-6 press enter to continue...");
                        Console.ReadKey();
                        break;

                }


            }
            
        }

        private static void AddEmployee()
        {
            Employee newEmployee = new Employee();

            Console.Clear();
            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("================================");
            Console.WriteLine("      Add Employee Details      ");
            Console.WriteLine("================================");
            ConsoleColorHelper.ResetColor();


            ConsoleColorHelper.SetSubheadingColor();

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
                _employeeService.AddEmployee(newEmployee);
                ConsoleColorHelper.SetSuccessColor();
                Console.WriteLine("\nEmployee Added Successfully!");
                Console.WriteLine("Press enter to Continue...");
                Console.ReadKey();
                ConsoleColorHelper.ResetColor();
            }
            catch (Exception ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine($"Error adding employee: {ex.Message}");
                ConsoleColorHelper.ResetColor();
            }
        }

        private static void ViewAllEmployees()
        {
            try
            {
                Console.Clear();
                ConsoleColorHelper.SetHeadingColor();
                Console.WriteLine("=====================================");
                Console.WriteLine("          LIST OF EMPLOYEES          ");
                Console.WriteLine("=====================================");
                ConsoleColorHelper.ResetColor();

                IEnumerable<Employee> employees = _employeeService.GetAllEmployees();

                ConsoleColorHelper.SetSubheadingColor();
                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.EmployeeID}: {employee.FirstName} {employee.LastName}");
                }
                Console.WriteLine("-------------------------------------");
                ConsoleColorHelper.ResetColor();

                Console.WriteLine("\nPress Enter to Go back...");
                Console.ReadKey();

            }
            catch (EmployeeNotFoundException ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }
        }



        private static void UpdateEmployee()
        {
            Employee udaptedEmployee = new Employee();

            Console.Clear();
            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("=====================================");
            Console.WriteLine("          LIST OF EMPLOYEES          ");
            Console.WriteLine("=====================================");
            ConsoleColorHelper.ResetColor();

            IEnumerable<Employee> employees = _employeeService.GetAllEmployees();

            ConsoleColorHelper.SetSubheadingColor();
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.EmployeeID}: {employee.FirstName} {employee.LastName}");
            }
            Console.WriteLine("-------------------------------------");
            ConsoleColorHelper.ResetColor();

            Console.Write("\nSelect Employee you want to update, Enter the Employee Id: ");
            int employeeID = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("========================================");
            Console.WriteLine("      Add Updated Employee Details      ");
            Console.WriteLine("========================================");
            ConsoleColorHelper.ResetColor();


            ConsoleColorHelper.SetSubheadingColor();

            Console.Write("Enter First Name: ");
            udaptedEmployee.FirstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            udaptedEmployee.LastName = Console.ReadLine();

            Console.Write("Enter Date of Birth (YYYY-MM-DD): ");
            DateTime dateOfBirth;
            while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
            {
                Console.Write("Invalid date format. Please enter a valid Date of Birth (YYYY-MM-DD): ");
            }
            udaptedEmployee.DateOfBirth = dateOfBirth;

            Console.Write("Enter Gender (M/F): ");
            char gender;
            while (!char.TryParse(Console.ReadLine(), out gender) || (gender != 'M' && gender != 'F'))
            {
                Console.Write("Invalid input. Please enter 'M' for Male or 'F' for Female: ");
            }
            udaptedEmployee.Gender = gender;

            Console.Write("Enter Email: ");
            udaptedEmployee.Email = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            udaptedEmployee.PhoneNumber = Console.ReadLine();

            Console.Write("Enter Address: ");
            udaptedEmployee.Address = Console.ReadLine();

            Console.Write("Enter Position: ");
            udaptedEmployee.Position = Console.ReadLine();

            Console.Write("Enter Joining Date (YYYY-MM-DD): ");
            DateTime joiningDate;
            while (!DateTime.TryParse(Console.ReadLine(), out joiningDate))
            {
                Console.Write("Invalid date format. Please enter a valid Joining Date (YYYY-MM-DD): ");
            }
            udaptedEmployee.JoiningDate = joiningDate;

            Console.Write("Enter Termination Date (leave blank if not applicable, format: YYYY-MM-DD): ");
            string terminationDateInput = Console.ReadLine();
            if (DateTime.TryParse(terminationDateInput, out DateTime terminationDate))
            {
                udaptedEmployee.TerminationDate = terminationDate;
            }
            else
            {
                udaptedEmployee.TerminationDate = null;
            }

            try
            {
                _employeeService.UpdateEmployee(udaptedEmployee, employeeID);
                ConsoleColorHelper.SetSuccessColor();
                Console.WriteLine($"Employee {udaptedEmployee.FirstName} {udaptedEmployee.LastName} is updated Successfully!");
                ConsoleColorHelper.ResetColor();
            }catch(InvalidInputException ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }


            Console.WriteLine("Press Enter to go back...");
            Console.ReadKey();

        }

        private static void DeleteEmployee()
        {
            Console.Clear();
            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("=====================================");
            Console.WriteLine("          LIST OF EMPLOYEES          ");
            Console.WriteLine("=====================================");
            ConsoleColorHelper.ResetColor();

            IEnumerable<Employee> employees = _employeeService.GetAllEmployees();

            ConsoleColorHelper.SetSubheadingColor();
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.EmployeeID}: {employee.FirstName} {employee.LastName}");
            }
            Console.WriteLine("-------------------------------------");
            ConsoleColorHelper.ResetColor();

            Console.Write("\nSelect Employee you want to delete, Enter the Employee Id: ");
            int employeeID = Convert.ToInt32(Console.ReadLine());
            
            _employeeService.RemoveEmployee(employeeID);
            
            
            Console.WriteLine("press Enter to go back...");
            Console.ReadKey();
        }

        private static void SearchEmployeeByID()
        {
            Console.Clear();
            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("=======================================");
            Console.WriteLine("         Search Employee by ID         ");
            Console.WriteLine("=======================================");
            ConsoleColorHelper.ResetColor();

            ConsoleColorHelper.SetSubheadingColor();
            Console.Write("Enter the Employee ID: ");
            int employeeId = Convert.ToInt32(Console.ReadLine());
            ConsoleColorHelper.ResetColor();
            Employee employee=_employeeService.GetEmployeeById(employeeId);
            
            if(employee != null)
            {
                ConsoleColorHelper.SetSuccessColor();
                Console.WriteLine("\nEmployee Found!");
                Console.WriteLine($"Employee Name is: {employee.FirstName} {employee.LastName}");
                ConsoleColorHelper.ResetColor();

            }
            ConsoleColorHelper.ResetColor();
            Console.WriteLine("\nPress enter to go back...");
            Console.ReadKey();
        }


    }

}
