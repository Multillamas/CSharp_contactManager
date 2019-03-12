using System;
using System.Windows;
using System.Windows.Controls;
using Model;
using BLL;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Diagnostics;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
       // ContactManager contactManager = new ContactManager();
        Contact contact = new Contact();
        List<Contact> contactlist = null;
        
        public Window1()
        {
            InitializeComponent();
            contactlist = ContactManager.LoadContactsOnline();
            dataGrid.ItemsSource = contactlist;                   
        }       

        private void Btn_quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }

        private void Btn_add_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.Items.Refresh();
            Btns_toOriginalBorder();
            int intResult = 0;
            Contact contact = new Contact();
            Regex R_phone = new Regex(@"^\(\d{3}\)\d{3}-\d{4}$");
            Regex R_mail = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            Regex R_names = new Regex(@"^[A-Za-z\\s]+$");

            try
            {
                if (txt_firstname.Text == "" || txt_lastname.Text == "" || txt_phone.Text == "")
                {
                    txt_firstname.BorderBrush = Brushes.Red;
                    txt_lastname.BorderBrush = Brushes.Red;
                    txt_phone.BorderBrush = Brushes.Red;
                    MessageBox.Show("You must enter a first name, a lastname and a phone number");
                }

                else
                {
                    int duplicateName = ContactManager.GetDuplicate(txt_firstname.Text, txt_lastname.Text);


                    if ( (duplicateName < 1) && R_names.IsMatch(txt_firstname.Text) && R_names.IsMatch(txt_lastname.Text) && R_phone.IsMatch(txt_phone.Text))
                    {
                        txt_firstname.BorderBrush = Brushes.Green;
                        txt_lastname.BorderBrush = Brushes.Green;
                        txt_phone.BorderBrush = Brushes.Green;
                        contact.Firstname = txt_firstname.Text;
                        contact.Lastname = txt_lastname.Text;
                        contact.Phone = txt_phone.Text;

                    }
                    else if(duplicateName >= 1)
                    {
                        MessageBox.Show("It seems your contact already exists. Please try to change the values of firstname and lastname before adding.");
                        Restart_Fields();
                    }
                    else if (!R_names.IsMatch(txt_firstname.Text))
                    {
                        txt_firstname.BorderBrush = Brushes.Red;
                        MessageBox.Show("Name and lastname should contain only letters.");
                    }
                    else if (!R_names.IsMatch(txt_lastname.Text))
                    {
                        txt_lastname.BorderBrush = Brushes.Red;
                        MessageBox.Show("Name and lastname should contain only letters.");
                    }
                    else if (!R_phone.IsMatch(txt_phone.Text))
                    {
                        txt_phone.BorderBrush = Brushes.Red;
                        txt_phone.Text = "(XXX)XXX-XXXX";
                        MessageBox.Show("Check the format requiered for telephone number");

                    }

                    if (txt_email.Text == "" || txt_email.Text != "" && R_mail.IsMatch(txt_email.Text))
                    {
                        contact.Email = txt_email.Text;
                    }
                    else
                    {
                        txt_email.BorderBrush = Brushes.Red;
                        txt_email.Text = "xxx@xxx.xxx";
                        MessageBox.Show("Check the format requiered for the email");
                    }
                    contact.Address = txt_address.Text;

                    intResult = ContactManager.InsertContact(contact);
                    if (intResult > 0)
                    {
                        dataGrid.Items.Refresh();
                        MessageBox.Show("New contact added");
                        contactlist = ContactManager.LoadContactsOnline();
                        dataGrid.ItemsSource = contactlist;
                       // 
                        Btns_toOriginalBorder();
                        Restart_Fields();
                    }
                    else
                    {
                        MessageBox.Show("Already exsists");
                    }
                }               
            }
            catch (Exception exeption)
            {
                //MessageBox.Show(exeption.Message);
            }
            finally
            {
                contact = null;
            }
        }

        private void Txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Btn_edit_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.Items.Refresh();
            Btns_toOriginalBorder();
            var lineSelected = dataGrid.SelectedItem;
            Contact selectedContact = lineSelected as Contact;

            if (selectedContact != null)
            {
                txt_firstname.Text = selectedContact.Firstname;
                txt_lastname.Text = selectedContact.Lastname;
                txt_phone.Text = selectedContact.Phone;
                txt_email.Text = selectedContact.Email;
                txt_address.Text = selectedContact.Address;
            }
        }

        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            contactlist = ContactManager.LoadContactsOnline();
            dataGrid.ItemsSource = contactlist;
            dataGrid.Items.Refresh();
            Contact contact = new Contact();
            var lineSelected = dataGrid.SelectedItem;
            Contact selectedContact = lineSelected as Contact;
            int id = 0;
            int intResult = 0;

            try
            {
                if (selectedContact.Idcontact != 0 || lineSelected != null )
                {
                    id = selectedContact.Idcontact;

                    TextBox tFN = txt_firstname;
                    TextBox tLastname = txt_lastname;
                    TextBox tPhone = txt_phone;
                    TextBox tEmail = txt_email;
                    TextBox tAddress = txt_address;

                    contact.Idcontact = selectedContact.Idcontact;
                    contact.Firstname = tFN.Text;
                    contact.Lastname = tLastname.Text;
                    contact.Phone = tPhone.Text;
                    contact.Email = tEmail.Text;
                    contact.Address = tAddress.Text;
                }
                else
                    MessageBox.Show("Select an existing contact first");



                intResult = ContactManager.UpdateContact(contact);

                if (intResult > 0)
                {
                    MessageBox.Show("Changes saved");
                    contactlist = ContactManager.LoadContactsOnline();
                    dataGrid.ItemsSource = contactlist;
                    dataGrid.Items.Refresh();
                    Btns_toOriginalBorder();
                    Restart_Fields();

                }
                else
                    MessageBox.Show("Record couldn't be updated");
            }
            catch 
            {
                MessageBox.Show("Changes can't be saved! You need to select an existing contact or you need to added to the contact list first.");
                
            }
            finally
            {
                contact = null;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            contactlist = ContactManager.LoadContactsOnline();
            int intResult = 0;
            Contact contact = new Contact();

            try
            {
                var lineSelected = dataGrid.SelectedItem;
                if (lineSelected == null)
                    MessageBox.Show("Please select a contact to delete");
                else
                {
                    Contact selectedContact = lineSelected as Contact;
                    int id = selectedContact.Idcontact;

                    intResult = ContactManager.DeleteContact(id);

                    if (intResult > 0)
                    {
                        MessageBox.Show("Contact deleted");
                        contactlist = ContactManager.LoadContactsOnline();
                        dataGrid.ItemsSource = contactlist;
                        dataGrid.Items.Refresh();
                        Restart_Fields();
                    }
                    else
                        MessageBox.Show("Failure to delete contact");
                }
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            }
            finally
            {
                contact = null;
            }
            contactlist = ContactManager.LoadContactsOnline();
        }
                        
        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            Btns_toOriginalBorder();
            txt_firstname.Clear();
            txt_lastname.Clear();
            txt_phone.Text = "(XXX)XXX-XXXX";
            txt_email.Clear();
            txt_address.Clear();
        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Btns_toOriginalBorder()
        {
            txt_firstname.BorderBrush = Brushes.LightGray;
            txt_lastname.BorderBrush = Brushes.LightGray;
            txt_phone.BorderBrush = Brushes.LightGray;
            txt_email.BorderBrush = Brushes.LightGray;
        }

        private void Restart_Fields()
        {
            Btns_toOriginalBorder();
            txt_firstname.Clear();
            txt_lastname.Clear();
            txt_phone.Text = "(XXX)XXX-XXXX";
            txt_email.Clear();
            txt_address.Clear();
        }
    }
}
 