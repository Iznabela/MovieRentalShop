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
        /// <summary>
        /// Initial seeding of the database.
        /// </summary>
        /// <param name="ctx"></param>
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
        }
        /// <summary>
        /// Importing the initial orders. Was meant for being able to sort by "most popular" among more
        /// but ran out of time.
        /// </summary>
        /// <param name="ctx"></param>
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
             
                var movie1 = mList.Where(m => m.Id == rMovie1).Single();
                var movie2 = mList.Where(m => m.Id == rMovie2).Single();
                var chosenMovies = new List<Movie>() { movie1, movie2 };

                var customerId = random.Next(firstCustomerID, lastCustomerID);
                var customer = cList.Where(c => c.Id == customerId).Single();

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

        /// <summary>
        /// Seeding the prices into the database seperately since they were not included in
        /// the Movies.csv file.
        /// </summary>
        /// <param name="ctx"></param>
        public static void AddPricesToDB(Context ctx)
        {
            var mListCount = ctx.Movies.Count();
            var mList = ctx.Movies.ToList();
            var pList = new List<Double>();
            int i = 0;

            using (var reader = new StreamReader(Helper.priceData))
            {
                while (i < mListCount)
                {
                    pList.Add(Convert.ToDouble(reader.ReadLine(), CultureInfo.InvariantCulture));
                    i++;

                    if (reader.EndOfStream)
                    {
                        reader.DiscardBufferedData();
                        reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    }
                }
            }

            for (int j = 0; j < mListCount; j++)
            {
                mList[j].Price = pList[j];
            }

            ctx.UpdateRange(mList);
            ctx.SaveChanges();        
        }
    }
}
