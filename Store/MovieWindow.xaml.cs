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
using DatabaseConnection.Models;

namespace Store
{
    /// <summary>
    /// Interaction logic for MovieWindow.xaml
    /// </summary>
    public partial class MovieWindow : Window
    {
        public MovieWindow()
        {
            InitializeComponent();

            // getting current title and printing as header
            string title = State.Pick.Title.ToString();
            MovieBox.Header = title;



            // getting current poster and printing it in BoxGrid
            var image = new Image() { };
            image.Source = new BitmapImage(new Uri(State.Pick.Poster));
            image.Height = 250;
            image.Margin = new Thickness(0, 5, 25, 5);
            BoxGrid.Children.Add(image);
            Grid.SetRow(image, 1);
            Grid.SetColumn(image, 0);
            Grid.SetColumnSpan(image, 4);

            Genre.Text = State.Pick.Genre;
            Score.Text = "Score: " + State.Pick.IMDBScore.ToString();

           

            Price.Text = State.Pick.Price.ToString() + " kr";
            
        }

        private void ToCartClick(object sender, RoutedEventArgs e)
        {
            State.PickedMovies.Add(State.Pick);
            MessageBox.Show("Added to your basket", "Visit the basket to checkout!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MouseUpCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
