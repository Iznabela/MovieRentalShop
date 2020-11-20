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
using DatabaseConnection;

namespace Store
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            using var ctx = new Context();
            var customer = ctx.Customers.Where(c => c.UserName == UsernameField.Text).FirstOrDefault();            

            State.User = API.GetCustomerByUserName(UsernameField.Text.Trim());

            if (State.User != null)
            {
                if (PasswordField.Text == customer.Password)
                {
                    var next_window = new MainWindow();
                    next_window.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password did not match user - please try again!");
                    PasswordField.Text = "...";
                }
            }
            else
            {
                MessageBox.Show("User not found - please create an account!");
                UsernameField.Text = "...";
                PasswordField.Text = "...";
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }
    }
}
