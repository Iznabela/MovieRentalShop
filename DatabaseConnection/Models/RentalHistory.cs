using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseConnection.Models
{
    public class RentalHistory
    {
        public string MovieTitle { get; set; }
        public DateTime RentalDate { get; set; }
        public int DaysToReturn { get; set; }       
       
        
        public int ReturnDays(DateTime date)
        {
            var returnDate = date.AddDays(7);

            var daysleft = returnDate - DateTime.Now;

            return Convert.ToInt32(daysleft.Days.ToString());            
            
        }

        

    }

   
}
