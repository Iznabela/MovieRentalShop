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
                .UseSqlServer(Helper.ConnString); //ÄNDRA BEROENDE PÅ VEM SOM ANSLUTER.         
                
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()                
                .Property(c => c.UserName)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .HasAlternateKey(c => c.UserName);

            modelBuilder.Entity<Customer>()
                .HasAlternateKey(c => c.EmailAdress);                

            modelBuilder.Entity<Customer>()
                .Property(c => c.Password)
                .IsRequired();

            



            
                
        }
    }
}
