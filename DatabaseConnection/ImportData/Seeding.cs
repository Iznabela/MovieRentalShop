using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.EntityFrameworkCore;
using DatabaseConnection.Models;
using CsvHelper;
using System.Globalization;
using DatabaseConnection.DataAccess;
using DatabaseConnection.ClassMaps;
using System.Linq;

namespace DatabaseConnection
{
    public static class Seeding
    {
        public static void ImportData(Context ctx)
        {

            using (var reader = new StreamReader(Helper.movieData))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<MovieMap>();
                csv.Configuration.MissingFieldFound = null;
                var records = csv.GetRecords<Movie>();
                ctx.AddRange(records);
                ctx.SaveChanges();
            }
          

            using (var reader = new StreamReader(Helper.customerData))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.RegisterClassMap<CustomerMap>();
                var records = csv.GetRecords<Customer>();
                ctx.AddRange(records);
                ctx.SaveChanges();
            }            


            //ctx.RemoveRange(ctx.Rentals);
            //ctx.RemoveRange(ctx.Movies);
            //ctx.RemoveRange(ctx.Customers);

            //ctx.AddRange(new List<Customer> {
            //        new Customer { FirstName = "Björn" },
            //        new Customer { FirstName = "Robin" },
            //        new Customer { FirstName = "Kalle" },
            //    });

            //// Här laddas data in från SeedData foldern för att fylla ut Movies tabellen
            //var movies = new List<Movie>();
            //var lines = File.ReadAllLines(@"..\..\..\SeedData\MovieGenre.csv");
            //for (int i = 1; i < 200; i++)
            //{
            //    // imdbId,Imdb Link,Title,IMDB Score,Genre,Poster
            //    var cells = lines[i].Split(',');

            //    var url = cells[5].Trim('"');

            //    // Hoppa över alla icke-fungerande url:er
            //    try { var test = new Uri(url); }
            //    catch (Exception) { continue; }

            //    movies.Add(new Movie { Title = cells[2], Poster = url });
            //}
            //ctx.AddRange(movies);

            //ctx.SaveChanges();

        }


        public static void ImportOrders(Context ctx)
        {
            var random = new Random();            
            var cList = ctx.Customers.ToList();
            var mList = ctx.Movies.ToList();
            var date = new DateTime(2019, 11, 16);

            var firstCustomerID = ctx.Customers.OrderBy(c => c.Id).FirstOrDefault().Id;
            var lastCustomerID = ctx.Customers.OrderBy(c => c.Id).LastOrDefault().Id;
            var firstMovieId = ctx.Movies.OrderBy(m => m.Id).FirstOrDefault().Id;
            var lastMovieId = ctx.Movies.OrderBy(m => m.Id).LastOrDefault().Id;


            for (int i = 0; i < 500; i++)
            {
                var rMovie1 = random.Next(firstMovieId, lastMovieId);
                var rMovie2 = random.Next(firstMovieId, lastMovieId);

                var movie1 = ctx.Movies.Where(m => m.Id == rMovie1).FirstOrDefault();
                var movie2 = ctx.Movies.Where(m => m.Id == rMovie2).FirstOrDefault();
                var chosenMovies = new List<Movie>() { movie1, movie2 };

                var customerId = random.Next(firstCustomerID, lastCustomerID);
                var customer = ctx.Customers.Where(c => c.Id == customerId).Single();

                var nRental = new Rental
                {
                    Customer = customer,
                    Date = date.AddDays(random.Next(360)),
                    Movies = chosenMovies
                };
                ctx.Add(nRental);
                ctx.SaveChanges();

            }


        }
    }
}
