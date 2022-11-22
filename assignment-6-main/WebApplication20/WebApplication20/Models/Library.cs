namespace WebApplication3
{
    public class Books
    {


        public int bookID { get; set; }

        public int price { get; set; }
        public string bookName { get; set; }
        public string category { get; set; }
        public string shelfNumber { get; set; }

        public string bookIssuedStatus { get; set; }

    }

    public class User

    {
        public int userID { get; set; }
        public string userName {get;set;}

       public string bookIssuedList { get; set; }
    }

}
