using System;
using System.Data.SqlClient;
using TicketBookingSystem.Entity;
using TicketBookingSystem.Util;


namespace TicketBookingSystem.BusinessLayer.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer GetCustomerById(int customerId)
        {
            Customer customer = null;
            using (SqlConnection conn = GetDBConn.GetConnection())
            {
                string query = "select * from Customer where CustomerID = @CustomerID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@CustomerID", customerId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customer = new Customer()
                        {
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            CustomerName = Convert.ToString(reader["CustomerName"]),
                            Email = Convert.ToString(reader["Email"]),
                            PhoneNumber = Convert.ToString(reader["PhoneNumber"]),
                            BookingID = reader["BookingID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["BookingID"]) : null
                        };
                    }

                }
            }
            return customer;
        }
        public void DisplayCustomerDetails(Customer customer)
        {
            Console.WriteLine("========== Displaying Customer Details ==========");
            Console.WriteLine($"Customer Name: {customer.CustomerName}\nCustomer Email: {customer.Email}\nCustomer Phone-No: {customer.PhoneNumber}");
        }

        public Customer CheckUser(string email, string password)
        {
            Customer customer = null;

            try
            {
                using (SqlConnection conn = GetDBConn.GetConnection())
                {
                    string query = "SELECT CustomerID, CustomerName, Email, PhoneNumber, BookingID, Password FROM Customer WHERE Email = @Email";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (string.Equals(Convert.ToString(reader["Email"]), email, StringComparison.OrdinalIgnoreCase)
                                && Convert.ToString(reader["Password"]) == password)
                            {
                                customer = new Customer()
                                {
                                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                                    CustomerName = Convert.ToString(reader["CustomerName"]),
                                    Email = Convert.ToString(reader["Email"]),
                                    PhoneNumber = Convert.ToString(reader["PhoneNumber"]),
                                    BookingID = reader["BookingID"] != DBNull.Value ? (int?)Convert.ToInt32(reader["BookingID"]) : null
                                };
                            }
                            else
                            {

                                throw new UnauthorizedAccessException("Invalid credentials provided.");
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException("Customer not found.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine("Database error: " + sqlEx.Message);
                ConsoleColorHelper.ResetColor();

            }
            catch (UnauthorizedAccessException authEx)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(authEx.Message);
                ConsoleColorHelper.ResetColor();
            }
            catch (InvalidOperationException opEx)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(opEx.Message);
                ConsoleColorHelper.ResetColor();
            }

            catch (System.Exception ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
                ConsoleColorHelper.ResetColor();
            }

            return customer;
        }

        public Customer AddCustomer(Customer customerReg)
        {
            Customer newCustomer = null;
            try
            {
                using(SqlConnection conn = GetDBConn.GetConnection())
                {
                    string query = "insert into Customer (CustomerName, Email,PhoneNumber,Password) values (@CustomerName,@Email,@PhoneNumber,@Password) select SCOPE_IDENTITY();";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@CustomerName", customerReg.CustomerName);
                    command.Parameters.AddWithValue("@Email", customerReg.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", customerReg.PhoneNumber);
                    command.Parameters.AddWithValue("@Password", customerReg.Password);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        int customerID = Convert.ToInt32(result);
                        newCustomer = GetCustomerById(customerID);
                    }
                    else
                    {
                        throw new System.Exception("Unexpected error occur");
                    }

                }
            }catch(System.Exception ex)
            {
                ConsoleColorHelper.SetErrorColor();
                Console.WriteLine(ex.Message);
                ConsoleColorHelper.ResetColor();
            }
            return newCustomer;
        }
    }
}

