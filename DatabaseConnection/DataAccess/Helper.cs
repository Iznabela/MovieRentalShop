using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseConnection.DataAccess
{
    public static class Helper
    {
        //Replace this string with your own.
        public static string ConnString = @"Data Source=(localdb)\MSSQLLocalDB;Database = SaleDatabaseTest; Trusted_Connection = Yes";

        //Bella Connection string: @"server=.; database=SaleDatabase; trusted_connection=true;MultipleActiveResultSets=True";
        //Fredrik Connction string: @"Data Source=(localdb)\MSSQLLocalDB;Database = SaleDatabaseTest; Trusted_Connection = Yes";

        public static string movieData = @"..\\netcoreapp3.1\\ImportData\\Movies.csv";
        public static string customerData = @"..\\netcoreapp3.1\\ImportData\\Customers.csv";
        public static string priceData = @"..\\netcoreapp3.1\\ImportData\\Price.csv";
    }
}
