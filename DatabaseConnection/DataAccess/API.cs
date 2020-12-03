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
        /// <summary>
        /// Initial movies on display in home window. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static List<Movie> GetMovieSlice(int a, int b)
        {
            return ctx.Movies.OrderBy(m => m.Id)
                .Skip(a)
                .Take(30)
                .ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>

        public static Customer GetCustomerByUserName(string userName)
        {
            return ctx.Customers
                .FirstOrDefault(c => c.UserName.ToLower() == userName.ToLower());
        }
        /// <summary>
        /// Method for registering the sale. 
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="movies"></param>
        /// <returns></returns>
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
        ///Method for creating a personalized rentalhistory. Unfortunantely a major bug atm
        ///when clearing the cart (state.pickedmovies) which in turn makes the history not
        ///being able to update. No solution as of yet since everything is logged in database
        ///correctly, its only this query that does not display the movies of the recent rentals.
        ///Closing the program and opening again shows the correct history.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IEnumerable rentalsHistory(Customer customer)
        {
            var rh = new List<RentalHistory>();
            var rentals = customer.Rentals;

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
        /// <summary>
        /// Method for searching for movies by the title.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static List<Movie> SearchFunction(string text)
        {
            var mList = new List<Movie>();
            mList = ctx.Movies.Where(m => m.Title.Contains(text)).Take(100).ToList();

            return mList;
        }
        /// <summary>
        /// Method for sorting movies by the genre
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
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
