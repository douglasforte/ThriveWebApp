using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Xml;
using System.IO;

namespace SampleTemplate.Models
{

    public class DAO
    {
        SqlConnection conn;
        public string message;

        #region constructor
        public DAO()
        {
            conn =
  new SqlConnection(WebConfigurationManager.ConnectionStrings["conString"].ConnectionString);
        }
        #endregion

        #region books
        public List<Book> ShowAllBooks()
        {
            List<Book> bookList = new List<Book>();

            SqlDataReader reader;
            //Creating an instance of SqlCommand 
            SqlCommand cmd;
            //Intialising SqlCommand
            cmd = new SqlCommand("uspAllBooks", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.ISBN = reader["ISBN"].ToString();
                    book.Title = reader["Title"].ToString();
                    book.Publisher = reader["Publisher"].ToString();
                    book.PublicationDate = DateTime.Parse(reader["DatePublished"].ToString());
                    book.Price = decimal.Parse(reader["Price"].ToString());
                    book.BookImage = reader["BookImage"].ToString();
                    bookList.Add(book);
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

            return bookList;
        }
        #endregion

        #region User
        public int InsertUser(UserModel user)
        {
            int count = 0;
            string password;
            SqlCommand cmd = new SqlCommand("uspInsertUserTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@first", user.FirstName);
            cmd.Parameters.AddWithValue("@last", user.LastName);
            cmd.Parameters.AddWithValue("@email", user.Email);
            password = Crypto.HashPassword(user.Password);
            cmd.Parameters.AddWithValue("@pass", password);
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

        #region Course
        public int InsertCourse(Course course)
        {
            int count = 0;
            SqlCommand cmd = new SqlCommand("uspInsertCourse", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@code", course.CourseCode);
            cmd.Parameters.AddWithValue("@title", course.Title);
            cmd.Parameters.AddWithValue("@type", course.Type);
            cmd.Parameters.AddWithValue("@price", course.Price);
            cmd.Parameters.AddWithValue("@itemType", "Course");
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

        public List<Course> ShowAllCourses()
        {
            List<Course> list = new List<Course>();
            SqlDataReader reader;
            SqlCommand cmd = new SqlCommand("uspAllCourses", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Course course = new Course();
                    course.CourseCode = reader[0].ToString();
                    course.Title = reader[1].ToString();
                    course.Type = reader[2].ToString();
                    course.Price = decimal.Parse(reader[3].ToString());
                    list.Add(course);
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

            return list;
        }
        #endregion

        #region Module
        public int InsertModule(Module module)
        {
            int count = 0;
            DataSet ds = GetSyllabus(module);
            SqlCommand cmd = new SqlCommand("uspINsertModule", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@code", module.ModuleCode);
            cmd.Parameters.AddWithValue("@title", module.ModuleTitle);
            cmd.Parameters.AddWithValue("@hours", module.Hours);
            cmd.Parameters.AddWithValue("@syllabus", ds.GetXml());
            cmd.Parameters.AddWithValue("@courseTitle", module.CourseTitle);
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

        public DataSet GetSyllabus(Module module)
        {
            DataSet ds = new DataSet("Syllabus");
            DataTable dt = new DataTable("Section");

            dt.Columns.Add("heading");
            dt.Columns.Add("description");
            ds.Tables.Add(dt);
            DataRow row;
            List<Section> list = module.Syllabus;
            foreach (Section section in list)
            {
                row = dt.NewRow();
                row["heading"] = section.Heading;
                row["description"] = section.Description;
                dt.Rows.Add(row);
            }
            ds.AcceptChanges();
            return ds;
        }

        public Module FindModule(string moduleCode)
        {
            Module module = null;
            SqlDataReader reader;
            List<Section> list = new List<Section>();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("uspFindModule", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@code", moduleCode);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    module = new Module();
                    module.ModuleCode = reader["ModuleCode"].ToString();
                    module.ModuleTitle = reader["ModuleTitle"].ToString();
                    module.Hours = int.Parse(reader["ModuleHours"].ToString());
                    module.CourseTitle = reader["CourseTitle"].ToString();
                    ds.ReadXml(new XmlTextReader(new StringReader(reader["ModuleSyllabus"].ToString())));
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        Section section = new Section();
                        section.Heading = row[0].ToString();
                        section.Description = row[1].ToString();
                        list.Add(section);
                    }
                    module.Syllabus = list;
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

            return module;

        }
        #endregion

        #region Transaction
        public int AddTransaction(string transactionId, DateTime date, decimal totalPrice, string email)
        {
            int count = 0;
            SqlCommand cmd = new SqlCommand("InsertTransactionTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", transactionId);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@price", totalPrice);
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
            SqlCommand cmd = new SqlCommand("uspTransactionItem", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@tranId", transactionId);
            cmd.Parameters.AddWithValue("@ItemId", item.ItemId);
            cmd.Parameters.AddWithValue("@quantity", item.Quantity);

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
