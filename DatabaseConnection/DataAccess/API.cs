using System;
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
                bool one_record_added =  ctx.SaveChanges() == 1;
                return one_record_added;
            }
            catch (DbUpdateException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.InnerException.Message);
                return false;
            }
        }
    }   

}
