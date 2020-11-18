using CsvHelper.Configuration;
using DatabaseConnection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseConnection.ClassMaps
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Map(c => c.FirstName).Name("FirstName");
            Map(c => c.LastName).Name("LastName");
            Map(c => c.UserName).Name("UserName");
            Map(c => c.Password).Name("Password");
            Map(c => c.EmailAdress).Name("EmailAdress");
        }
        

        
    }
}
