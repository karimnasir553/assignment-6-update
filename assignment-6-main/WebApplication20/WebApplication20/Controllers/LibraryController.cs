using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using WebApplication3.DataLayer;
using WebApplication3.Models;
using WebApplication3.BusinessLayer;

using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      private  LibraryData _libraryData = new LibraryData();
        


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }



        [HttpGet("getBooks")]

        public List<Books> Get()
        {
            return _libraryData.GetData();

        }

        [Route("getUsers")]
        [HttpGet]

        public List<User> GetUsers()
        {
            return _libraryData.GetUserData();

        }

        [Route("booknames")]
        [HttpGet]
        public List<string> GetNamesofBook()
        {
            
            return _libraryData.GetNamesofBook();


        }

        ////adding book (task 1)
        [Route("addBooks")]
        [HttpPost]
        public List<Books> AddBook([FromBody] Books bookInput)
        {

            return _libraryData.AddBook(bookInput);

        }

        ////adding user(task 2)
        [Route("addUsers")]
        [HttpPost]

        public List<User> AddUser([FromBody] User userInput)
        {
            return _libraryData.AddUser(userInput);
        }

        ////find book by their name (task 3)
        [Route("bookNames/{bookName}")]
        [HttpGet]
        public Books findByNames(string bookName)
        {

            return _libraryData.findBookByName(bookName);

        }

        //find user by user name to see details (task 4)
        [Route("finduser/{userName}")]
        [HttpGet]
        public User finduserByName(string userName)
        {
            return _libraryData.finduserByName(userName);


        }

        //updating book (task 5)
        [Route("bookupdate/{bookUpdatebyID}")]
        [HttpPut]

        public List<Books> UpdateBook(int bookUpdatebyID, [FromBody] Books bookUpdateInput)
        {
            return _libraryData.UpdateBook(bookUpdatebyID, bookUpdateInput);

        }

        ////updating user (task 6)
        [Route("updateUser/{userUpdatebyID}")]
        [HttpPut]
        public List<User> UpdateUser(int userUpdatebyID, [FromBody] User userUpdateInput)
        {
            return _libraryData.UpdateUser(userUpdatebyID, userUpdateInput);

        }

        //issue books to user (task 7)
        [Route("issueBook/{userNameInput}/{bookName}")]
        [HttpPost]
        public List<User> IssueBook(string bookName, string userNameInput)
        {
            return _libraryData.IssueBook(bookName, userNameInput);

        }


        //removing book (task 8)
        [Route("bookDelete/{bookDeleteInput}")]
        [HttpDelete]
        public List<Books> DeleteBook(string bookDeleteInput)
        {
            
            return _libraryData.DeleteBook(bookDeleteInput);
        }


   


        //removing user (task 9)
        [Route("userDelete/{userDeleteID}")]
        [HttpDelete]
        public List<User> DeleteUser(int userDeleteID)
        {
           return _libraryData.DeleteUser(userDeleteID);

        }





        //task 10 (returning book)
        [Route("returnBook/{bookNameInput}")]
        [HttpDelete]

        public List<User> returnBook(string bookNameInput)
        {
            return _libraryData.returnBook(bookNameInput);


        }



        //task 11(imposing fine for late returning
        [HttpGet("getFine/{id}")]

        public int GetFine(int id)
        {
            penCalculatorcs p = new penCalculatorcs();
            List<LibrarayManagement> tempList = new List<LibrarayManagement>();//making a list to store records of book issuing and returning
            int fine = 0;
            

            tempList = _libraryData.GetPenaltyData();
            foreach (var i in tempList)//searching for the user
            {
                if (i.userId == id)
                {
                    fine = p.CalculateAmount(i.BookCheckIn, i.BookReceived);//calculating fine see Model Class for explaination
                }
            }
            return fine;






        }



    }
}