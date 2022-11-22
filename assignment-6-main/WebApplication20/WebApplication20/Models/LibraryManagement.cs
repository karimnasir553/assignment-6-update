using System;

namespace WebApplication3.Models
{
    public class LibrarayManagement
    {
        public LibrarayManagement()
        {
            BookCheckIn = DateTime.Now;
            BookReceived = DateTime.Now;
        }
        public DateTime BookCheckIn { get; set; }
        public DateTime BookReceived { get; set; }
        public int userId { get; set; }
        

    }
}
