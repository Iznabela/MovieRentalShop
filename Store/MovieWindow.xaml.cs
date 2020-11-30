﻿using System;
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
        public MovieWindow(Movie movie)
        {
            InitializeComponent();

            // getting current title and printing as header
            string title = movie.Title.ToString();
            MovieBox.Header = title;

            // getting current poster and printing it in BoxGrid
            var image = new Image() { };
            image.Source = new BitmapImage(new Uri(movie.Poster));
            image.Height = 180;
            image.Margin = new Thickness(5, 5, 5, 5);
            BoxGrid.Children.Add(image);
            Grid.SetRow(image, 1);
            Grid.SetColumn(image, 0);
            Grid.SetColumnSpan(image, 3);

            Genre.Text = movie.Genre;


            State.PickedMovies.Add(movie);

            MessageBox.Show("Added to your basket", "Visit the basket to checkout!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
