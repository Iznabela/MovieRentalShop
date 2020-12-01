using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseConnection.DataAccess
{
    public static class Helper
    {
        public static string BellaConnString = @"server=.; database=SaleDatabase; trusted_connection=true;MultipleActiveResultSets=True";
        public static string FredrikConnString = @"Data Source=(localdb)\MSSQLLocalDB;Database = SaleDatabase; Trusted_Connection = Yes";

        public static string movieData = @"C:\Users\fredr\source\repos\MovieRentalShop\DatabaseConnection\ImportData\Movies.csv";
        public static string customerData = @"C:\Users\fredr\source\repos\MovieRentalShop\DatabaseConnection\ImportData\Customers.csv";

        public static string bellaMovieData = @"C:\Users\isabe\source\repos\ProjektHyrfilm\DatabaseConnection\ImportData\Movies.csv";
        public static string bellaCustomerData = @"C:\Users\isabe\source\repos\ProjektHyrfilm\DatabaseConnection\ImportData\Customers.csv";

        public static string priceData = @"C:\Users\fredr\source\repos\MovieRentalShop\DatabaseConnection\ImportData\Price.csv";
        public static string bellaPriceData = @"C:\Users\isabe\source\repos\ProjektHyrfilm\DatabaseConnection\ImportData\Price.csv";
    }
}
