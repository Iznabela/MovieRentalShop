using DatabaseConnection;
using DatabaseConnection.Models;
using System;
using System.Collections.Generic;
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

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
