using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Attendence_Management.Data_Layer
{
    public class CrateTableDB
    {
        public void CreateTables()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-ODBH2U2\SQLEXPRESS;Initial Catalog=Attendence_Management;Integrated Security=True");
            con.Open();

            string createTables = @"
                CREATE TABLE admins (
                    admin_id INT IDENTITY(1,1) PRIMARY KEY,
                    name NVARCHAR(100) NOT NULL,
                    email NVARCHAR(100) UNIQUE NOT NULL,
                    password_hash NVARCHAR(255) NOT NULL
                );

                CREATE TABLE groups (
                    group_id INT IDENTITY(1,1) PRIMARY KEY,
                    group_name NVARCHAR(100) NOT NULL
                );

                CREATE TABLE users (
                    user_id INT IDENTITY(1,1) PRIMARY KEY,
                    name NVARCHAR(100) NOT NULL,
                    email NVARCHAR(100) UNIQUE NOT NULL,
                    role NVARCHAR(20) NOT NULL CHECK (role IN ('employee', 'student')),
                    group_id INT,
                    FOREIGN KEY (group_id) REFERENCES groups(group_id)
                );

                CREATE TABLE attendance (
                    attendance_id INT IDENTITY(1,1) PRIMARY KEY,
                    user_id INT,
                    date DATE NOT NULL,
                    status NVARCHAR(10) NOT NULL CHECK (status IN ('present', 'absent', 'leave')),
                    marked_by INT,
                    FOREIGN KEY (user_id) REFERENCES users(user_id),
                    FOREIGN KEY (marked_by) REFERENCES admins(admin_id)
                );

                CREATE TABLE logins (
                    login_id INT IDENTITY(1,1) PRIMARY KEY,
                    user_id INT,
                    login_time DATETIME NOT NULL,
                    logout_time DATETIME,
                    FOREIGN KEY (user_id) REFERENCES users(user_id)
                );

                CREATE TABLE leaves (
                    leave_id INT IDENTITY(1,1) PRIMARY KEY,
                    user_id INT,
                    start_date DATE NOT NULL,
                    end_date DATE NOT NULL,
                    reason NVARCHAR(MAX),
                    status NVARCHAR(20) DEFAULT 'pending' CHECK (status IN ('pending', 'approved', 'rejected')),
                    FOREIGN KEY (user_id) REFERENCES users(user_id)
                );

                CREATE TABLE notifications (
                    notification_id INT IDENTITY(1,1) PRIMARY KEY,
                    user_id INT,
                    message NVARCHAR(MAX) NOT NULL,
                    created_at DATETIME DEFAULT GETDATE(),
                    is_read BIT DEFAULT 0,
                    FOREIGN KEY (user_id) REFERENCES users(user_id)
                );
            ";

            SqlCommand cmd = new SqlCommand(createTables, con);
            cmd.ExecuteNonQuery();

            Console.WriteLine("All tables created successfully in Attendence_Management database.");
            con.Close();
        }
    }
}
