using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseConnection.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnection
{
    public class API
    {
        public static List<Movie> GetMovieSlice(int a, int b)
        {
            using var ctx = new Context();
            return ctx.Movies.OrderBy(m => m.Title).Skip(a).Take(b).ToList();
        }
        public static Customer GetCustomerByName(string name)
        {
            using var ctx = new Context();
            return ctx.Customers.FirstOrDefault(c => c.FirstName.ToLower() == name.ToLower());
        }

        public static bool RegisterSale(Customer customer, List<Movie> movies)
        {
            using var ctx = new Context();
            try
            {
                // Här säger jag åt contextet att inte oroa sig över innehållet i dessa records.
                // Om jag inte gör detta så kommer den att försöka updatera databasens Id och cracha.
                ctx.Entry(customer).State = EntityState.Unchanged;
                //ctx.Entry(movies).State = EntityState.Unchanged;

                foreach (var movie in movies)
                {
                    ctx.Entry(movie).State = EntityState.Unchanged;
                }

                ctx.Add(new Rental() { Date = DateTime.Now, Customer = customer, Movies = movies });
                return ctx.SaveChanges() == 1;
            }
            catch (DbUpdateException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.InnerException.Message);
                return false;
            }
        }
    }

    //public static class API
    //{
    //    static Context ctx;

    //    static API()
    //    {
    //        ctx = new Context();
    //    }
    //    public static List<Movie> GetMovieSlice(int a, int b)
    //    {
    //        return ctx.Movies.OrderBy(m => m.Title).Skip(a).Take(b).ToList();
    //    }
    //    public static Customer GetCustomerByName(string name)
    //    {
    //        return ctx.Customers.FirstOrDefault(c => c.FirstName.ToLower() == name.ToLower());
    //    }
    //    public static bool RegisterSale(Customer customer, List<Movie> movie)
    //    {
    //        try
    //        {
    //            ctx.Add(new Rental() { Date = DateTime.Now, Customer = customer, Movies = movie });
    //            return ctx.SaveChanges() == 1;
    //        }
    //        catch (DbUpdateException e)
    //        {
    //            System.Diagnostics.Debug.WriteLine(e.Message);
    //            System.Diagnostics.Debug.WriteLine(e.InnerException.Message);
    //            return false;
    //        }
    //    }
    //}

}
