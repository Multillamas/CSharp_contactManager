using Model;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;



namespace DAL
{
    public class ContactService
    {
        static string connStr = ConfigurationManager.ConnectionStrings["ConnectionNAMETHATYOUSETONAPPCONFIG"].ConnectionString;
       
      
        public ContactService() { }
       
        //INSERT Contact
        public static int Insert(Contact contact)

        {
           
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand dCmd = new SqlCommand("ContactCreate", conn);
                dCmd.CommandType = CommandType.StoredProcedure;

                try

                {
                    dCmd.Parameters.AddWithValue("@firstName", contact.Firstname);
                    dCmd.Parameters.AddWithValue("@lastName", contact.Lastname);
                    dCmd.Parameters.AddWithValue("@phone", contact.Phone);
                    dCmd.Parameters.AddWithValue("@email", contact.Email);
                    dCmd.Parameters.AddWithValue("@address", contact.Address);

                    return dCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new DataExceptions("Error inserting contact", e);

                }

                finally

                {

                    dCmd.Dispose();

                    conn.Close();

                    conn.Dispose();

                }
            }
        }

        //UPDATE - SAVE
        public static int ContactUpdate(Contact contact)
        {

           
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand dCmd = new SqlCommand("contactUpdate", conn);

                dCmd.CommandType = CommandType.StoredProcedure;

                try

                {
                    dCmd.Parameters.AddWithValue("@idcontact", contact.Idcontact);
                    dCmd.Parameters.AddWithValue("@firstName", contact.Firstname);
                    dCmd.Parameters.AddWithValue("@lastName", contact.Lastname);
                    dCmd.Parameters.AddWithValue("@phone", contact.Phone);
                    dCmd.Parameters.AddWithValue("@email", contact.Email);
                    dCmd.Parameters.AddWithValue("@address", contact.Address);            
                    return dCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new DataExceptions("Error when trying to update contact", e);
                }
                finally
                {
                    dCmd.Dispose();

                    conn.Close();

                    conn.Dispose();

                }
            }
        }

        //DELETE Contact
        public static int Delete(int idcontact)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {


                conn.Open();


                SqlCommand dCmd = new SqlCommand("contactDelete", conn);

                dCmd.CommandType = CommandType.StoredProcedure;

                try

                {
                    dCmd.Parameters.AddWithValue("@idcontact", idcontact);
                    return dCmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new DataExceptions("Error when listing contacts", e);
                }
                finally
                {
                    dCmd.Dispose();
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        //GET ALL Contacts 
        public static List<Contact> LoadContactsConnected()
        {
            List<Contact> contactsList = new List<Contact>();
           
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select * from CONTACTS";
                 using (SqlDataReader reader = cmd.ExecuteReader())
                 {
                        try

                        {
                            while (reader.Read())
                            {
                                Contact contact = new Contact();
                                contact.Idcontact = Convert.ToInt32(reader["Idcontact"]);
                                contact.Firstname = reader["Firstname"].ToString();
                                contact.Lastname = reader["Lastname"].ToString();
                                contact.Phone = reader["Phone"].ToString();
                                contact.Email = reader["Email"].ToString();
                                contact.Address = reader["Address"].ToString();

                                contactsList.Add(contact);

                            }
                    
                        return contactsList;
                        }
                        catch (Exception e)
                        {
                    throw new DataExceptions("Error when listing contacts", e);
                        }
                        finally
                        {
                        conn.Close();
                        conn.Dispose();
                        }
                 }
                }
            }
        }

        //SEARCH INFO in DB
        public static List<Contact> SearchInfoInDB()
        {
            List<Contact> contactsList = new List<Contact>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * from CONTACTS WHERE firstname LIKE '% 0}%' OR lastname LIKE '%{0}%' OR phone LIKE '%{0}%'	OR email LIKE '%{0}%' OR address LIKE '%{0}%'";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        try

                        {
                            while (reader.Read())
                            {
                                Contact contact = new Contact();
                                contact.Idcontact = Convert.ToInt32(reader["Idcontact"]);
                                contact.Firstname = reader["Firstname"].ToString();
                                contact.Lastname = reader["Lastname"].ToString();
                                contact.Phone = reader["Phone"].ToString();
                                contact.Email = reader["Email"].ToString();
                                contact.Address = reader["Address"].ToString();

                                contactsList.Add(contact);

                            }
                           return contactsList;
                        }
                        catch (Exception e)
                        {
                            throw new DataExceptions("Error when listing contacts", e);
                        }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                        }
                    }
                }
            }
        }

        //GET Duplicate Contacts 
        public static int GettingDuplicateContact(string firstname, string lastname)
        {

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                Contact contact = new Contact();
                contact.Firstname = firstname;
                contact.Lastname = lastname;

                try
                {
                    SqlCommand check_ContactName = new SqlCommand("SELECT COUNT(*) FROM CONTACTS WHERE (firstname = @firstname AND lastname = @lastname )", conn);

                    check_ContactName.Parameters.AddWithValue("@firstname", contact.Firstname);
                    check_ContactName.Parameters.AddWithValue("@lastname", contact.Lastname);

                    int contactExist = (int)check_ContactName.ExecuteScalar();

                    return contactExist;

                }
                catch (Exception e)
                {
                    throw new DataExceptions("Error impossible to execute command", e);
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }


            }
        }
    }
}
