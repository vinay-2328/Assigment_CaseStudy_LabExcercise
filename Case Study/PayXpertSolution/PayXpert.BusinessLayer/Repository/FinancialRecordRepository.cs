using PayXpert.Entity;
using PayXpert.Exception;
using PayXpert.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace PayXpert.BusinessLayer.Repository
{
    internal class FinancialRecordRepository : IFinancialRecordRepository
    {
        public void AddFinancialRecord(FinancialRecord financialRecord)
        {
            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                string query = "insert into FinancialRecord (EmployeeID, RecordDate, Description, Amount, RecordType) " +
                               "values (@EmployeeID, @RecordDate, @Description, @Amount, @RecordType)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeID", financialRecord.EmployeeID);
                command.Parameters.AddWithValue("@RecordDate", financialRecord.RecordDate);
                command.Parameters.AddWithValue("@Description", financialRecord.Description);
                command.Parameters.AddWithValue("@Amount", financialRecord.Amount);
                command.Parameters.AddWithValue("@RecordType", financialRecord.RecordType);

                int rowAffected = command.ExecuteNonQuery();

                if (rowAffected == 0)
                {
                    throw new FinancialRecordException("Error while adding financial record.");
                }
                else
                {
                    Console.WriteLine($"{rowAffected} records added successfully!");
                }

            }
        }
        public FinancialRecord GetFinancialRecordById(int recordId)
        {
            FinancialRecord record = null;
            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                string query = "select * from FinancialRecord where RecordID = @RecordID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RecordID", recordId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        record = new FinancialRecord
                        {
                            RecordID = Convert.ToInt32(reader["RecordID"]),
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            RecordDate = Convert.ToDateTime(reader["RecordDate"]),
                            Description = reader["Description"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            RecordType = reader["RecordType"].ToString()
                        };
                    }
                }
            }
            if(record == null )
            {
                throw new System.Exception("Financial record not found");
            }
            return record;
        }

        public IEnumerable<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId)
        {
            List<FinancialRecord> records = new List<FinancialRecord>();
            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                string query = "select * from FinancialRecord where EmployeeID = @EmployeeID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeID", employeeId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        records.Add(new FinancialRecord
                        {
                            RecordID = Convert.ToInt32(reader["RecordID"]),
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            RecordDate = Convert.ToDateTime(reader["RecordDate"]),
                            Description = reader["Description"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            RecordType = reader["RecordType"].ToString()
                        });
                    }
                }
            }
            if(records.Count == 0 )
            {
                throw new System.Exception("Financial records not found");
            }
            return records;
        }
        public IEnumerable<FinancialRecord> GetFinancialRecordForDate(DateTime recordDate)
        {
            List<FinancialRecord> records = new List<FinancialRecord>();
            using (SqlConnection connection = DBConnUtil.GetConnection())
            {
                string query = "select * from FinancialRecord WHERE RecordDate = @RecordDate";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RecordDate", recordDate);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        records.Add(new FinancialRecord
                        {
                            RecordID = Convert.ToInt32(reader["RecordID"]),
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            RecordDate = Convert.ToDateTime(reader["RecordDate"]),
                            Description = reader["Description"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            RecordType = reader["RecordType"].ToString()
                        });
                    }
                }
            }
            if (records.Count == 0)
            {
                throw new System.Exception($"No record found for date {recordDate}");
            }
            return records;
        }
    }
}
