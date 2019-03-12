using System;
using System.Collections.Generic;
using DAL;
using Model;
using System.Diagnostics;

namespace BLL
{
    public class ContactManager
    {
        public ContactManager() { }
              
        //INSERTING Contact
        public static int InsertContact(Contact contact)
        {
            try
            {
                return ContactService.Insert(contact);
            }
            catch (Exception e)

            {

                throw new DataExceptions("Error when inserting contact", e);

            }

        }

        //UPDATING Contact
        public static int UpdateContact(Contact contact)
        {
            try
            {
                return ContactService.ContactUpdate(contact);
            }
            catch (Exception e)

            {

                throw new DataExceptions("Error when updating contact", e);

            }
        }
       
        //LOADING Contacts
        public static List<Contact> LoadContactsOnline()
        {
          
            try

            {
                Debug.WriteLine("BLL Loading Contacts");
                
                return ContactService.LoadContactsConnected();

            }

            catch (Exception e)

            {

                throw new DataExceptions("Error when listing contacts", e);

            }
        }

        //Search in DB Contact
        public static List<Contact> SearchContact()
        {
            try

            {
                return ContactService.SearchInfoInDB();
            }
            catch (Exception e)
            {
                throw new DataExceptions("Error when listing contacts", e);
            }
        }

        //DELETEING contact
        public static int DeleteContact(int idcontact)
        {
            try
            {
                return ContactService.Delete(idcontact);
            }
            catch (Exception e)

            {

                throw new DataExceptions("Error when deleting contact", e);

            }
        }

        //GETTING DUPLICATES
        public static int GetDuplicate(string firstname, string lastname)
        {
            try
            {
                return ContactService.GettingDuplicateContact(firstname, lastname);
            }
            catch (Exception e)

            {

                throw new DataExceptions("Error getting duplicate contact", e);

            }
        }
    }
}
