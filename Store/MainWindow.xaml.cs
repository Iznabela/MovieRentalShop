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
            HomeWindow.Children.Clear();
            PrintPosters(CreateMovieGrid());            
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
            //Debugging, den känner inte av vilken kolumn den är i.
            //Kanske känner av i fel grid? 
            var x = (sender as PosterImage).X;
            var y = (sender as PosterImage).Y;
            int i = y * State.currentGrid.ColumnDefinitions.Count() + x;

            State.Pick = State.Movies[i];

            var movieWindow = new MovieWindow();
            movieWindow.Show();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow.Children.Clear();

            var border = new Border
            {
                Name = "ProfileBorder",
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(3),
                Height = 750,
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
                Height = 400,
                Width = 600,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 20, 0, 0),
                DataContext = State.Movies, ////ändra sedan, bara för test
                Foreground = Brushes.Black,
                Background = Brushes.GhostWhite,
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.Black
            };

            stackpanel.Children.Add(dataGrid);
            dataGrid.ItemsSource = API.rentalsHistory(State.User.Id);
        }


        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow.Children.Clear();

            State.Movies = API.GetMovieSlice(0, 30);

            PrintPosters(CreateMovieGrid());
        }

        private Grid CreateMovieGrid()
        {
            var movieBorder = new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(10,10,10,10)
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

            State.currentGrid = updatedMovieGrid;

            return updatedMovieGrid;
        }

        /// <summary>
        /// Metod för att komma till varukorgen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow.Children.Clear();

            var border = new Border
            {
                Name = "replaceGroupBoxInCart",
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(3),
                Height = 750,
                Width = 700,
                CornerRadius = new CornerRadius(15, 15, 15, 15)
            };

            var groupBox = new GroupBox
            {
                Name = "groupboxforcart",
                Header = "Cart",
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(3),
                Height = 750,
                Width = 700,
                FontFamily = new FontFamily("Segoe Script")
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
                HorizontalAlignment = HorizontalAlignment.Left,
                Text = $"Hi {State.User.UserName.ToUpper()}!",
                FontSize = 18,
                Margin = new Thickness(47, 20, 0, 0),
                FontFamily = new FontFamily("Segoe UI Semibold"),
            };

            stackpanel.Children.Add(welcomeMessage);

            var infoMessage = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Text = $"Below you can see your current cart.",
                FontSize = 18,
                Margin = new Thickness(47, 20, 0, 20),
                FontFamily = new FontFamily("Segoe UI Semibold"),
            };

            stackpanel.Children.Add(infoMessage);

            var lview = new ListView()
            {
                Name = "Lview",
                Height = 400,
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

            GridViewColumn gv2 = new GridViewColumn();
            gv2.DisplayMemberBinding = new Binding("Price");
            gv2.Header = "Price";
            gv.Columns.Add(gv2);          

            lview.ItemsSource = State.PickedMovies;
            lview.View = gv;

            // TOTAL PRICE of movies in cart
            double sum = State.PickedMovies.Sum(c => Convert.ToDouble(c.Price));

            var totalPrice = new TextBlock
            {
                Text = "Total Price: " + sum.ToString() + " kr",
                FontSize = 18,
                FontFamily = new FontFamily("Segoe UI Semibold"),
                Margin = new Thickness(47,0,10,0)
            };

            stackpanel.Children.Add(totalPrice);

            // BUYBUTTON with clickevent
            var buyButton = new Button
            {
                Height = 60,
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Name = "BuyButton",
                Content = "Buy",
                FontSize = 24,
                Background = Brushes.Black,
                Foreground = Brushes.LightGray,
                BorderThickness = new Thickness(2),
                Margin = new Thickness(170,40,0,0)
            };

            var clearButton = new Button
            {
                Height = 60,
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Name = "ClearButton",
                Content = "Clear",
                FontSize = 24,
                Background = Brushes.Black,
                Foreground = Brushes.LightGray,
                BorderThickness = new Thickness(2),
                Margin = new Thickness(170,40,0,0)
            };

            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            buttonPanel.Children.Add(buyButton);
            buttonPanel.Children.Add(clearButton);

            stackpanel.Children.Add(buttonPanel);

            // BUY BUTTON EVENTS
            buyButton.MouseEnter += BuyButton_MouseEnter;
            buyButton.MouseLeave += BuyButton_MouseLeave;
            buyButton.Cursor = Cursors.Hand;
            buyButton.Click += buybtn_Click;

            // CLEAR BUTTON EVENTS
            clearButton.MouseEnter += ClearButton_MouseEnter;
            clearButton.MouseLeave += ClearButton_MouseLeave;
            clearButton.Cursor = Cursors.Hand;
            clearButton.Click += ClearButton_Click;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            var listLength = State.PickedMovies.Count;
            for (int i = 0; i < listLength; i++)
            {
                State.PickedMovies.RemoveAt(0);
            }
            CartButton_Click(sender, e);
        }

        private void ClearButton_MouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Background = Brushes.Black;
        }

        private void ClearButton_MouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Background = Brushes.Red;
        }

        private void BuyButton_MouseLeave(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Background = Brushes.Black;
        }

        private void BuyButton_MouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Button;
            button.Background = Brushes.Red;
        }


        /// <summary>
        /// BuyButton for the Cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buybtn_Click(object sender, RoutedEventArgs e)
        {
            //Använd kod i varukorg
            if (API.RegisterSale(State.User, State.PickedMovies))
                MessageBox.Show("Added to your basket.", "Sale Succeeded!", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("An error happened while buying the movie, please try again at a later time.", "Sale Failed!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                            image.Margin = new Thickness(0, 20, 0, 20);

                            var movieBorder = new Border
                            {
                                BorderBrush = Brushes.Black,
                                BorderThickness = new Thickness(2),
                                CornerRadius = new CornerRadius(10,10,10,10)
                            };

                            // adding MOVIE TITELS
                            var titleBlock = new TextBlock
                            {
                                Text = State.Movies[i].Title,
                                FontFamily = new FontFamily("Segoe UI Semibold"),
                                FontSize = 12,
                                Margin = new Thickness(3,3,3,3),
                                HorizontalAlignment = HorizontalAlignment.Center
                            };


                            var movieTitlePanel = new StackPanel();

                            movieTitlePanel.Children.Add(image);
                            movieTitlePanel.Children.Add(titleBlock);

                            movieBorder.Child = movieTitlePanel;

                            movieGrid.Children.Add(movieBorder);

                           

                            Grid.SetRow(movieBorder, y);
                            Grid.SetColumn(movieBorder, x);
                            i++;
                        }
                        catch (Exception e) when
                            (e is ArgumentNullException ||
                             e is System.IO.FileNotFoundException ||
                             e is UriFormatException)
                        {
                            
                            continue;
                        }
                    }
                    


                }
            }
        }

        private void search_Btn_Click(object sender, RoutedEventArgs e)
        {
            State.Movies = API.SearchFunction(searchTxt.Text);
            HomeWindow.Children.Clear();
            PrintPosters(CreateMovieGrid());
        }

        private void sortGenre_Btn_Click(object sender, RoutedEventArgs e)
        {
            State.Movies = API.SortGenre(SortByGenre.SelectedValue);
            HomeWindow.Children.Clear();
            PrintPosters(CreateMovieGrid());
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

        private void searchTxt_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (searchTxt.Text == "Search for Title")
            {
                searchTxt.Text = string.Empty;
            }
        }
    }

    public class PosterImage : Image
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
