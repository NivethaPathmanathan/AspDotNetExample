using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AspDotNetExample.Models
{
    public class EmployeeDBAccessLayer
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        //  List<EmployeeEntities> employeeEntities = new List<EmployeeEntities>();
        //  using (SqlConnection con = new SqlConnection(connectionString))
        static string connectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        List<EmployeeEntities> employeeEntities = new List<EmployeeEntities>();
        SqlConnection con = new SqlConnection(connectionString);

        public string AddEmployeeRecord(EmployeeEntities employeeEntities)  
        {  
            try  
            {  
                SqlCommand cmd = new SqlCommand("sp_Employee_Add",con);  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@Emp_Name", employeeEntities.Emp_Name);  
                cmd.Parameters.AddWithValue("@City", employeeEntities.City);  
                cmd.Parameters.AddWithValue("@State", employeeEntities.State);  
                cmd.Parameters.AddWithValue("@Country", employeeEntities.Country);  
                cmd.Parameters.AddWithValue("@Department", employeeEntities.Department);  
                con.Open();  
                cmd.ExecuteNonQuery();  
                con.Close();  
                return ("Data save Successfully");  
            }  
            catch(Exception ex)  
            {  
                if(con.State==ConnectionState.Open)  
                {  
                    con.Close();  
                }  
                return (ex.Message.ToString());  
            }  
        }

        public EmployeeEntities GetEmployeeData(int? id)
        {
            EmployeeEntities employeeEntities = new EmployeeEntities();

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("sp_GetEmployeeByID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Emp_Id", id);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                   // EmployeeEntities.ID = Convert.ToInt32(sdr["Emp_Id"]);
                    EmployeeEntities.Emp_Name = sdr["Emp_Name"].ToString();
                    EmployeeEntities.City = sdr["City"].ToString();
                    EmployeeEntities.State = sdr["State"].ToString();
                    EmployeeEntities.Country = sdr["Country"].ToString();
                    EmployeeEntities.Department = sdr["Department"].ToString();
                }
            }
            return EmployeeEntities;
        }

        public void UpdateEmployee(EmployeeEntities employeeEntities)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Emp_Name", employeeEntities.Emp_Name);
                cmd.Parameters.AddWithValue("@City", employeeEntities.City);
                cmd.Parameters.AddWithValue("@State", employeeEntities.State);
                cmd.Parameters.AddWithValue("@Country", employeeEntities.Country);
                cmd.Parameters.AddWithValue("@Department", employeeEntities.Department);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}