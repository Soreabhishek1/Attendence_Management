using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Attendence_Management.Data_Layer
{
    public class DatabaseDB
    {
        public void CreateDatabase()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-ODBH2U2\SQLEXPRESS;Integrated Security=True");
            con.Open();

            string qur = "SELECT COUNT(*) FROM sys.databases WHERE name='Attendence_Management'";
            SqlCommand cmd = new SqlCommand(qur, con);
            int count = (int)cmd.ExecuteScalar();

            if (count > 0)
            {
                Console.WriteLine("Database already exists. Deleting...");
                string dropDb = "ALTER DATABASE Attendence_Management SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE Attendence_Management;";
                SqlCommand dropCmd = new SqlCommand(dropDb, con);
                dropCmd.ExecuteNonQuery();
                Console.WriteLine("Database deleted.");
            }

            SqlCommand createCmd = new SqlCommand("CREATE DATABASE Attendence_Management", con);
            createCmd.ExecuteNonQuery();
            Console.WriteLine("Database created successfully.");

            con.Close();
        }
    }
}