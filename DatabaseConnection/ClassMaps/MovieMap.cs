using CsvHelper.Configuration;
using DatabaseConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseConnection.ClassMaps
{
    class MovieMap : ClassMap<Movie>
    {
        public MovieMap()
        {
            Map(m => m.imdbId).Name("imdbId");
            Map(m => m.IMDBLink).Name("IMDBLink");
            Map(m => m.Title).Name("Title");
            Map(m => m.IMDBScore).Name("IMDBScore");
            Map(m => m.Genre).Name("Genre");
            Map(m => m.Poster).Name("Poster");
            Map(m => m.Price).Name("Price");
        }
    }

    
}
