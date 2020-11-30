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

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            using var ctx = new Context();
            var customer = ctx.Customers.Where(c => c.UserName == userNameText.Text).FirstOrDefault();            

            State.User = API.GetCustomerByUserName(userNameText.Text.Trim());

            if (State.User != null)
            {
                if (passwordText.Password == customer.Password)
                {
                    var next_window = new MainWindow();
                    next_window.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password did not match user - please try again!");
                    passwordText.Password = "...";
                }
            }
            else
            {
                MessageBox.Show("User not found - please create an account!");
                userNameText.Text = "...";
                passwordText.Password = "...";
            }
        }

        private void registerButton_click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
            
        }
    }
}
