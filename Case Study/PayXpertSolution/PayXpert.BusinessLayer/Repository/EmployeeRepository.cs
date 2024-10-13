using PayXpert.Entity;
using PayXpert.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net.Sockets;
using PayXpert.Exception;


namespace PayXpert.BusinessLayer.Repository
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        public Employee GetEmployeeById(int employeeId)
        {
            Employee employee = null;

            try
            {
                using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    string query = "SELECT * FROM Employee WHERE EmployeeID = @EmployeeID";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employee = new Employee
                            {
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                Gender = Convert.ToChar(reader["Gender"]),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Address = reader["Address"].ToString(),
                                Position = reader["Position"].ToString(),
                                JoiningDate = Convert.ToDateTime(reader["JoiningDate"].ToString()),
                                TerminationDate = reader["TerminationDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["TerminationDate"])
                            };
                        }
                    }
                }

                if (employee == null)
                {
                    throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine($"\n{ex.Message}");
                ConsoleColorHelper.ResetColor();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Unexpected Error occur");   
            }

            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees() 
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection conn = DBConnUtil.GetConnection()) 
            {
                string query = "Select * from Employee";
                SqlCommand command = new SqlCommand(query, conn);

                using (SqlDataReader reader = command.ExecuteReader()) 
                {
                    if (reader != null) 
                    {
                        while (reader.Read()) 
                        {
                            employees.Add(new Employee()
                            {
                                EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                Gender = Convert.ToChar(reader["Gender"]),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Address = reader["Address"].ToString(),
                                Position = reader["Position"].ToString(),
                                JoiningDate = Convert.ToDateTime(reader["JoiningDate"].ToString()),
                                TerminationDate = reader["TerminationDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["TerminationDate"])
                            });
                        }
                    }
                    else
                    {
                        throw new System.Exception("There is no record in the present in db");
                    }
                }
            }
            return employees;
        }
        public void AddEmployee(Employee employee)
        {
            using(SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "insert into Employee (FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, Address, Position, JoiningDate, TerminationDate) " +
                        "values (@FirstName, @LastName, @DateOfBirth, @Gender, @Email, @PhoneNumber, @Address, @Position, @JoiningDate, @TerminationDate)";

                SqlCommand command = new SqlCommand(query, conn);
                
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                command.Parameters.AddWithValue("@Address", employee.Address);
                command.Parameters.AddWithValue("@Position", employee.Position);
                command.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                command.Parameters.AddWithValue("@TerminationDate", (object)employee.TerminationDate ?? DBNull.Value); 

                int rowAffected=command.ExecuteNonQuery();
                if(rowAffected == 0) {
                    throw new System.Exception("Record not inserted");
                }
                else
                {
                    Console.WriteLine("Employee Added Successfully!");
                }
            }
        }
        
        public void RemoveEmployee(int employeeId) 
        {
            try{using (SqlConnection conn = DBConnUtil.GetConnection())
                {
                    string query = "Delete from Employee where EmployeeID = @EmployeeID";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@EmployeeID", employeeId);

                    int rowAffected = command.ExecuteNonQuery();

                    if (rowAffected == 0)
                    {
                        ConsoleColorHelper.SetErrorColor();
                        throw new EmployeeNotFoundException("Employee Not found");
                        ConsoleColorHelper.ResetColor();
                    }
                    else
                    {
                        ConsoleColorHelper.SetSuccessColor();
                        Console.WriteLine($"{rowAffected} rows deleted from Employee table with employeeid : {employeeId}");
                        ConsoleColorHelper.ResetColor();
                    };
                }
            }
            catch (EmployeeNotFoundException ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }

        }
        public void UpdateEmployee(Employee employee,int employeeID) 
        {
            using (SqlConnection conn = DBConnUtil.GetConnection()) 
            {
                string query = "update Employee set FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Gender = @Gender, " +
                               "Email = @Email, PhoneNumber = @PhoneNumber, Address = @Address, Position = @Position, JoiningDate = @JoiningDate, " +
                               "TerminationDate = @TerminationDate WHERE EmployeeID = @EmployeeID";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                command.Parameters.AddWithValue("@Address", employee.Address);
                command.Parameters.AddWithValue("@Position", employee.Position);
                command.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                command.Parameters.AddWithValue("@TerminationDate", employee.TerminationDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EmployeeID", employeeID);

                int rowAffected = command.ExecuteNonQuery();

                if (rowAffected == 0)
                {
                    throw new System.Exception("No records updated");
                }
                else
                {
                    Console.WriteLine($"{rowAffected} rows updated successfull!");
                }
            }

            
        }
    }
}
