using DatabaseConnection.DataAccess;
using DatabaseConnection.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DatabaseConnection
{
    public class Context : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                //.LogTo(s => System.Diagnostics.Debug.WriteLine(s))
                .UseLazyLoadingProxies()
                .UseSqlServer(Helper.BellaConnString); //ÄNDRA BEROENDE PÅ VEM SOM ANSLUTAR.         
                
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
