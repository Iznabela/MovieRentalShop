using System;
using System.Collections.Generic;

namespace DatabaseConnection.Models
{

    public class Rental
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<Movie> Movies { get; set; } 
    }

}


