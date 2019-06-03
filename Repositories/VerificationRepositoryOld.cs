//using FontaineVerificationProject.Models;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FontaineVerificationProject.Repositories
//{
//    public class VerificationRepository
//    {
//        public static bool AddChassisToDB(Verification verification)
//        {
//            var connectionString = "Server=sage200-2016\\sql2014;Database=Fontaine;User Id=dash;Password=Chatburn441977";
//            var query = "INSERT INTO Verification (ChassisNo) VALUES ('@ChassisNo')";
//            query = query.Replace("@ChassisNo", verification.ChassisNo);
//            SqlConnection connection = new SqlConnection(connectionString);

//            try
//            {
//                connection.Open();
//                SqlCommand command = new SqlCommand(query, connection);
//                command.ExecuteNonQuery();
//                command.Dispose();
//                connection.Close();
//                return true;
//            }
//            catch (Exception)
//            {
//                //throw new Exception("Add New Chassis to the Database has failed");
//                return false;
//            }
//        }

//        public static bool DeleteChassisFromDB(Verification verification)
//        {
//            var connectionString = "Server=sage200-2016\\sql2014;Database=Fontaine;User Id=dash;Password=Chatburn441977";
//            var query = "DELETE FROM Verification WHERE ChassisNo = '@ChassisNo'";
//            query = query.Replace("@ChassisNo", verification.ChassisNo);
//            SqlConnection connection = new SqlConnection(connectionString);

//            try
//            {
//                connection.Open();
//                SqlCommand command = new SqlCommand(query, connection);
//                command.ExecuteNonQuery();
//                command.Dispose();
//                connection.Close();
//                return true;
//            }
//            catch (Exception)
//            {
//                //throw new Exception("Delete chassis from the Database has failed");
//                return false;
//            }
//        }

//        public static bool UpDateV1toDB(Verification verification)
//        {
//            var connectionString = "Server=sage200-2016\\sql2014;Database=Fontaine;User Id=dash;Password=Chatburn441977";
//            var query = "UPDATE Verification SET V1UserName = '@UserName', V1DateTime = CURRENT_TIMESTAMP, V1Passed = '@V1Passed' WHERE ChassisNo = '@ChassisNo'";
//            query = query.Replace("@ChassisNo", verification.ChassisNo)
//                .Replace("@UserName", verification.V1UserName)
//                .Replace("@V1Passed", verification.V1Passed);
//            SqlConnection connection = new SqlConnection(connectionString);

//            try
//            {
//                connection.Open();
//                SqlCommand command = new SqlCommand(query, connection);
//                command.ExecuteNonQuery();
//                command.Dispose();
//                connection.Close();
//                return true;
//            }
//            catch (Exception)
//            {
//                //throw new Exception("Updating the first verification process to the Database has failed");
//                return false;
//            }
//        }

//        public static bool UpDateV2toDB(Verification verification)
//        {
//            var connectionString = "Server=sage200-2016\\sql2014;Database=Fontaine;User Id=dash;Password=Chatburn441977";
//            var query = "UPDATE Verification SET V2UserName = '@UserName', V2DateTime = CURRENT_TIMESTAMP, V2Passed = '@V2Passed' WHERE ChassisNo = '@ChassisNo'";
//            query = query.Replace("@ChassisNo", verification.ChassisNo)
//                .Replace("@UserName", verification.V2UserName)
//                .Replace("@V2Passed", verification.V2Passed);
//            SqlConnection connection = new SqlConnection(connectionString);

//            try
//            {
//                connection.Open();
//                SqlCommand command = new SqlCommand(query, connection);
//                command.ExecuteNonQuery();
//                command.Dispose();
//                connection.Close();
//                return true;
//            }
//            catch (Exception)
//            {
//                //throw new Exception("Updating the second verification process to the Database has failed");
//                return false;
//            }
//        }

//        public static string[] GetChassisFromDB()
//        {
//            List<string> chassisNo = new List<string>();
//            var connectionString = "Server=sage200-2016\\sql2014;Database=Fontaine;User Id=dash;Password=Chatburn441977";
//            var query = "SELECT * FROM Verification";     
//            SqlConnection connection = new SqlConnection(connectionString);

//            try
//            {
//                connection.Open();
//                SqlCommand command = new SqlCommand(query, connection);

//                using (SqlDataReader dr = command.ExecuteReader())
//                {
//                    while (dr.Read())
//                    {
//                        var data = dr[0].ToString();
//                        chassisNo.Add(data);
//                    }
//                }

//                command.Dispose();
//                connection.Close();
//                string[] arr = chassisNo.ToArray();
//                return arr;
               
//            }
//            catch (Exception)
//            {
//                throw new Exception("Retrieving all chassis's from the Database has failed");            
//            }
//        }

//        public static string[] GetChassisById(Verification verification)
//        {          
//            List<string> chassisNo = new List<string>();
//            var connectionString = "Server=sage200-2016\\sql2014;Database=Fontaine;User Id=dash;Password=Chatburn441977";
//            var query = "SELECT ChassisNo FROM Verification  WHERE ChassisNo = '@ChassisNo'";
//            query = query.Replace("@ChassisNo", verification.ChassisNo);

//            SqlConnection connection = new SqlConnection(connectionString);

//            try
//            {
//                connection.Open();
//                SqlCommand command = new SqlCommand(query, connection);

//                using (SqlDataReader dr = command.ExecuteReader())
//                {
//                    while (dr.Read())
//                    {
//                        var data = dr[0].ToString();
//                        chassisNo.Add(data);
//                    }
//                }

//                command.Dispose();
//                connection.Close();
//                string[] arr = chassisNo.ToArray();
//                return arr;

//            }
//            catch (Exception)
//            {
//                throw new Exception("Retrieving all chassis's from the Database has failed");
//            }
//        }

        
//    }
//}
