﻿using PayXpert.Entity;
using PayXpert.Exception;
using PayXpert.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace PayXpert.BusinessLayer.Repository
{
    internal class PayrollRepository : IPayrollRepository
    {
        public Payroll GetPayrollById(int payrollId)
        {
            Payroll payroll=null;
            
            using(SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "select * from Payroll where PayrollID = @PayrollID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@PayrollID", payrollId);

                using(SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        payroll = new Payroll
                        {
                            PayrollID = Convert.ToInt32(reader["PayrollID"]),
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            PayPeriodStartDate = Convert.ToDateTime(reader["PayPeriodStartDate"]),
                            PayPeriodEndDate = Convert.ToDateTime(reader["PayPeriodEndDate"]),
                            BasicSalary = Convert.ToDecimal(reader["BasicSalary"]),
                            OvertimePay = Convert.ToDecimal(reader["OvertimePay"]),
                            Deductions = Convert.ToDecimal(reader["Deductions"]),
                        };
                    }
                }
                
            }

            if (payroll == null)
            {
                throw new System.Exception($"Record not found for Payroll id: {payroll}");
            }
            return payroll;
        }
        public IEnumerable<Payroll> GetPayrollsForEmployee(int employeeId)
        {
            List<Payroll> payrolls = new List<Payroll>();

            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Payroll WHERE EmployeeID = @EmployeeID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@EmployeeID", employeeId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payrolls.Add(new Payroll
                        {
                            PayrollID = Convert.ToInt32(reader["PayrollID"]),
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            PayPeriodStartDate = Convert.ToDateTime(reader["PayPeriodStartDate"]),
                            PayPeriodEndDate = Convert.ToDateTime(reader["PayPeriodEndDate"]),
                            BasicSalary = Convert.ToDecimal(reader["BasicSalary"]),
                            OvertimePay = Convert.ToDecimal(reader["OvertimePay"]),
                            Deductions = Convert.ToDecimal(reader["Deductions"]),
                        });
                    }
                }
            }
            if(payrolls.Count == 0)
            {
                throw new System.Exception($"No rcords found for employee id: {employeeId}");
            }
            return payrolls;
        }
        public void GeneratePayroll(Payroll payroll)
        {
            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Payroll (EmployeeID, PayPeriodStartDate, PayPeriodEndDate, BasicSalary, OvertimePay, Deductions) " +
                               "VALUES (@EmployeeID, @PayPeriodStartDate, @PayPeriodEndDate, @BasicSalary, @OvertimePay, @Deductions)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeID", payroll.EmployeeID);
                command.Parameters.AddWithValue("@PayPeriodStartDate", payroll.PayPeriodStartDate);
                command.Parameters.AddWithValue("@PayPeriodEndDate", payroll.PayPeriodEndDate);
                command.Parameters.AddWithValue("@BasicSalary", payroll.BasicSalary);
                command.Parameters.AddWithValue("@OvertimePay", payroll.OvertimePay);
                command.Parameters.AddWithValue("@Deductions", payroll.Deductions);

               int rowAffected= command.ExecuteNonQuery();

                if (rowAffected == 0) 
                {
                    throw new PayrollGenerationException($"Failed to generate payroll for Employee ID: {payroll.EmployeeID}");
                }
                else
                {
                    Console.WriteLine($"{rowAffected} rows affected and record added successfully!");
                }
            }

        }
        public IEnumerable<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            List<Payroll> payrolls = new List<Payroll>();
            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Payroll WHERE PayPeriodStartDate >= @StartDate AND PayPeriodEndDate <= @EndDate";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payrolls.Add(new Payroll
                        {
                            PayrollID = Convert.ToInt32(reader["PayrollID"]),
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            PayPeriodStartDate = Convert.ToDateTime(reader["PayPeriodStartDate"]),
                            PayPeriodEndDate = Convert.ToDateTime(reader["PayPeriodEndDate"]),
                            BasicSalary = Convert.ToDecimal(reader["BasicSalary"]),
                            OvertimePay = Convert.ToDecimal(reader["OvertimePay"]),
                            Deductions = Convert.ToDecimal(reader["Deductions"]),
                        });
                    }
                }
            }

            if (payrolls.Count == 0)
            {
                throw new System.Exception("No record found in given period");
            }
            return payrolls;
        }
    
    }
}
