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
                CREATE TABLE Users (
    UserID INT PRIMARY KEY,  -- Manually insert unique IDs
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(15),
    Role NVARCHAR(20) CHECK (Role IN ('Employee', 'Student')) NOT NULL,
    Department NVARCHAR(50),
    DesignationOrCourse NVARCHAR(100),
    Password NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);
CREATE TABLE Admins (
    AdminID INT PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Username NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(15),
    CreatedAt DATETIME DEFAULT GETDATE()
);
CREATE TABLE Attendance (
    AttendanceID INT PRIMARY KEY,
    UserID INT NOT NULL,
    AttendanceDate DATE NOT NULL,
    Status NVARCHAR(10) CHECK (Status IN ('Present', 'Absent', 'Leave')) NOT NULL,
    MarkedByAdminID INT NOT NULL,
    Timestamp DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (MarkedByAdminID) REFERENCES Admins(AdminID)
);
CREATE TABLE Notifications (
    NotificationID INT PRIMARY KEY,
    Message NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    TargetRole NVARCHAR(20) CHECK (TargetRole IN ('Employee', 'Student', 'All')) DEFAULT 'All'
);
CREATE TABLE LoginLogs (
    LogID INT PRIMARY KEY,
    UserOrAdmin NVARCHAR(10) CHECK (UserOrAdmin IN ('User', 'Admin')),
    AccountID INT NOT NULL,
    LoginTime DATETIME DEFAULT GETDATE(),
    LogoutTime DATETIME
);

               
            ";

            SqlCommand cmd = new SqlCommand(createTables, con);
            cmd.ExecuteNonQuery();

            Console.WriteLine("All tables created successfully in Attendence_Management database.");
            con.Close();
        }
    }
}
