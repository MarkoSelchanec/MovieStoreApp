using DataAccess;
using Models.Enums;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services2
{
    public static class UserService
    {
        private static Repository _repository = new Repository();
        public static void CheckSubscription(this User _loggedUser)
        {
            //Done
            Console.WriteLine($"Your subscription type is: {_loggedUser.SubscriptionType}");
            if (_loggedUser.SubscriptionType == SubscriptionType.Annually)
            {
                Console.WriteLine($"Your subscription ends on: {_loggedUser.DateOfRegistration.AddYears(1).ToShortDateString()}");
            }
            if (_loggedUser.SubscriptionType == SubscriptionType.Monthly)
            {
                Console.WriteLine($"Your subscription ends on: {_loggedUser.DateOfRegistration.AddMonths(1).ToShortDateString()}");
            }
            Service.ClearConsole();
        }
        public static void CheckRentedMovies(this User _loggedUser)
        {
            //Done
            if (_loggedUser.Movies.Count == 0)
            {
                Console.WriteLine("You do not have any rented movies.");
            }
            else
            {
                Console.WriteLine("Your rented movies:");
                _loggedUser.Movies.ForEach(m => Console.WriteLine(m));
            }
            Service.ClearConsole();
        }
        public static void RentMovie(this User _loggedUser)
        {
            //Done
            var movies = Service.CheckAvailableMovies();
            Console.WriteLine("Press X to go back.");
            var movieChoice = Console.ReadLine();
            Movie rentedMovie = null;
            if (movieChoice.ToUpper() != "X")
            {
                int movieChoiceInt = int.Parse(movieChoice);
                rentedMovie = movies[movieChoiceInt - 1];
                rentedMovie.IsRented = true;
                _repository.UpdateMovie(rentedMovie);
                _loggedUser.Movies.Add(rentedMovie.Title);
                _repository.UpdateUser(_loggedUser);
                Service.ClearConsole();
            }
            if (movieChoice.ToUpper() == "X")
            {
                Console.Clear();
            }
            if (rentedMovie != null)
            {
                Console.WriteLine($"You rented {rentedMovie.Title}");
            }
        }
        public static void ReturnMovie(this User _loggedUser)
        {
            //Done
            if (_loggedUser.Movies.Count == 0)
            {
                Console.WriteLine("You do not have any rented movies.");
            }
            else
            {
                int counter = 1;
                _loggedUser.Movies.ForEach(m =>
                {
                    Console.WriteLine($"{counter}) {m}");
                    counter++;
                });
                Console.WriteLine("Press X to go back.");
                var movieChoice = Console.ReadLine();
                if (movieChoice.ToUpper() != "X")
                {
                    int movieChoiceInt = int.Parse(movieChoice);
                    var movie = _repository.GetMovie(movieChoiceInt - 1).Result;
                    movie.IsRented = false;
                    _repository.UpdateMovie(movie);
                    _loggedUser.Movies.Remove(_loggedUser.Movies[movieChoiceInt - 1]);
                    _repository.UpdateUser(_loggedUser);
                }
            }
            Service.ClearConsole();
        }
    }
}
