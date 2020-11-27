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
using DatabaseConnection;

namespace Store
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            int i = 0;

            State.Movies = API.GetMovieSlice(0, 30);
            for (int y = 0; y < MovieGrid.RowDefinitions.Count; y++)
            {
                for (int x = 0; x < MovieGrid.ColumnDefinitions.Count; x++)
                {
                    if (i < State.Movies.Count)
                    {
                        var movie = State.Movies[i];
                        try
                        {
                            var image = new Image()
                            {
                                Cursor = Cursors.Hand,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center,
                                Margin = new Thickness(0, 0, 0, 0),
                                Height = 150
                            };

                            var bigImage = new Image()
                            {
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center,
                                Margin = new Thickness(10, 10, 10, 10),
                                Height = 250
                            };

                            image.MouseUp += Image_MouseUp;
                            image.Source = new BitmapImage(new Uri(movie.Poster));
                            bigImage.Source = new BitmapImage(new Uri(movie.Poster));

                            var comboBox = new ComboBox()
                            {
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center,
                                Margin = new Thickness(3, 3, 3, 3),
                                Text = "INFO",
                                Height = 20,
                                Width = 100
                            };

                            var titleText = new TextBlock()
                            {
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Bottom,
                                Text = State.Movies[i].Title.ToString()
                            };

                            var buyButton = new Button()
                            {
                                HorizontalAlignment = HorizontalAlignment.Right,
                                VerticalAlignment = VerticalAlignment.Bottom,
                                Content = "Buy"
                            };

                            var infoButton = new Button()
                            {
                                HorizontalAlignment = HorizontalAlignment.Left,
                                VerticalAlignment = VerticalAlignment.Bottom,
                                Content = "Info"
                            };

                            var comboBoxPanel = new StackPanel()
                            {
                            };

                            comboBoxPanel.Children.Add(buyButton);
                            comboBoxPanel.Children.Add(infoButton);
                            comboBoxPanel.Children.Add(titleText);

                            comboBox.Items.Add(bigImage);
                            comboBox.Items.Add(comboBoxPanel);


                            var gridPanel = new StackPanel()
                            {
                                Name = "movieGridBox",
                                Height = 180,
                                Width = 120,
                            };
                            gridPanel.Children.Add(image);
                            gridPanel.Children.Add(comboBox);

                            comboBoxPanel.Orientation = Orientation.Horizontal;
                            comboBoxPanel.Margin = new Thickness(3, 3, 3, 3);

                            MovieGrid.Children.Add(gridPanel);

                            Grid.SetRow(gridPanel, y);
                            Grid.SetColumn(gridPanel, x);

                            gridPanel.MouseEnter += ImageMouseEnter;
                            gridPanel.MouseLeave += ImageMouseLeave;
                        }
                        catch (Exception e) when
                            (e is ArgumentNullException ||
                             e is System.IO.FileNotFoundException ||
                             e is UriFormatException)
                        {
                            continue;
                        }
                    }
                    i++;
                }
            }
        }

        // when hovering over a movie poster
        private void ImageMouseEnter(object sender, MouseEventArgs e)
        {
            var gridPanel = (StackPanel)sender;
            gridPanel.Height = 220;
            gridPanel.Width = 160;        
        }

        // when not hovering over a movie poster
        private void ImageMouseLeave(object sender, MouseEventArgs e)
        {
            var gridPanel = (StackPanel)sender;
            gridPanel.Height = 180;
            gridPanel.Width = 120;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var x = Grid.GetColumn(sender as UIElement);
            var y = Grid.GetRow(sender as UIElement);

            int i = y * MovieGrid.ColumnDefinitions.Count + x;
            State.Pick = State.Movies[i];

            if (API.RegisterSale(State.User, State.Movies))
                MessageBox.Show("All is well and you can download your movie now.", "Sale Succeeded!", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("An error happened while buying the movie, please try again at a later time.", "Sale Failed!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow.Children.Clear();

            var groupBox = new GroupBox
            {
                Name = "groupbox",
                Header = "Profile Information",
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(3),
                Height = 400,
                Width = 430,
                FontFamily = new FontFamily("Segoe Script")
            };

            var textblock = new TextBlock
            {
                Text = "Hej",
                FontSize = 20
            };
            
            var stackpanel = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            HomeWindow.Children.Add(groupBox);

            groupBox.Content = stackpanel;

            stackpanel.Children.Add(textblock);

            

            var moviesRented = new TextBlock
            {

            };
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow.Children.Clear();

            var updatedMovieGrid = new Grid()
            {
                Height = 750,
                Width = 700,
                Name = "updatedMovieGrid",
                ShowGridLines = true
            };

            HomeWindow.Children.Add(updatedMovieGrid);


            for (int i = 0; i < 5; i++)
            {
                var columnDefinition = new ColumnDefinition()
                {

                };

                var rowDefinition = new RowDefinition()
                {

                };

                updatedMovieGrid.ColumnDefinitions.Add(columnDefinition);
                updatedMovieGrid.RowDefinitions.Add(rowDefinition);
            }
        }
    }
}
