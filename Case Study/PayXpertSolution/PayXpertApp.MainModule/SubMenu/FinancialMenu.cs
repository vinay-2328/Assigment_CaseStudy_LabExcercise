using PayXpert.BusinessLayer.Repository;
using PayXpert.BusinessLayer.Services;
using PayXpert.Entity;
using PayXpert.Exception;
using PayXpert.Util;
using System;
using System.Collections.Generic;

namespace PayXpertApp.MainModule.SubMenu
{
    internal class FinancialMenu
    {
        private static readonly FinancialRecordService _financialRecordService = new FinancialRecordService();

        internal static void Menu()
        {
            while (true)
            {
                Console.Clear();
                ConsoleColorHelper.SetHeadingColor();
                Console.WriteLine("=====================================================");
                Console.WriteLine("                 FINANCIAL REPORTING                 ");
                Console.WriteLine("=====================================================");
                Console.WriteLine("1. Add Financial Record");
                Console.WriteLine("2. View Financial Record by Employee ID");
                Console.WriteLine("3. View Financial Record by Date");
                Console.WriteLine("4. Generate Financial Report");
                Console.WriteLine("5. Exit");
                Console.WriteLine("-----------------------------------------------------");

                ConsoleColorHelper.SetSubheadingColor();
                Console.Write("Please select an option: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                ConsoleColorHelper.ResetColor();

                switch (choice)
                {
                    case 1:
                        AddFinancialRecord();
                        break;
                    case 2:
                        ViewFinancialRecordByEmployeeID();
                        break;
                    case 3:
                        ViewFinancialRecordByDate();
                        break;
                    case 4:
                        GenerateFinancialReport();
                        break;
                    case 5:
                        ConsoleColorHelper.SetSuccessColor();
                        Console.WriteLine("Press Enter to exit Financial Reporting...");
                        ConsoleColorHelper.ResetColor();
                        Console.ReadKey();
                        return;
                    default:
                        ConsoleColorHelper.SetErrorColor();
                        Console.WriteLine("Invalid Choice!");
                        ConsoleColorHelper.ResetColor();
                        Console.WriteLine("Choice from 1-5 press enter to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void AddFinancialRecord()
        {
           

            try
            {
                Console.Clear();
                ConsoleColorHelper.SetHeadingColor();
                Console.WriteLine("========================================================");
                Console.WriteLine("               Enter new Financial record               ");
                Console.WriteLine("========================================================");
                ConsoleColorHelper.ResetColor();

                FinancialRecord record = new FinancialRecord();
                Console.Write("Enter Employee ID: ");
                record.EmployeeID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Record Date (yyyy-mm-dd): ");
                record.RecordDate = Convert.ToDateTime(Console.ReadLine());
                Console.Write("Enter Description: ");
                record.Description = Console.ReadLine();
                Console.Write("Enter Amount: ");
                record.Amount = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Enter Record Type (Income/Expense): ");
                record.RecordType = Console.ReadLine();

                _financialRecordService.AddFinancialRecord(record);

                ConsoleColorHelper.SetSuccessColor();
                Console.WriteLine("Financial record added successfully!");
                ConsoleColorHelper.ResetColor();
            }
            catch (Exception ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine($"Error: {ex.Message}");
                ConsoleColorHelper.ResetColor();
            }


            Console.WriteLine("\nPress enter to go back...");
            Console.ReadKey();
        }

        private static void ViewFinancialRecordByEmployeeID()
        {
            try
            {
                Console.Clear();
                ConsoleColorHelper.SetHeadingColor();
                Console.WriteLine("=================================================================");
                Console.WriteLine("               View Financial Record By EmployeeID               ");
                Console.WriteLine("=================================================================");
                ConsoleColorHelper.ResetColor();

                Console.Write("Enter Employee ID: ");
                int employeeId = Convert.ToInt32(Console.ReadLine());
                IEnumerable<FinancialRecord> records = _financialRecordService.GetFinancialRecordsForEmployee(employeeId);

                Console.WriteLine($"\nFinancial Records for Employee ID: {employeeId}");
                foreach (var record in records)
                {
                    Console.WriteLine($"Record ID: {record.RecordID}, Date: {record.RecordDate}, Description: {record.Description}, Amount: {record.Amount}, Type: {record.RecordType}");
                }
            }
            catch (Exception ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine($"Error: {ex.Message}");
                ConsoleColorHelper.ResetColor();
            }

            Console.WriteLine("\nPress enter to go back...");
            Console.ReadKey();
        }

        private static void ViewFinancialRecordByDate()
        {
            try
            {
                Console.Clear();
                ConsoleColorHelper.SetHeadingColor();
                Console.WriteLine("=======================================================");
                Console.WriteLine("             View Financial Record By Date             ");
                Console.WriteLine("=======================================================");
                ConsoleColorHelper.ResetColor();

                Console.Write("Enter Record Date (yyyy-mm-dd): ");
                DateTime recordDate = Convert.ToDateTime(Console.ReadLine());
                IEnumerable<FinancialRecord> records = _financialRecordService.GetFinancialRecordForDate(recordDate);

                Console.WriteLine($"\nFinancial Records for Date: {recordDate.ToShortDateString()}");
                foreach (var record in records)
                {
                    Console.WriteLine($"Record ID: {record.RecordID}, Employee ID: {record.EmployeeID}, Description: {record.Description}, Amount: {record.Amount}, Type: {record.RecordType}");
                }
            }
            catch (Exception ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine($"Error: {ex.Message}");
                ConsoleColorHelper.ResetColor();
            }

            Console.WriteLine("\nPress enter to go back...");
            Console.ReadKey();
        }

        private static void GenerateFinancialReport()
        {
            
            Console.WriteLine("Generating financial report");

            Console.WriteLine("\nPress enter to go back...");
            Console.ReadKey();
            
        }
    }
}
