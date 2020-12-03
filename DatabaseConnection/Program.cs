using DatabaseConnection.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseConnection
{
    public class Program
    {
        static void Main()
        {
            //1. Change to your connection string in DataAccess\Helper.cs
            //2. Enter 'update-database' in Package Manager Console
            //3. Build solution
            //4. Start DataBaseConnection
            //5. Start Store

            var ctx = new Context();
            if (ctx.Customers.Count() == 0 && ctx.Movies.Count() == 0)
            {
                Seeding.ImportData(ctx);
                Seeding.ImportOrders(ctx);
                Seeding.AddPricesToDB(ctx);
            }
        }
    }
}
