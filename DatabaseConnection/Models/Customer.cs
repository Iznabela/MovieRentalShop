using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseConnection.Models
{
    [Index(nameof(EmailAdress), nameof(UserName), IsUnique = true)]

    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }       
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAdress { get; set; }
        public virtual List<Rental> Rentals { get; set; }
    }
}
