using PayXpert.BusinessLayer.Services;
using PayXpert.Entity;
using PayXpert.Exception;
using PayXpertApp.MainModule.SubMenu;
using System;
using System.Collections.Generic;
using PayXpert.Util;

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
                Console.Clear();
                ConsoleColorHelper.SetHeadingColor();
                Console.WriteLine("===========================================");
                Console.WriteLine("                 USER MENU                 ");
                Console.WriteLine("===========================================");
                
                Console.WriteLine("1. Employee Management");
                Console.WriteLine("2. Payroll Processing");
                Console.WriteLine("3. Tax Management");
                Console.WriteLine("4. Financial Reporting");
                Console.WriteLine("5. Exit");
                Console.WriteLine("--------------------------------------------");
                ConsoleColorHelper.SetSubheadingColor();
                Console.Write("Please select an option: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                ConsoleColorHelper.ResetColor();

                switch(choice)
                {
                    case 1:
                        EmployeeMenu.Menu();
                        break;
                    case 2:
                        PayrollMenu.Menu();
                        break;
                    case 3:
                        TaxMenu.Menu();
                        break;
                    case 4:
                        FinancialMenu.Menu();
                        break;
                    case 5:
                        Console.Beep();
                        ConsoleColorHelper.SetSuccessColor();
                        Console.WriteLine("Exiting the system...");
                        ConsoleColorHelper.ResetColor();

                        return;
                    default:
                        ConsoleColorHelper.SetErrorColor();
                        Console.WriteLine("Choose between 1-5");
                        ConsoleColorHelper.ResetColor();
                        break;
                }

            }

        }

    }
}
