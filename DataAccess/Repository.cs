using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Models.Models;

namespace DataAccess
{
    public class Repository
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "7mLhRqbtl68Hu6bTgV7bVnTtVilVD9QnL9GFadPI",
            BasePath = "https://moviestore-2dc10.firebaseio.com/"
        };
        public void Client()
        {
            client = new FireSharp.FirebaseClient(config);
        }
        IFirebaseClient client;
        public async void InsertMovie(Movie movie)
        {
            Client();
            Movie newMovie = movie;
            SetResponse response = await client.SetTaskAsync("movies/" + newMovie.Id, newMovie);
        }
        public async void UpdateMovie(Movie movie)
        {
            Client();
            FirebaseResponse response = await client.UpdateTaskAsync("movies/" + movie.Id, movie);
        }
        public async Task<List<Movie>> GetMovies()
        {
            Client();
            FirebaseResponse response = await client.GetTaskAsync("movies/");
            List<Movie> returnedList = response.ResultAs<List<Movie>>();
            return returnedList;
        }
        public async Task<Movie> GetMovie(string title)
        {
            Client();
            FirebaseResponse response = await client.GetTaskAsync("movies/");
            List<Movie> returnedList = response.ResultAs<List<Movie>>();
            Movie matchedMovie = returnedList.Where(e => e.Title == title).FirstOrDefault();
            return matchedMovie;
        }
        public async Task<Movie> GetMovie(int id)
        {
            Client();
            FirebaseResponse response = await client.GetTaskAsync("movies/");
            List<Movie> returnedList = response.ResultAs<List<Movie>>();
            Movie matchedMovie = returnedList.Where(e => e.Id == id).FirstOrDefault();
            return matchedMovie;
        }
        public async void RemoveMovie(Movie movie)
        {
            Client();
            FirebaseResponse response = await client.DeleteTaskAsync("movies/" + movie.Id);
        }
        public async void InsertUser(User user)
        {
            Client();
            User newUser = user;
            SetResponse response = await client.SetTaskAsync("users/" + newUser.Id, newUser);
        }
        public async void UpdateUser(User user)
        {
            Client();
            FirebaseResponse response = await client.UpdateTaskAsync("users/" + user.Id, user);
        }
        public async void RemoveUser(User user)
        {
            Client();
            FirebaseResponse response = await client.DeleteTaskAsync("users/" + user.Id);
        }
        public async Task<List<User>> GetUsers()
        {
            Client();
            FirebaseResponse response = await client.GetTaskAsync("users/");
            List<User> returnedList = response.ResultAs<List<User>>();
            return returnedList;
        }
        public async Task<User> GetUser(string username, string password)
        {
            Client();
            FirebaseResponse response = await client.GetTaskAsync("users/");
            List<User> returnedObj = response.ResultAs<List<User>>();
            User returnedUser = returnedObj.Where(e => e.UserName == username && e.Password == password).FirstOrDefault();
            return returnedUser;
        }
        public async Task<List<Employee>> GetEmployees()
        {
            Client();
            FirebaseResponse response = await client.GetTaskAsync("employees/");
            List<Employee> returnedList = response.ResultAs<List<Employee>>();
            return returnedList;
        }
        public async Task<Employee> GetEmployee(string username, string password)
        {
            Client();
            FirebaseResponse response = await client.GetTaskAsync("employees/");
            List<Employee> returnedObj = response.ResultAs<List<Employee>>();
            Employee returnedEmployee = returnedObj.Where(e => e.UserName == username && e.Password == password).FirstOrDefault();
            return returnedEmployee;
        }
    }
}
