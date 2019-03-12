using Model;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;

namespace DAL
{
    public class UserService
    {
        public UserService() { }
        string connStr = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;

        public int getUser(User user)
        {

            using (SqlConnection conn = new SqlConnection(connStr))
            {           
                try
                {
                    if(conn.State == ConnectionState.Closed)
                    
                        conn.Open();

                    string query = "SELECT Count(1) FROM USERS WHERE username = @username AND userpassword = @userpassword";
                    SqlCommand dCmd = new SqlCommand(query, conn);
                   
                    dCmd.Parameters.AddWithValue("@username", user.Username);
                    dCmd.Parameters.AddWithValue("@userpassword", user.Userpassword);
                    int count = Convert.ToInt32(dCmd.ExecuteScalar());
                    return count;
                }
                catch (Exception e)
                {
                    throw new DataExceptions("Error getting user", e);

                }
               
            }         
        }
    }
}
