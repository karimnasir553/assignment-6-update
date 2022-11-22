using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication3.Models;
using Microsoft.Extensions.Configuration;


namespace WebApplication3.DataLayer

{
    public class LibraryData
    {

        public List<Books> booklist = new List<Books>();
        public List<User> usersList = new List<User>();
    
        public List<Books> GetData()//getting books data from sql database
        {
            List<Books> booksList = new List<Books>();
            //setting up string connection
            string ConnectionString = @"Data Source=DESKTOP-MOVS8IT\SQLEXPRESS; database=StudentDB; Trusted_Connection=True"; using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                //selecting stored procedure from database
                SqlCommand cmd = new SqlCommand("SelectAllBooksData", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                //reading data fromdatabase and storing into a list
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Books tempBooks = new Books();
                    tempBooks.bookID = Convert.ToInt32(sdr["bookID"]);
                    tempBooks.price = Convert.ToInt32(sdr["price"]);
                    tempBooks.bookName = sdr["bookName"].ToString();
                    tempBooks.category = sdr["category"].ToString();
                    tempBooks.shelfNumber = sdr["shelfNumber"].ToString();
                    tempBooks.bookIssuedStatus = sdr["bookIssuedStatus"].ToString();

                    booksList.Add(tempBooks);

                }
            }

            return booksList;
        }

        public List<User> GetUserData()
        {


            List<User> usersList = new List<User>();
            //establishing connection with database
            string ConnectionString = @"Data Source=DESKTOP-MOVS8IT\SQLEXPRESS; database=StudentDB; Trusted_Connection=True"; using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                //selecting stored procedure
                SqlCommand cmd = new SqlCommand("SelectAllUsersData", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                //reading data
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {                  
                    User tempUser = new User();
                    tempUser.userID= Convert.ToInt32(sdr["userID"]);
                    tempUser.userName = (sdr["userName"]).ToString();                   
                    tempUser.bookIssuedList = (sdr["bookIssuedList"]).ToString();
                    usersList.Add(tempUser);

                }
            }

            return usersList;
        }

        public List<string> GetNamesofBook()
        {
            booklist = GetData();//getting books data initialized in LibraryData.cs


            List<string> namesList = new List<string>();

            for (int i = 0; i < booklist.Count; i++)
            {
                namesList.Add(booklist[i].bookName);//addding thr names in a new list to returrn all the names


            }
            return namesList;


        }

        public Books findBookByName(string bookName)
        {
            booklist = GetData();
            usersList = GetUserData();

            foreach (var i in booklist)//iterating each book till we find the searched book
            {
                if (i.bookName == bookName)//when book name matched with the user inut
                {
                    return i;  //return the searched object
                }
            }
            return null;

        }


        public User finduserByName(string userName)
        {
            booklist = GetData();
            usersList = GetUserData();



            foreach (var z in usersList)
            {
                if (z.userName == userName)//comparing the username in the user list with the input
                {
                    return z;//return the books issued by that person
                }

            }
            return null;//return null if nothing found

        }


        //adding book
        public List<Books> AddBook(Books bookInput)
        {
            //getting data and estabilishing database connectio
            booklist = GetData();
            string ConnectionString = @"Data Source=DESKTOP-MOVS8IT\SQLEXPRESS; database=StudentDB; Trusted_Connection=True"; using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                //executing stored procedure
                using (SqlCommand cmd = new SqlCommand("AddBook", connection)
                { CommandType = CommandType.StoredProcedure }

                    )

                {
                    //adding items from json body to the database
                    cmd.Parameters.AddWithValue("@bookID", bookInput.bookID);
                    cmd.Parameters.AddWithValue("@bookName", bookInput.bookName);
                    cmd.Parameters.AddWithValue("@shelfNumber", bookInput.shelfNumber);
                    cmd.Parameters.AddWithValue("@category", bookInput.category);
                    cmd.Parameters.AddWithValue("@price", bookInput.price);
                    cmd.Parameters.AddWithValue("@bookIssuedStatus", bookInput.bookIssuedStatus);

                    cmd.Connection = connection;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

            }
            
            booklist.Add(bookInput);//adding the object to a list

            return booklist;
        }


