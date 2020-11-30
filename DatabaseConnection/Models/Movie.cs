using System.Collections.Generic;

namespace DatabaseConnection.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public int? imdbId { get; set; }        
        public string? IMDBLink { get; set; }
        public string? Title { get; set; }
        public double? IMDBScore { get; set; }
        public string? Genre { get; set; }
        public string? Poster { get; set; }
        public double? Price { get; set; }
        public virtual List<Rental> Rentals { get; set; }
    }
}


