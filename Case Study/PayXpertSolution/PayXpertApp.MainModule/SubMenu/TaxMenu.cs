using PayXpert.BusinessLayer.Services;
using PayXpert.Exception;
using PayXpert.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpertApp.MainModule.SubMenu
{
    internal class TaxMenu
    {

        private static readonly  ITaxService _taxService = new TaxService();
        internal static void Menu()
        {
            while (true)
            {
                Console.Clear();
                ConsoleColorHelper.SetHeadingColor();
                Console.WriteLine("================================================");
                Console.WriteLine("                 TAX MANAGEMENT                 ");
                Console.WriteLine("================================================");
                Console.WriteLine("1. Calculate Tax for an Employee");
                Console.WriteLine("2. View Taxes by Employee ID");
                Console.WriteLine("3. View Taxes by Year");
                Console.WriteLine("4. Exit");
                Console.WriteLine("------------------------------------------------");

                ConsoleColorHelper.SetSubheadingColor();
                Console.Write("Please select an option: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                ConsoleColorHelper.ResetColor();

                switch (choice)
                {
                    case 1:
                        CalculateTaxForEmployee();

                        break;
                    case 2:
                        TaxByEmployeeID();
                        break;
                    case 3:
                        ViewTaxByYear();
                        break;
                    case 4:
                        ConsoleColorHelper.SetSuccessColor();
                        Console.WriteLine("Press Enter to exit Tax Management...");
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
        private static void CalculateTaxForEmployee()
        {
            Console.Clear();
            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("================================================");
            Console.WriteLine("         CALCULATE TAX FOR AN EMPLOYEE         ");
            Console.WriteLine("================================================");
            ConsoleColorHelper.ResetColor();

            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine());

            Console.Write("Enter Tax Year (YYYY): ");
            int taxYear = int.Parse(Console.ReadLine());

            try
            {
                var taxAmount = _taxService.CalculateTax(employeeId, taxYear);
                ConsoleColorHelper.SetSuccessColor();
                Console.WriteLine($"Tax for Employee ID {employeeId} for the year {taxYear}: {taxAmount}");
                ConsoleColorHelper.ResetColor();
            }
            catch (TaxCalculationException ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine($"Error calculating tax: {ex.Message}");
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

        private static void TaxByEmployeeID()
        {
            Console.Clear();
            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("================================================");
            Console.WriteLine("               VIEW TAX BY EMPLOYEE ID         ");
            Console.WriteLine("================================================");
            ConsoleColorHelper.ResetColor();

            Console.Write("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine());

            try
            {
                var taxes = _taxService.GetTaxesForEmployee(employeeId);
                if (!taxes.Any())
                {
                    ConsoleColorHelper.SetErrorColor();
                    Console.WriteLine($"No tax records found for Employee ID: {employeeId}");
                    ConsoleColorHelper.ResetColor();
                }
                else
                {
                    foreach (var tax in taxes)
                    {
                        Console.WriteLine($"Tax ID: {tax.TaxID}, " +
                                          $"Year: {tax.TaxYear}, " +
                                          $"Taxable Income: {tax.TaxableIncome}, " +
                                          $"Tax Amount: {tax.TaxAmount}");
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine($"Error retrieving tax records: {ex.Message}");
                ConsoleColorHelper.ResetColor();
            }

            Console.WriteLine("\nPress Enter to go back...");
            Console.ReadKey();
        }

        private static void ViewTaxByYear()
        {
            Console.Clear();
            ConsoleColorHelper.SetHeadingColor();
            Console.WriteLine("================================================");
            Console.WriteLine("                VIEW TAX BY YEAR               ");
            Console.WriteLine("================================================");
            ConsoleColorHelper.ResetColor();

            Console.Write("Enter Tax Year (YYYY): ");
            int taxYear = int.Parse(Console.ReadLine());

            try
            {
                var taxes = _taxService.GetTaxesForYear(taxYear);
                if (!taxes.Any())
                {
                    ConsoleColorHelper.SetErrorColor();
                    Console.WriteLine($"No tax records found for the year: {taxYear}");
                    ConsoleColorHelper.ResetColor();
                }
                else
                {
                    foreach (var tax in taxes)
                    {
                        Console.WriteLine($"Tax ID: {tax.TaxID}, " +
                                          $"Employee ID: {tax.EmployeeID}, " +
                                          $"Taxable Income: {tax.TaxableIncome}, " +
                                          $"Tax Amount: {tax.TaxAmount}");
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine($"Error retrieving tax records: {ex.Message}");
                ConsoleColorHelper.ResetColor();
            }

            Console.WriteLine("\nPress Enter to go back...");
            Console.ReadKey();
        }

    }
}
