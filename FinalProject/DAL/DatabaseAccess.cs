﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinalProject.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using FinalProject.App.Main.ThucDon;
using System.Drawing;
using System.Net;

namespace FinalProject.DAL
{
    internal class DatabaseAccess
    {
        String strConn = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
        public string checkLoginData_DA_DAL(User tk)
        {
            string userName = null;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "select * from LoginData where userName = @userName and userPassword = @userPassword";
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.AddWithValue("@userName", tk.userName);
            cmd.Parameters.AddWithValue("@userPassword", tk.userPassword);
            cmd.Connection = conn;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userName = reader.GetString(5);
                }
                reader.Close();
                conn.Close();
            }
            else
            {
                reader.Close();
                conn.Close();
                return "Thông tin đăng nhập không chính xác!";
            }
            return userName;
        }
        public string signUp_DA_DAL(User newUser)
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "select * from LoginData where userName = @userName";
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.AddWithValue("@userName", newUser.userName);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                String anotherStringSQL = "exec InsertUserLoginData @fullName, @emailAddress, @contactAddress, @phoneNumber, @userName, @userPassword";
                SqlCommand anotherCmd = new SqlCommand(anotherStringSQL, conn);
                anotherCmd.Parameters.AddWithValue("@fullName", newUser.fullName);
                anotherCmd.Parameters.AddWithValue("@emailAddress", newUser.emailAddress);
                anotherCmd.Parameters.AddWithValue("@contactAddress", newUser.contactAddress);
                anotherCmd.Parameters.AddWithValue("@phoneNumber", newUser.phoneNumber);
                anotherCmd.Parameters.AddWithValue("@userName", newUser.userName);
                anotherCmd.Parameters.AddWithValue("@userPassword", newUser.userPassword);
                anotherCmd.ExecuteNonQuery();
                conn.Close();
                return "Đăng ký tài khoản thành công";
            }
            else
            {
                conn.Close();
                return "Tài khoản đã được đăng ký trước đây";
            }
        }
        public DataTable populateMenuData_Combo_DA_DAL()
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "select * from MenuData where dishType = 'combo'";
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        // populateMenuData_Food_DA_DAL()
        // populateMenuData_Drink_DA_DAL()
        public DataTable populatePromotionData_DA_DAL()
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "select * from PromotionData";
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public DataTable populateStoreAddressData_DA_DAL()
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "select * from StoreAddress";
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        //Data admin page user
        public DataTable getAllUser_DA_DAL()
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "select * from LoginData";
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public DataTable searchUser_DA_BLL(String key)
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            DataTable dt = new DataTable();
            if (key==null || key.Equals(""))
            {
                String sSQL = "select * from LoginData";
                SqlCommand cmd = new SqlCommand(sSQL, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            else
            {
                String sSQL = "select * from LoginData Where userID=@key";
                SqlCommand cmd = new SqlCommand(sSQL, conn);
                cmd.Parameters.AddWithValue("@key", key);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            conn.Close();
            return dt;
        }
        public void deleteUser_DA_BLL(String id)
        {
            MessageBox.Show("Delete id :"+id);
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "delete from LoginData where userID = @id";
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Successfuly");
        }
        public void updateUser_DA_BLL(User user)
        {
            MessageBox.Show("Update id :" + user.userID);
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "update LoginData set fullName=@name, emailAddress=@email, contactAddress=@contact, phoneNumber=@phone,userName=@username, userPassword=@userpassword where userID=@id";
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.AddWithValue("@name", user.fullName);
            cmd.Parameters.AddWithValue("@email", user.emailAddress);
            cmd.Parameters.AddWithValue("@contact", user.contactAddress);
            cmd.Parameters.AddWithValue("@phone", user.phoneNumber);
            cmd.Parameters.AddWithValue("@username", user.userName);
            cmd.Parameters.AddWithValue("@userpassword", user.userPassword);
            cmd.Parameters.AddWithValue("@id", user.userID);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Successfuly");
        }
        public void addUser_DA_BLL(User user)
        {
            MessageBox.Show("add user :" + user.fullName);
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            if (user.userRole.Equals("user"))
            {
                String sSQL = "exec InsertUserLoginData @name,@email,@contact,@phone,@username,@userpassword";
                SqlCommand cmd = new SqlCommand(sSQL, conn);
                cmd.Parameters.AddWithValue("@name", user.fullName);
                cmd.Parameters.AddWithValue("@email", user.emailAddress);
                cmd.Parameters.AddWithValue("@contact", user.contactAddress);
                cmd.Parameters.AddWithValue("@phone", user.phoneNumber);
                cmd.Parameters.AddWithValue("@username", user.userName);
                cmd.Parameters.AddWithValue("@userpassword", user.userPassword);
                cmd.ExecuteNonQuery();
            }
            else
            {
                String sSQL = "exec InsertAdminLoginData @name,@email,@contact,@phone,@username,@userpassword";
                SqlCommand cmd = new SqlCommand(sSQL, conn);
                cmd.Parameters.AddWithValue("@name", user.fullName);
                cmd.Parameters.AddWithValue("@email", user.emailAddress);
                cmd.Parameters.AddWithValue("@contact", user.contactAddress);
                cmd.Parameters.AddWithValue("@phone", user.phoneNumber);
                cmd.Parameters.AddWithValue("@username", user.userName);
                cmd.Parameters.AddWithValue("@userpassword", user.userPassword);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            MessageBox.Show("Successfuly");
        }
        //StoreAddress
        public DataTable getAllStoreAdress()
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "select * from StoreAddress";
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        // REVENUE
        public DataTable getAllRevenue_DA_DAL()
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "select * from revenue";
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public DataTable getAllRevenueById_DA_DAL(String key)
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "select * from revenue where storeID=@id";
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.AddWithValue("@id", key);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public DataTable getAllRevenueByTime_DA_DAL(String start, String end,String id)
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "SELECT * FROM revenue WHERE dateCreate >= @start AND dateCreate <= @end AND storeID=@id";
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.Parameters.AddWithValue("@start", start);
            cmd.Parameters.AddWithValue("@end", end);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        // NOTIFICATION
        public DataTable getAllNotification_DA_DAL()
        {
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String sSQL = "SELECT * from notification";
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
    }
}