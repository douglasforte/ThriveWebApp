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
        SqlConnection con;
        public string message;

        #region constructor

        public DAO()
        {
            con = new SqlConnection(WebConfigurationManager.ConnectionStrings["conString"].ConnectionString);
        }

        #endregion

        #region User

        public int InsertUser(UserModel user)
        {
            int count = 0;
            SqlCommand cmd = new SqlCommand("uspInsertUserTable", con);
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

            //int count = 0;
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = @"INSERT INTO dbo.tblUser (FirstName, LastName, Email, Pass, Addres, City, County, Country, Phone) VALUES(@firstName,@lastName,@email,@pass,@addres,@city,@county,@country,@phone)";

            //cmd.Parameters.AddWithValue("@firstName", user.FirstName);
            //cmd.Parameters.AddWithValue("@lastName", user.LastName);
            //cmd.Parameters.AddWithValue("@email", user.Email);
            //user.Password = Crypto.HashPassword(user.Password);
            //cmd.Parameters.AddWithValue("@pass", user.Password);
            //cmd.Parameters.AddWithValue("@addres", user.Address);
            //cmd.Parameters.AddWithValue("@city", user.City);
            //cmd.Parameters.AddWithValue("@county", user.County);
            //cmd.Parameters.AddWithValue("@country", user.Country);
            //cmd.Parameters.AddWithValue("@phone", user.Phone);

            try
            {
                con.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                con.Close();
            }

            return count;
        }

        public string CheckLogin(UserModel user)
        {
            string password, firstName = null;
            SqlDataReader reader;
            SqlCommand cmd = new SqlCommand("uspCheckLogin", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@email", user.Email);
            try
            {
                con.Open();
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
                con.Close();
            }

            return firstName;
        }

        #endregion

        #region PhoneCase

        public int InsertPhonecase(PhoneCase phoneCase)
        {
            //int count = 0;
            //SqlCommand cmd = new SqlCommand("uspInsertPhoneCase", con);
            //cmd.CommandType = CommandType.StoredProcedure;

            int count = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"INSERT INTO dbo.PhoneCase (PhoneCaseName, PhoneCaseModel, Quantity) VALUES (@phoneCaseName,@phoneCaseModel,@quantity";
            
            cmd.Parameters.AddWithValue("@phoneCaseName", phoneCase.PhoneCaseName);
            cmd.Parameters.AddWithValue("@phoneCaseModel", phoneCase.ProductModel);
            cmd.Parameters.AddWithValue("@quantity", phoneCase.Quantity);
            try
            {
                con.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                con.Close();
            }

            return count;
        }

        public List<PhoneCase> ShowAllPhoneCases()
        {
            List<PhoneCase> phoneCasesList = new List<PhoneCase>();
            SqlDataReader reader;
            //SqlCommand cmd;
            //cmd = new SqlCommand("uspAllPhoneCase", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"SELECT * FROM dbo.PhoneCase";

            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                //    PhoneCase phoneCase = new PhoneCase();
                //    phoneCase.PhoneCaseName = Enum.Parse(typeof(ProductNameEnum))reader[0].ToString();
                //    phoneCase.ProductModel = Enum.Parse(reader[1].ToString());
                //    phoneCase.Quantity = Enum.TryParse(reader[2].ToString());
                //    phoneCasesList.Add(phoneCase);
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                con.Close();
            }

            return phoneCasesList;
        }

        #endregion

        #region OrderTable

        public int OrderTable(string email, ProductNameEnum productName)
        {
            //int count = 0;
            //SqlCommand cmd = new SqlCommand("uspInsertOrderTable", con);
            //cmd.CommandType = CommandType.StoredProcedure;

            int count = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"INSERT INTO PhoneCase (Email, PhoneCaseName) VALUES  (@email, @phoneCaseName)";
            
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@phoneCaseName", productName);
            try
            {
                con.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return count;
        }
        

        #endregion 
    }
}
