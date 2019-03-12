using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using BLL;
using System.Diagnostics;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Btn_Login(object sender, RoutedEventArgs e)
        {
          
            Window1 Contacts = new Window1();          
            int intResult = 0;
            UserManager userManager = new UserManager();
            User user = new User();
           
            try
            {

                if (txt_username.Text != "" && passwordBox.Password.ToString() != "")
                {
                    
                    user.Username = txt_username.Text;
                    user.Userpassword = passwordBox.Password.ToString();
                    intResult = userManager.CheckUser(user);
                    Debug.WriteLine("user.Username on Btn_submit inside try : " + user.Username);
                    Debug.WriteLine("user.Username on Btn_submit inside try : " + user.Userpassword);
                    Debug.WriteLine("user.Username on Btn_submit inside try : " + intResult);
                    if(intResult == 0 || intResult <0)
                        MessageBox.Show("Wrong password or username");
                    else
                    {
                        this.Hide();
                        Contacts.Show();
                    }
                }
                else if (txt_username.Text == "" || passwordBox.Password.ToString() == "")
                {
                    MessageBox.Show("Please fill all the information");
                    txt_username.Focus();

                }
                
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            }
            finally
            {
                user = null;
                userManager = null;
            }

        }

        private void Btn_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
