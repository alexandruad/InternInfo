using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternInfo.InternInfoClass
{
    class InternInfoEntry
    {
        //getter and setter properties
        public string Company { get; set; }
        public string PositionProp { get; set; }
        public DateTime DateForApplication { get; set; }
        public DateTime DateForResponse { get; set; }
        public string Response { get; set; }
        public string FollowUp { get; set; }
        public string AdditionalInfo { get; set; }
        public string Active { get; set; }

        //DB
        static String myConnString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        //select
        public DataTable Select()
        {
            //DB connection
            SqlConnection conn = new SqlConnection(myConnString);
            DataTable dt = new DataTable();
            try
            {
                // SQL query
                string sql = "Select company, position, date_app, date_resp, response, followup, additionalInfo, active from internship";
                // Create cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Adapter
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);

            }
            catch (Exception e) { }
            finally { conn.Close(); }
            return dt;

        }

        //Inserting data => database
        public bool Insert(InternInfoEntry foo)
        {
            //basic return type, default = false
            bool isSucces = false;

            //DB connection
            SqlConnection conn = new SqlConnection(myConnString);
            try
            {
                //Create sql query
                string sql = "INSERT INTO internship (company, position, date_app, date_resp, response, followup, additionalInfo, active) " +
                             "VALUES(@company, @position, @date_app, @date_resp, @response, @followup, @additionalInfo, @active)";

                //create sql command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Create parameters to add data
                cmd.Parameters.AddWithValue("@company", foo.Company);
                cmd.Parameters.AddWithValue("@position", foo.PositionProp);
                cmd.Parameters.AddWithValue("@date_app ", foo.DateForApplication);
                cmd.Parameters.AddWithValue("@date_resp ", foo.DateForResponse);
                cmd.Parameters.AddWithValue("@response", foo.Response);
                cmd.Parameters.AddWithValue("@followUp", foo.FollowUp);
                cmd.Parameters.AddWithValue("@additionalInfo", foo.AdditionalInfo);
                cmd.Parameters.AddWithValue("@active", foo.Active);
                //Open connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //check if the query was succesfull
                if (rows > 0) isSucces = true;

            }
            catch (Exception e) { Console.WriteLine(e.StackTrace); }
            finally { conn.Close(); }
            return isSucces;
        }


        //Method to update data from our application
        public bool Update(InternInfoEntry foo)
        {
            bool isSuccess = false;
            //DB connection
            SqlConnection conn = new SqlConnection(myConnString);

            try
            {
                string sql = "UPDATE internship SET Company=@company, Position=@position, date_app=@date_app, date_resp=@date_resp, response=@response, followUp=@followUp, additionalinfo=@additionalInfo, active=@active " +
                             " WHERE company=@company";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@company", foo.Company);
                cmd.Parameters.AddWithValue("@position", foo.PositionProp);
                cmd.Parameters.AddWithValue("@date_app", foo.DateForApplication);
                cmd.Parameters.AddWithValue("@date_resp", foo.DateForResponse);
                cmd.Parameters.AddWithValue("@response", foo.Response);
                cmd.Parameters.AddWithValue("@followUp", foo.FollowUp);
                cmd.Parameters.AddWithValue("@additionalInfo", foo.AdditionalInfo);
                cmd.Parameters.AddWithValue("@active", foo.Active);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0) isSuccess = true;

            }
            catch (Exception e) { Console.Write(e.StackTrace); }
            finally { conn.Close(); }





            return isSuccess;
        }

        public bool Delete(InternInfoEntry foo)
        {
            bool isSuccess = false;
            //DB connection
            SqlConnection conn = new SqlConnection(myConnString);
            try
            {
                string sql = "DELETE FROM internship WHERE company=@company ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@company", foo.Company);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0) isSuccess = true;
            }
            catch (Exception e) { }
            finally { conn.Close(); }
            return isSuccess;
        }

    }   
    }

