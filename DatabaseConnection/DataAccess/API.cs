using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseConnection.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnection
{
    public static class API
    {

        static Context ctx;

        static API()
        {
            ctx = new Context();
        }

        public static List<Movie> GetMovieSlice(int a, int b)
        {
            return ctx.Movies.OrderBy(m => m.Title)
                .Skip(a)
                .Take(b)
                .ToList();
        }

        public static Customer GetCustomerByUserName(string userName)
        {
            return ctx.Customers
                .FirstOrDefault(c => c.UserName.ToLower() == userName.ToLower());
        }

        public static bool RegisterSale(Customer customer, List<Movie> movies)
        {
            try
            {

                ctx.Add(new Rental() { Date = DateTime.Now, Customer = customer, Movies = movies });
                bool one_record_added = ctx.SaveChanges() == 1;
                return one_record_added;
            }
            catch (DbUpdateException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.InnerException.Message);
                return false;
            }
        }


        /// <summary>
        /// Metod för att skapa propertys i klassen RentalHistory för att
        /// skicka tillbaka data till DataGrid, kunna lista varje kunds historik. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IEnumerable rentalsHistory(int id)
        {


            var rh = new List<RentalHistory>();

            var rentals = ctx.Rentals
                .Where(m => m.Customer.Id == id)
                .Include(c => c.Movies)
                .ToList();


            foreach (var obj in rentals)
            {

                foreach (var item in obj.Movies)
                {
                    var rHistory = new RentalHistory();
                    rHistory.MovieTitle = item.Title;
                    rHistory.RentalDate = obj.Date;
                    rHistory.DaysToReturn = rHistory.ReturnDays(rHistory.RentalDate);
                    rh.Add(rHistory);
                }
            }

            return rh;

        }

        public static List<Movie> SearchFunction(string text)
        {
            var mList = new List<Movie>();
            mList = ctx.Movies.Where(m => m.Title.Contains(text)).Take(100).ToList();

            return mList;
        }

        public static List<Movie> SortGenre(object selectedValue)
        {
            var sortValue = selectedValue.ToString();
            var mList = new List<Movie>();

            if (sortValue == "All")
            {
                mList = ctx.Movies
                    .Take(100)
                    .ToList();
            }
            else
            {
                mList = ctx.Movies.Where(m => m.Genre.Contains(sortValue)).Take(100).ToList();

            }
            return mList;
        }
    }

}
