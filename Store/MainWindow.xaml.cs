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
            for (int y = 3; y < MovieGrid.RowDefinitions.Count; y++)
            {
                for (int x = 1; x < MovieGrid.ColumnDefinitions.Count - 1; x++)
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
                                Margin = new Thickness(20, 20, 20, 20),
                                Height = 100
                            };

                            var bigImage = new Image()
                            {
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center,
                                Margin = new Thickness(10, 10, 10, 10),
                                Height = 150
                            };

                            image.MouseUp += Image_MouseUp;
                            image.Source = new BitmapImage(new Uri(movie.Poster));
                            bigImage.Source = new BitmapImage(new Uri(movie.Poster));

                            var comboBox = new ComboBox()
                            {
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center,
                                Margin = new Thickness(3, 3, 3, 3),
                                Text = "INFO"
                            };

                            var titleText = new TextBlock()
                            {
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Bottom,
                                Text = State.Movies[i].Title
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

                            var stackPanel = new StackPanel ()
                            {
                            };

                            var pictureStackPanel = new StackPanel()
                            {
                                Height = 160
                            };

                            pictureStackPanel.Children.Add(bigImage);

                            stackPanel.Children.Add(buyButton);
                            stackPanel.Children.Add(infoButton);
                            stackPanel.Children.Add(titleText);

                            comboBox.Items.Add(image);
                            comboBox.Items.Add(stackPanel);


                            var groupBox = new GroupBox()
                            {
                                Header = pictureStackPanel,
                                Content = comboBox,
                                Height = 180
                            };

                            stackPanel.Orientation = Orientation.Horizontal;
                            stackPanel.Margin = new Thickness(3, 3, 3, 3);
                                                        
                            MovieGrid.Children.Add(groupBox);

                            Grid.SetRow(groupBox, y);
                            Grid.SetColumn(groupBox, x);
                            
                            image.MouseEnter += ImageMouseEnter;
                            image.MouseLeave += ImageMouseLeave;
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
            MovieGrid.ShowGridLines = false;
        }

        // when not hovering over a movie poster
        private void ImageMouseLeave(object sender, MouseEventArgs e)
        {
            MovieGrid.ShowGridLines = true;
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
    }
}
