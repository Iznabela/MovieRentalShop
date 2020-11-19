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
            var ctx = new Context();
            if (ctx.Customers.Count() == 0 && ctx.Movies.Count() == 0)
            {
                Seeding.ImportData(ctx);
                Seeding.ImportOrders(ctx);
            }


        }
    }
}
