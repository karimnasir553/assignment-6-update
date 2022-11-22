
using WebApplication3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.DataLayer;



namespace WebApplication3.BusinessLayer

{
    public class penCalculatorcs
    {
        LibraryData libraryData=new LibraryData();

        public List<Books> GetBooksData()
        {
            return libraryData.GetData();
        }
        public List<User> GetUserData()
        {
            return libraryData.GetUserData();
        }
        public int CalculateAmount(DateTime BookCheckIn, DateTime BookReceived)
        {
            int amount = 0;
            int days = 0;
            int fineDays = 0;
            List<DateTime> holidays = new List<DateTime>();
            // Manually adding all holiday list.
            holidays.Add(new DateTime(DateTime.Now.Year, 1, 1));// New Year.

            DateTime issue = BookCheckIn;
            DateTime ret = BookReceived;
            var diffOfDates = BookReceived - BookCheckIn  ;
            int countdays = diffOfDates.Days;

            //calculations here
            while (countdays!=0)
            {
                if (issue.DayOfWeek != DayOfWeek.Saturday && issue.DayOfWeek != DayOfWeek.Sunday && !holidays.Contains(issue))
                {
                    days++; //counting working days between the issue and return date
                   
                }

                issue.AddDays(1);//incrementing date
                countdays--;
                

            }
            if (days > 15)//calculating fine
            {
               fineDays = days - 15;


                amount = 1 * fineDays;

            }

            
            return amount;//returning fine
        }

    }
}
