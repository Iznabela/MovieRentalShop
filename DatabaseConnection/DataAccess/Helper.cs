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
    }
}
