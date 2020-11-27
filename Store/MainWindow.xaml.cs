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
            State.Movies = API.GetMovieSlice(0, 30);
            PrintPosters(MovieGrid);
        }

        // when hovering over a movie poster
        private void MovieBoxEnter(object sender, MouseEventArgs e)
        {
            var movieBox = (GroupBox)sender;
            movieBox.Height = 220;
            movieBox.Width = 160;
        }

        // when not hovering over a movie poster
        private void MovieBoxLeave(object sender, MouseEventArgs e)
        {
            var movieBox = (GroupBox)sender;
            movieBox.Height = 180;
            movieBox.Width = 120;
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
                Height = 750,
                Width = 700,
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

            groupBox.Content = stackpanel;

            var welcomeMessage = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = $"Hello {State.User.UserName}!",
                FontSize = 20,
                Margin = new Thickness(0, 20, 0, 0),
            };

            stackpanel.Children.Add(welcomeMessage);

            var historyMessage = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = $"Below you can see your history and currently rented movies.",
                FontSize = 20,
                Margin = new Thickness(0, 20, 0, 0),
            };

            stackpanel.Children.Add(historyMessage);

            stackpanel.Children.Add(textblock);



            var dataGrid = new DataGrid
            {
                Height = 500,
                Width = 650,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 20, 0, 0),
                DataContext = State.Movies ////ändra sedan, bara för test
            };

            DataGridTextColumn titleColumn = new DataGridTextColumn();               
            stackpanel.Children.Add(dataGrid);
            dataGrid.ItemsSource = API.rentalsHistory(State.User.Id);
            

        }




        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow.Children.Clear();

            var updatedMovieGrid = new Grid()
            {
                Height = 750,
                Width = 900,
                Name = "updatedMovieGrid",
                ShowGridLines = true
            };

            var scrollViewer = new ScrollViewer()
            {
                Content = updatedMovieGrid,
                BorderThickness = new Thickness(1),
            };

            HomeWindow.Children.Add(scrollViewer);

            for (int i = 0; i < 5; i++)
            {
                var columnDefinition = new ColumnDefinition()
                {
                    
                };

                var rowDefinition = new RowDefinition()
                {
                   
                };

                rowDefinition.Height = new GridLength(230);
                updatedMovieGrid.ColumnDefinitions.Add(columnDefinition);
                updatedMovieGrid.RowDefinitions.Add(rowDefinition);
            }

            PrintPosters(updatedMovieGrid);
        }

        // Printing movie posters in movie grid
        private void PrintPosters(Grid movieGrid)
        {
            //TODO 
            // create stackpanel to put groupbox and separate titletext in


            int i = 0;

            for (int y = 0; y < movieGrid.RowDefinitions.Count; y++)
            {
                for (int x = 0; x < movieGrid.ColumnDefinitions.Count; x++)
                {
                    if (i < State.Movies.Count)
                    {
                        var movie = State.Movies[i];
                        try
                        {
                            var image = new Image()
                            {
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Margin = new Thickness(0, 0, 0, 0),
                                Height = 170
                            };
                            image.Source = new BitmapImage(new Uri(movie.Poster));

                            var bigImage = new Image()
                            {
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center,
                                Margin = new Thickness(10, 10, 10, 10),
                                Height = 250
                            };
                            bigImage.Source = new BitmapImage(new Uri(movie.Poster));

                            image.MouseUp += Image_MouseUp;

                            var titleText = new TextBlock()
                            {
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Bottom,
                                Text = movie.Title.ToString(),
                                FontSize = 12
                            };

                            var popupTitleText = new TextBlock()
                            {
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Top,
                                Text = movie.Title.ToString()
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

                            var movieBox = new GroupBox()
                            {
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Height = 230,
                                Width = 170,
                            };

                            var movieStack = new StackPanel()
                            {
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Height = 230,
                                Width = 170
                            };

                            movieStack.Children.Add(image);

                            movieStack.Children.Add(titleText);

                            movieBox.Content = movieStack;

                            movieGrid.Children.Add(movieBox);

                            Grid.SetRow(movieBox, y);
                            Grid.SetColumn(movieBox, x);

                            movieBox.MouseEnter += MovieBoxEnter;
                            movieBox.MouseLeave += MovieBoxLeave;
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
    }
}
