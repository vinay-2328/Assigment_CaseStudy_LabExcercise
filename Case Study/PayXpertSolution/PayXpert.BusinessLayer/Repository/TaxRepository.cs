using PayXpert.Entity;
using PayXpert.Exception;
using PayXpert.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.BusinessLayer.Repository
{
    internal class TaxRepository : ITaxRepository
    {

        //Calculating tax of Employee with employee id and tax year
        public decimal CalculateTax(int employeeId, int taxYear)
        {
            decimal tax = 0;

            using (SqlConnection conn = DBConnUtil.GetConnection()) 
            {
                string query = "select TaxAmount from Tax where EmployeeID =@EmployeeID and TaxYear = @TaxYear";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@EmployeeID", employeeId); 
                command.Parameters.AddWithValue("@TaxYear", taxYear);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tax = Convert.ToDecimal(reader["TaxAmount"]);
                    }
                }
            }

            if(tax == 0)
            {
                throw new TaxCalculationException($"Failed to calculate tax for Employee ID: {employeeId} for the year {taxYear}.");
            }
            return tax;
        }


        //getting tax from database by taxID
        public Tax GetTaxById(int taxId)
        {
            Tax tax = null;
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "select * from Tax where TaxID =@TaxID";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@TaxID", taxId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read()) 
                    {
                        tax = new Tax
                        {
                            TaxID = Convert.ToInt32(reader["TaxID"]),
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            TaxYear = Convert.ToInt32(reader["TaxYear"]),
                            TaxableIncome = Convert.ToDecimal(reader["TaxableIncome"]),
                            TaxAmount = Convert.ToDecimal(reader["TaxAmount"])
                        };
                    }
                }
            }
            if (tax == null)
            {
                throw new System.Exception("Tax record not found.");
            }

            return tax;

        }


        //getting list of taxs from database of single employee using employee ID
        public IEnumerable<Tax> GetTaxesForEmployee(int employeeId)
        {
            List<Tax> taxes = new List<Tax>();
            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                string query = "select * from Tax where EmployeeID = @EmployeeID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeID", employeeId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        taxes.Add(new Tax
                        {
                            TaxID = Convert.ToInt32(reader["TaxID"]),
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            TaxYear = Convert.ToInt32(reader["TaxYear"]),
                            TaxableIncome = Convert.ToDecimal(reader["TaxableIncome"]),
                            TaxAmount = Convert.ToDecimal(reader["TaxAmount"])
                        });
                    }
                }
            }

            return taxes;
        }


        //Getting list of taxs from database of single year using tax year
        public IEnumerable<Tax> GetTaxesForYear(int taxYear)
        {
            List<Tax> taxes = new List<Tax>();
            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Tax WHERE TaxYear = @TaxYear";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TaxYear", taxYear);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        taxes.Add(new Tax
                        {
                            TaxID = Convert.ToInt32(reader["TaxID"]),
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            TaxYear = Convert.ToInt32(reader["TaxYear"]),
                            TaxableIncome = Convert.ToDecimal(reader["TaxableIncome"]),
                            TaxAmount = Convert.ToDecimal(reader["TaxAmount"])
                        });
                    }
                }
            }

            return taxes;
        }
    
    }

}

