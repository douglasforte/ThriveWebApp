using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Web.Helpers;

namespace ThriveWebApp.Models
{
    public class DAO
    {
        SqlConnection conn;
        public string message;

        #region constructor

        public DAO()
        {
           conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["conString"].ConnectionString);   
        }

        #endregion

        #region User
        public int InsertUser(UserModel user)
        {
            int count = 0;
              
            SqlCommand cmd = new SqlCommand("uspInsertUserTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@firstName", user.FirstName);
            cmd.Parameters.AddWithValue("@lastName", user.LastName);
            cmd.Parameters.AddWithValue("@email", user.Email);
            user.Password = Crypto.HashPassword(user.Password);
            cmd.Parameters.AddWithValue("@pass", user.Password);
            cmd.Parameters.AddWithValue("@addres", user.Address);
            cmd.Parameters.AddWithValue("@city", user.City);
            cmd.Parameters.AddWithValue("@county", user.County);
            cmd.Parameters.AddWithValue("@country", user.Country);
            cmd.Parameters.AddWithValue("@phone", user.Phone);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        
        public string CheckLogin(UserModel user)
        {
            string password, firstName = null;
            SqlDataReader reader;
            SqlCommand cmd = new SqlCommand("uspCheckLogin", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", user.Email);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    password = reader["Pass"].ToString();
                    if (Crypto.VerifyHashedPassword(password, user.Password))
                    {
                        firstName = reader["FirstName"].ToString();
                    }
                    else
                    {
                        message = "Passwords do not match";
                    }
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            catch (FormatException ex1)
            {
                message = ex1.Message;
            }
            finally
            {
                conn.Close();
            }

            return firstName;
        }
        #endregion
        
        #region PhoneCase

        public int InsertPhonecase(PhoneCase phoneCase)
        {
            int count = 0;
            SqlCommand cmd = new SqlCommand("uspInsertPhoneCase", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@productCode", phoneCase.ProductCode);
            cmd.Parameters.AddWithValue("@phoneCaseName", phoneCase.PhoneCaseName);
            cmd.Parameters.AddWithValue("@phoneCaseMakerModel", phoneCase.PhoneCaseMakerModel); 
            cmd.Parameters.AddWithValue("@phoneCasePrice", phoneCase.PhoneCasePrice);
            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return count;
        }

        public List<PhoneCase> ShowAllPhoneCases()
        {
            List<PhoneCase> phoneCasesList = new List<PhoneCase>();
            SqlDataReader reader;
            SqlCommand cmd;
            cmd = new SqlCommand("uspAllPhoneCase", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PhoneCase phoneCase = new PhoneCase();
                    phoneCase.ProductCode = reader["ProductCode"].ToString();
                    phoneCase.PhoneCaseName = reader["PhoneCaseName"].ToString();
                    phoneCase.PhoneCasePrice = decimal.Parse(reader["PhoneCasePrice"].ToString());
                    phoneCase.PhoneCaseImage = reader["PhoneCaseImage"].ToString();
                    phoneCasesList.Add(phoneCase);
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return phoneCasesList;
        }
        
        //public PhoneCase FindPhoneCase(string productCode)
        //{
        //    PhoneCase phoneCase = null;
        //    SqlDataReader reader;
        //    DataSet ds = new DataSet();
        //    SqlCommand cmd = new SqlCommand("uspFindPhoneCase", connection);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@productCode", productCode);

        //    try
        //    {
        //        connection.Open();
        //        reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            phoneCase = new PhoneCase();
        //            phoneCase.ProductCode = reader["ProductCode"].ToString();
        //            phoneCase.PhoneCaseMakerName = reader["PhoneCaseMakerName"].ToString();
        //            phoneCase.PhoneCaseMakerModel = reader["PhoneCaseMakerModel"].ToString();
        //            phoneCase.PhoneCasePrice = decimal.Parse(reader["PhoneCasePrice"].ToString());
        //            DataTable dt = ds.Tables[0];
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                instruction.Heading = row[0].ToString();
        //                instruction.Description = row[1].ToString();
        //                list.Add(instruction);
        //            }

        //            phoneCase.InstructionManual = list;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }

        //    return phoneCase;
        //}

        #endregion

        #region Transaction
        public int AddTransaction(string transactionId, DateTime transactionDate, decimal transactionPrice, string email)
        {
            int count = 0;
            SqlCommand cmd = new SqlCommand("uspInsertTransaction", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@transactionId", transactionId);
            cmd.Parameters.AddWithValue("@transactionDate", transactionDate);
            cmd.Parameters.AddWithValue("@transactionPrice", transactionPrice);
            cmd.Parameters.AddWithValue("@email", email);
            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return count;

        }

        public int AddTransactionItem(string transactionId, ItemModel item)
        {
            int count = 0;
            SqlCommand cmd = new SqlCommand("uspInsertTranscationItem", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@transactionId", transactionId);
            cmd.Parameters.AddWithValue("@ItemId", item.ItemId);
            cmd.Parameters.AddWithValue("@itemQuantity", item.Quantity);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        #endregion
    }
}