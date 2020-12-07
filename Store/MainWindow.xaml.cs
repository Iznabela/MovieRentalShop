using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DatabaseConnection;
using DatabaseConnection.Models;

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
            State.Movies = API.GetMovieSlice(0, 60);
            HomeWindow.Children.Clear();
            PrintPosters(CreateMovieGrid());

            // Button events
            SortGenreButton.MouseEnter += SortGenreButton_MouseEnter;
            SortGenreButton.MouseLeave += SortGenreButton_MouseLeave;

            SearchButton.MouseEnter += SearchButton_MouseEnter;
            SearchButton.MouseLeave += SearchButton_MouseLeave;
        }

        // CREATE MOVIEGRID
        private Grid CreateMovieGrid()
        {
            var movieBorder = new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(10, 10, 10, 10)
            };

            var updatedMovieGrid = new Grid()
            {
                Width = 950,
                Name = "updatedMovieGrid",
                ShowGridLines = false
            };

            var scrollViewer = new ScrollViewer()
            {
                Content = updatedMovieGrid,
                Width = 950
            };

            movieBorder.Child = scrollViewer;

            HomeWindow.Children.Add(movieBorder);

            var column = 5;
            var row = (int)Math.Ceiling((double)State.Movies.Count() / (double)column);

            for (int j = 0; j < column; j++)
            {
                var columnDefinition = new ColumnDefinition()
                {

                };
                updatedMovieGrid.ColumnDefinitions.Add(columnDefinition);
            }

            for (int i = 0; i < row; i++)
            {
                var rowDefinition = new RowDefinition()
                {

                };

                rowDefinition.Height = new GridLength(230);

                updatedMovieGrid.RowDefinitions.Add(rowDefinition);
            }

            State.CurrentGrid = updatedMovieGrid;

            return updatedMovieGrid;
        }

        // PRINTING MOVIE POSTERS
        private void PrintPosters(Grid movieGrid)
        {
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
                            // adding MOVIE POSTERS
                            var image = new PosterImage()
                            {
                                X = x,
                                Y = y
                            };
                            image.Cursor = Cursors.Hand;
                            image.MouseUp += Image_MouseUp;
                            image.HorizontalAlignment = HorizontalAlignment.Center;
                            image.VerticalAlignment = VerticalAlignment.Center;
                            image.Source = new BitmapImage(new Uri(movie.Poster));
                            image.Height = 150;
                            image.Margin = new Thickness(0, 10, 0, 20);

                            var movieBorder = new Border
                            {
                                BorderBrush = Brushes.Black,
                                BorderThickness = new Thickness(2),
                                CornerRadius = new CornerRadius(7, 7, 7, 7)
                            };

                            // adding MOVIE TITELS
                            var titleBlock = new TextBlock
                            {
                                Text = State.Movies[i].Title,
                                FontFamily = new FontFamily("Segoe UI Semibold"),
                                FontSize = 12,
                                Margin = new Thickness(3, 3, 3, 3),
                                HorizontalAlignment = HorizontalAlignment.Center
                            };

                            var movieTitlePanel = new StackPanel();

                            movieTitlePanel.Children.Add(image);
                            movieTitlePanel.Children.Add(titleBlock);

                            movieBorder.Child = movieTitlePanel;

                            movieGrid.Children.Add(movieBorder);

                            image.MouseEnter += MovieBoxEnter;
                            image.MouseLeave += MovieBoxLeave;

                            Grid.SetRow(movieBorder, y);
                            Grid.SetColumn(movieBorder, x);
                            i++;
                        }
                        catch (Exception e) when
                            (e is ArgumentNullException ||
                             e is System.IO.FileNotFoundException ||
                             e is UriFormatException)
                        {
                            i++;
                            continue;
                        }
                    }
                }
            }
        }

        // CLICKING & HOVERING MOVIE POSTER
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
           
            var x = (sender as PosterImage).X;
            var y = (sender as PosterImage).Y;
            int i = y * State.CurrentGrid.ColumnDefinitions.Count() + x;

            State.Pick = State.Movies[i];

            var movieWindow = new MovieWindow();
            movieWindow.Show();
        }
        private void MovieBoxEnter(object sender, MouseEventArgs e)
        {
            var poster = (sender as PosterImage);
            poster.Height = 170;
        }
        private void MovieBoxLeave(object sender, MouseEventArgs e)
        {
            var poster = (sender as PosterImage);
            poster.Height = 150;
        }

        // SEARCH EVENTS
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            State.Movies = API.SearchFunction(searchTxt.Text);
            HomeWindow.Children.Clear();
            PrintPosters(CreateMovieGrid());
        }
        private void SearchButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Foreground = Brushes.Black;
        }
        private void SearchButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Foreground = Brushes.LightGray;
        }
        private void SearchTxt_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (searchTxt.Text == "Search for Title")
            {
                searchTxt.Text = string.Empty;
            }
        }

        // SORT EVENTS
        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            State.Movies = API.SortGenre(SortByGenre.SelectedValue);
            HomeWindow.Children.Clear();
            PrintPosters(CreateMovieGrid());
        }
        private void SortGenreButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Foreground = Brushes.Black;
        }
        private void SortGenreButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Foreground = Brushes.LightGray;
        }

        // SIDEBAR EVENTS
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow.Children.Clear();

            PrintPosters(CreateMovieGrid());
        }
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow.Children.Clear();

            var border = new Border
            {
                Name = "ProfileBorder",
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(3),
                Height = 400,
                Width = 700,
                CornerRadius = new CornerRadius(12, 12, 12, 12)
            };

            HomeWindow.Children.Add(border);

            var stackpanel = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            border.Child = stackpanel;

            var welcomeMessage = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Text = $"Hi {State.User.UserName.ToUpper()}!",
                FontSize = 18,
                Margin = new Thickness(47, 20, 0, 0),
                FontFamily = new FontFamily("Segoe UI Semibold"),
                FontStyle = FontStyles.Italic,
                Foreground = Brushes.Black
            };

            stackpanel.Children.Add(welcomeMessage);

            var historyMessage = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Text = $"Below you can see your history and currently rented movies.",
                FontSize = 18,
                Margin = new Thickness(47, 20, 0, 0),
                FontFamily = new FontFamily("Segoe UI Semibold"),
                Foreground = Brushes.Black,
                FontStyle = FontStyles.Italic
            };

            stackpanel.Children.Add(historyMessage);

            var dataGrid = new DataGrid
            {
                Height = 200,
                Width = 600,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 20, 0, 0),
                DataContext = State.Movies, 
                Foreground = Brushes.Black,
                Background = Brushes.GhostWhite,
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.Black,
            };

            stackpanel.Children.Add(dataGrid);
            dataGrid.ItemsSource = API.RentalsHistory(State.User);
        }
        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow.Children.Clear();

            var border = new Border
            {
                Name = "replaceGroupBoxInCart",
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(3),
                Height = 450,
                Width = 700,
                CornerRadius = new CornerRadius(15, 15, 15, 15)
            };

            HomeWindow.Children.Add(border);

            var stackpanel = new StackPanel
            {
                Name = "CartStack",
                Orientation = Orientation.Vertical
            };

            border.Child = stackpanel;

            var welcomeMessage = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = $"Hi {State.User.UserName.ToUpper()}! Current items in your cart.",
                FontSize = 18,
                Margin = new Thickness(0, 20, 0, 20),
                FontFamily = new FontFamily("Segoe UI"),
            };

            stackpanel.Children.Add(welcomeMessage);

            var lview = new ListView()
            {
                Name = "Lview",
                Height = 250,
                Width = 600,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,

            };

            stackpanel.Children.Add(lview);
            lview.SetValue(Grid.RowProperty, 2);

            GridView gv = new GridView();
            GridViewColumn gv1 = new GridViewColumn();
            gv1.DisplayMemberBinding = new Binding("Title");
            gv1.Header = "Title";
            gv.Columns.Add(gv1);
            gv1.Width = 550;

            GridViewColumn gv2 = new GridViewColumn();
            gv2.DisplayMemberBinding = new Binding("Price");
            gv2.Header = "Price";
            gv.Columns.Add(gv2);
            gv2.Width = 50;

            lview.ItemsSource = State.PickedMovies;
            lview.View = gv;

            // TOTAL PRICE of movies in cart
            double sum = State.PickedMovies.Sum(c => Convert.ToDouble(c.Price));

            var totalPrice = new TextBlock
            {
                Text = "Total Price: " + sum.ToString() + " kr",
                FontSize = 18,
                FontFamily = new FontFamily("Segoe UI"),
                Margin = new Thickness(0, 0, 50, 0),
                HorizontalAlignment = HorizontalAlignment.Right
            };

            stackpanel.Children.Add(totalPrice);

            // BUYBUTTON
            var buyButton = new Button
            {
                Height = 35,
                Width = 75,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Name = "BuyButton",
                Content = "Buy",
                FontSize = 16,
                Background = Brushes.Black,
                Foreground = Brushes.LightGray,
                BorderThickness = new Thickness(2),
                BorderBrush = Brushes.DarkGray,
                Margin = new Thickness(170, 40, 0, 0),
                Cursor = Cursors.Hand
            };

            // BUY BUTTON EVENTS
            buyButton.MouseEnter += BuyButton_MouseEnter;
            buyButton.MouseLeave += BuyButton_MouseLeave;
            buyButton.Cursor = Cursors.Hand;
            buyButton.Click += BuyButton_Click;

            // CLEAR BUTTON
            var clearButton = new Button
            {
                Height = 35,
                Width = 75,
                Name = "ClearButton",
                Content = "Clear",
                FontSize = 16,
                Background = Brushes.Black,
                Foreground = Brushes.LightGray,
                BorderThickness = new Thickness(2),
                BorderBrush = Brushes.DarkGray,
                Margin = new Thickness(170, 40, 0, 0),
                Cursor = Cursors.Hand,
            };

            // CLEAR BUTTON EVENTS
            clearButton.MouseEnter += new MouseEventHandler(ClearButton_MouseEnter);
            clearButton.MouseLeave += new MouseEventHandler(ClearButton_MouseLeave);
            clearButton.Cursor = Cursors.Hand;
            clearButton.Click += ClearButton_Click;

            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            buttonPanel.Children.Add(buyButton);
            buttonPanel.Children.Add(clearButton);

            stackpanel.Children.Add(buttonPanel);
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            State.User.FirstName = "";
            State.User.LastName = "";
            State.User.EmailAdress = "";
            State.User.UserName = "";
            State.User.Password = "";

            var login = new LoginWindow();
            login.Show();
            this.Close();
        }

        // CLEAR BUTTON EVENTS
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            // State.PickedMovies.Clear();
            var listLength = State.PickedMovies.Count;
            for (int i = 0; i < listLength; i++)
            {
                State.PickedMovies.RemoveAt(0);
            }
            CartButton_Click(sender, e);
        }
        private void ClearButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Foreground = Brushes.Black;
        }
        private void ClearButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Foreground = Brushes.LightGray;
        }

        // BUY BUTTON EVENTS
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            //Använd kod i varukorg
            API.RegisterSale(State.User, State.PickedMovies);
            MessageBox.Show("Purchase completed!", "You can now view the movies in your profile history!", MessageBoxButton.OK, MessageBoxImage.Information);            
            CartButton_Click(sender, e);

        }
        private void BuyButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Foreground = Brushes.Black;
        }
        private void BuyButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Foreground = Brushes.LightGray;
        }
    }
}
