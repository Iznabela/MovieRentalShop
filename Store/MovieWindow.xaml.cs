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
        public static bool toCartButtonClicked = false;
        public MovieWindow(Movie movie)
        {
            InitializeComponent();

            // getting current title and printing as header
            string title = movie.Title.ToString();
            MovieBox.Header = title;

            // getting current poster and printing it in BoxGrid
            var image = new Image() { };
            image.Source = new BitmapImage(new Uri(movie.Poster));
            image.Height = 250;
            image.Margin = new Thickness(0, 5, 0, 5);
            BoxGrid.Children.Add(image);
            Grid.SetRow(image, 1);
            Grid.SetColumn(image, 0);
            Grid.SetColumnSpan(image, 3);

            Genre.Text = movie.Genre;
            Score.Text = "Score: " + movie.IMDBScore.ToString();

            var toCartButton = new Button
            {
                Margin = new Thickness(10, 10, 10, 10),
                Width = 80,
                Height = 25,
                BorderThickness = new Thickness(0),
                FontFamily = new FontFamily("Segoe UI Semibold"),
            };

            var cartButtonText = new TextBlock
            {
                Text = "Add to cart",
                FontSize = 12
            };

            toCartButton.Content = cartButtonText;

            toCartButton.Cursor = Cursors.Hand;
            toCartButton.Click += ToCartClick;

            BoxGrid.Children.Add(toCartButton);
            Grid.SetRow(toCartButton, 4);
            Grid.SetColumn(toCartButton, 0);
            Grid.SetColumnSpan(toCartButton, 1);
        }

        private void ToCartClick(object sender, RoutedEventArgs e)
        {
            toCartButtonClicked = true;
            MessageBox.Show("Added to your basket", "Visit the basket to checkout!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void MouseUpCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
