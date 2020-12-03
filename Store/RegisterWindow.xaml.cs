using DatabaseConnection;
using DatabaseConnection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Store
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            using var ctx = new Context();
            var validateUserInput = true;
            var cList = ctx.Customers.ToList();


            if (rEmailAdressText.Text == null || string.IsNullOrWhiteSpace(rEmailAdressText.Text))
            {
                MessageBox.Show("Email adress is required.");
                validateUserInput = false;
            }

            if (ruserNameText.Text == null || string.IsNullOrWhiteSpace(ruserNameText.Text))
            {
                MessageBox.Show("Username is required.");
                validateUserInput = false;
            }

            if (rpasswordText.Password == null || string.IsNullOrWhiteSpace(rpasswordText.Password))
            {
                MessageBox.Show("Password is required.");
                validateUserInput = false;
            }

            if (validateUserInput)
            {

                foreach (var customer in cList)
                {
                    if (customer.EmailAdress == rEmailAdressText.Text)
                    {
                        MessageBox.Show("Email adress already exists.");
                        validateUserInput = false;
                    }

                    if (customer.UserName == ruserNameText.Text)
                    {
                        MessageBox.Show("Username already exists.");
                        validateUserInput = false;
                    }
                }
            }

            if (validateUserInput)
            {
                var nCustomer = new Customer
                {
                    FirstName = rFirstNameText.Text,
                    LastName = rLastNameText.Text,
                    EmailAdress = rEmailAdressText.Text,
                    UserName = ruserNameText.Text,
                    Password = rpasswordText.Password
                };

                ctx.Add(nCustomer);
                ctx.SaveChanges();

                MessageBox.Show("Your account is now registered. Welcome!");

                var loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            
            

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void registerButton2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