        //adding user
        public List<User> AddUser(User userInput)
        {
            usersList = GetUserData();
            string ConnectionString = @"Data Source=DESKTOP-MOVS8IT\SQLEXPRESS; database=StudentDB; Trusted_Connection=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AddUser", connection)
                { CommandType = CommandType.StoredProcedure }
                    )
                {
                    cmd.Parameters.AddWithValue("@userID", userInput.userID);
                    cmd.Parameters.AddWithValue("@userName", userInput.userName);
                    cmd.Parameters.AddWithValue("@bookIssuedList", userInput.bookIssuedList);

                    cmd.Connection = connection;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

            }


           
            usersList.Add(userInput);//adding the object passed through JSON

            return usersList;
        }

        //updating user

        public List<User> UpdateUser(int userUpdatebyID, User userUpdateInput)
        {
            //getting data
            booklist = GetData();
            usersList = GetUserData();


            //searching for user to update it
            foreach (var i in usersList)
            {
                if (i.userID == userUpdatebyID)//searching the user
                {

                    string ConnectionString = @"Data Source=DESKTOP-MOVS8IT\SQLEXPRESS; database=StudentDB; Trusted_Connection=True";
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        //exectuing stored procedure
                        using (SqlCommand cmd = new SqlCommand("UpdateUser", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        }
                            )
                        {
                            cmd.Parameters.AddWithValue("@userID", userUpdateInput.userID);
                            cmd.Parameters.AddWithValue("@userName", userUpdateInput.userName);
                            cmd.Parameters.AddWithValue("@bookIssuedList", userUpdateInput.bookIssuedList);

                            cmd.Connection = connection;
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }

                    }
                    //updating user
                    i.userName = userUpdateInput.userName;
                    i.userID = userUpdateInput.userID;
                    i.bookIssuedList = userUpdateInput.bookIssuedList;
                    break;

                }

            }

            return usersList;
        }

        //updating book
        public List<Books> UpdateBook(int bookUpdatebyID, Books bookUpdateInput)
        {
            booklist = GetData();
            usersList = GetUserData();

            //search for the book
            foreach (var i in booklist)
            {
                if (i.bookID == bookUpdatebyID)//searching the user
                {
                    //establishing connection
                    string ConnectionString = @"Data Source=DESKTOP-MOVS8IT\SQLEXPRESS; database=StudentDB; Trusted_Connection=True";
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        //executing procedure
                        using (SqlCommand cmd = new SqlCommand("UpdateBook", connection)
                        {
                            CommandType = CommandType.StoredProcedure
                        }
                            )
                        {
                            //updating objects
                            cmd.Parameters.AddWithValue("@bookID", bookUpdateInput.bookID);
                            cmd.Parameters.AddWithValue("@bookName", bookUpdateInput.bookName);
                            cmd.Parameters.AddWithValue("@price", bookUpdateInput.price);
                            cmd.Parameters.AddWithValue("@shelfNumber", bookUpdateInput.shelfNumber);
                            cmd.Parameters.AddWithValue("@category", bookUpdateInput.category);
                            cmd.Parameters.AddWithValue("@bookIssuedStatus", bookUpdateInput.bookIssuedStatus);

                            cmd.Connection = connection;
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }

                    }
                    //updating user
                    i.bookName = bookUpdateInput.bookName;
                    i.price=bookUpdateInput.price;
                    i.shelfNumber=bookUpdateInput.shelfNumber;
                    i.category=bookUpdateInput.category;
                    i.bookIssuedStatus=bookUpdateInput.bookIssuedStatus;
                  
                    break;

                }

            }

            return booklist;



        }

        //removing book
        public List<Books> DeleteBook(string bookDeleteInput)
        {
            booklist = GetData();
            usersList = GetUserData();

            //removing book from booklist
            foreach (var j in booklist)
            {
                if (j.bookName == bookDeleteInput)//searching book
                {
                    //establishing database connection
                    string ConnectionString = @"Data Source=DESKTOP-MOVS8IT\SQLEXPRESS; database=StudentDB; Trusted_Connection=True";
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        //executing stored procedure fom database
                        using (SqlCommand cmd = new SqlCommand("DeleteBook", connection)

                        {
                            CommandType = CommandType.StoredProcedure
                        }

                            )
                        {

                            cmd.Connection = connection;
                            connection.Open();
                            cmd.Parameters.AddWithValue("@bookID", j.bookID);
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }

                    }

                    booklist.Remove(j);
                    break;



                }

            }

            //removing book from userIssued list
            foreach (var i in usersList)//searching for the user
            {

 
                string[] books = i.bookIssuedList.Split(',');

                for (int j = 0; j < books.Length; j++)
                {
                    if (books[j] == bookDeleteInput)
                    {

                        foreach (var z in booklist)
                        {
                            if (z.bookName == bookDeleteInput)
                            {
                                string newString = i.bookIssuedList.Replace(bookDeleteInput, "");
                                i.bookIssuedList = newString;

                                string ConnectionString = @"Data Source=DESKTOP-MOVS8IT\SQLEXPRESS; database=StudentDB; Trusted_Connection=True";
                                using (SqlConnection connection = new SqlConnection(ConnectionString))
                                {
                                    using (SqlCommand cmd = new SqlCommand("ReturnBook", connection)

                                    {
                                        CommandType = CommandType.StoredProcedure
                                    }

                                        )
                                    {

                                        cmd.Connection = connection;
                                        connection.Open();
                                        cmd.Parameters.AddWithValue("@userID", i.userID);
                                        cmd.Parameters.AddWithValue("@bookIssuedList", i.bookIssuedList);
                                        cmd.Parameters.AddWithValue("@bookIssuedStatus", z.bookIssuedStatus);
                                        cmd.Parameters.AddWithValue("@bookID", z.bookID);

                                        cmd.ExecuteNonQuery();
                                        connection.Close();
                                        break;
                                    }

                                }

                            }
                        }
                        


                    }
                }




            }
            return booklist;
        }

        //removing user
        public List<User> DeleteUser(int userDeleteID)
        {
            booklist = GetData();
            usersList = GetUserData();


            foreach (var i in usersList)//searching for the user to delete it
            {
                if (i.userID == userDeleteID)
                {
                    if (i.bookIssuedList == "")//if he have returned all books
                    {

                        //removing from database
                        string ConnectionString = @"Data Source=DESKTOP-MOVS8IT\SQLEXPRESS; database=StudentDB; Trusted_Connection=True";
                        using (SqlConnection connection = new SqlConnection(ConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("DeleteUser", connection)

                            {
                                CommandType = CommandType.StoredProcedure
                            }

                                )
                            {

                                cmd.Connection = connection;
                                connection.Open();
                                cmd.Parameters.AddWithValue("@userID", i.userID);
                                cmd.ExecuteNonQuery();
                                connection.Close();
                            }

                        }

                        usersList.Remove(i);//delete
                        return usersList;

                    }

                }

            }

            return usersList;

        }


        public List<User> IssueBook(string bookName, string userNameInput)
        {

            booklist = GetData();
            usersList =GetUserData();
            foreach (var i in usersList)//searching for the book in 
            {
                if (userNameInput == i.userName)//searchinf for the user in the list to issue book
                {
                    foreach (var j in booklist)
                    {
                        if (j.bookName == bookName)
                        {
                            if (j.bookIssuedStatus == "no")//if book is not issued before 
                            {
                                //adding book to the list
                                if(i.bookIssuedList=="")
                                {
                                    i.bookIssuedList = i.bookIssuedList + bookName;

                                }
                                else
                                {
                                    i.bookIssuedList = i.bookIssuedList + "," + bookName;
                                }
                                

                                j.bookIssuedStatus = "yes";//setting the book status to issued

                                //issuing in  database

                                string ConnectionString = @"Data Source=DESKTOP-MOVS8IT\SQLEXPRESS; database=StudentDB; Trusted_Connection=True";
                                using (SqlConnection connection = new SqlConnection(ConnectionString))
                                {
                                    using (SqlCommand cmd = new SqlCommand("IssueBook", connection)

                                    {
                                        CommandType = CommandType.StoredProcedure
                                    }

                                        )
                                    {

                                        cmd.Connection = connection;
                                        connection.Open();
                                        cmd.Parameters.AddWithValue("@userID", i.userID);
                                        cmd.Parameters.AddWithValue("@bookIssuedList", i.bookIssuedList);
                                        cmd.Parameters.AddWithValue("@bookIssuedStatus", j.bookIssuedStatus);
                                        cmd.Parameters.AddWithValue("@bookID", j.bookID);

                                        cmd.ExecuteNonQuery();
                                        connection.Close();
                                    }

                                }
                                 return usersList;

                            }

                        }
                    }
                }
            }
            return usersList;

        }


        //returning book

        public List<User> returnBook(string bookNameInput)
        {
            booklist = GetData();
            usersList = GetUserData();


            foreach (var i in usersList)//searching for the user
            {
                string[] books = i.bookIssuedList.Split(','); //breaking the string into array of books

                for (int j = 0; j < books.Length; j++)
                {
                    if (books[j] == bookNameInput)//searching for book in ser bookisssued list
                    {
                        string newString = i.bookIssuedList.Replace(bookNameInput, "");//removing the book
                        i.bookIssuedList = newString;

                        foreach (var k in booklist)//seting the book status to not issued
                        {
                            if (k.bookName == bookNameInput)
                            {
                                k.bookIssuedStatus = "no";
                            }



                            //retrning book in database                                                      
                            string ConnectionString = @"Data Source=DESKTOP-MOVS8IT\SQLEXPRESS; database=StudentDB; Trusted_Connection=True";
                            using (SqlConnection connection = new SqlConnection(ConnectionString))
                            {
                                using (SqlCommand cmd = new SqlCommand("ReturnBook", connection)

                                {
                                    CommandType = CommandType.StoredProcedure
                                }

                                    )
                                {

                                    cmd.Connection = connection;
                                    connection.Open();
                                    cmd.Parameters.AddWithValue("@userID", i.userID);
                                    cmd.Parameters.AddWithValue("@bookIssuedList", i.bookIssuedList);
                                    cmd.Parameters.AddWithValue("@bookIssuedStatus", k.bookIssuedStatus);
                                    cmd.Parameters.AddWithValue("@bookID", k.bookID);


                                    cmd.ExecuteNonQuery();
                                    connection.Close();
                                }

                            }

                        }


                    }
                }

            }
            return usersList;
        }


        //calculating fine task 11
        public List<LibrarayManagement> GetPenaltyData()
        {

            //grtting the book issue and return records from database
            List<LibrarayManagement> fineList = new List<LibrarayManagement>();
            string ConnectionString = @"Data Source=DESKTOP-MOVS8IT\SQLEXPRESS; database=StudentDB; Trusted_Connection=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {


                SqlCommand cmd = new SqlCommand("PenaltyData", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    LibrarayManagement tempRecord = new LibrarayManagement();

                    tempRecord.userId= Convert.ToInt32(sdr["userID"]);
                    tempRecord.BookCheckIn = Convert.ToDateTime(sdr["bookcCheckIn"]);
                    tempRecord.BookReceived = Convert.ToDateTime(sdr["BookReceived"]);


                    fineList.Add(tempRecord);

                }
            }

            return fineList;
        }



    }
}
