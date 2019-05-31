using FontaineVerificationProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FontaineVerificationProject.Repositories
{
    public class UserRepository
    {
        public static bool AddUserToDB(User user)
        {
            var connectionString = "Server=sage200-2016\\sql2014;Database=Fontaine;User Id=dash;Password=Chatburn441977";

            var query = "INSERT INTO Users (UserName,Password) VALUES ('@UserName', '@Password')";

            query = query.Replace("@UserName", user.UserName)
                .Replace("@Password", user.Password);

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                //throw new Exception("Add New User to the Database failed");
                return false;
            }
        }

        public static bool DeleteUserFromDB(User user)
        {
            var connectionString = "Server=sage200-2016\\sql2014;Database=Fontaine;User Id=dash;Password=Chatburn441977";

            var query = "DELETE FROM Users WHERE UserName = '@UserName' AND Password = '@Password'";

            query = query.Replace("@UserName", user.UserName)
                .Replace("@Password", user.Password);

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                //throw new Exception("Delete Existing User from the Database failed");
                return false;
            }
        }

        public static bool GetUserFromDB(User user)
        {
            var connectionString = "Server=sage200-2016\\sql2014;Database=Fontaine;User Id=dash;Password=Chatburn441977";

            var query = "SELECT UserName FROM Users";

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                //throw new Exception("Delete Existing User from the Database failed");
                return false;
            }
        }
    }
}
