using DataAccess;
using MovieStore.Models;
using MovieStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Services
{
    public static class UserService
    {
        public static void CheckAvailableMovies()
        {
            Console.WriteLine("Movies in stock: ");
            int i = 1;
            foreach (Movie movie in StaticDb.Movies)
            {
                Console.WriteLine($"{i}) {movie.Title}");
                i++;
            }
        }
        public static void CheckSubscription(this User _loggedUser)
        {
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
            if (_loggedUser.Movies.Count == 0)
            {
                Console.WriteLine("You do not have any rented movies.");
            }
            else
            {
                Console.WriteLine("Your rented movies:");
                _loggedUser.Movies.ForEach(m => Console.WriteLine(m.Title));
            }
            Service.ClearConsole();
        }
        public static void RentMovie(this User _loggedUser)
        {
            Console.WriteLine("Movies on offer: ");
            int counter = 1;
            StaticDb.Movies.ForEach(m =>
            {
                Console.WriteLine($"{counter}) {m.Title}");
                counter++;
            });
            Console.WriteLine("Press X to go back.");
            var movieChoice = Console.ReadLine();
            Movie rentedMovie = null;
            if (movieChoice.ToUpper() != "X")
            {
                int movieChoiceInt = int.Parse(movieChoice);
                _loggedUser.Movies.Add(StaticDb.Movies[movieChoiceInt - 1]);
                rentedMovie = StaticDb.Movies[movieChoiceInt - 1];
                StaticDb.Movies.Remove(StaticDb.Movies[movieChoiceInt - 1]);
                Service.ClearConsole();
            }
            if(movieChoice.ToUpper() == "X")
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
            if (_loggedUser.Movies.Count == 0)
            {
                Console.WriteLine("You do not have any rented movies.");
            }
            else
            {
                int counter = 1;
                _loggedUser.Movies.ForEach(m =>
                {
                    Console.WriteLine($"{counter}) {m.Title}");
                    counter++;
                });
                Console.WriteLine("Press X to go back.");
                var movieChoice = Console.ReadLine();
                if (movieChoice.ToUpper() != "X")
                {
                    int movieChoiceInt = int.Parse(movieChoice);
                    StaticDb.Movies.Add(_loggedUser.Movies[movieChoiceInt - 1]);
                    _loggedUser.Movies.Remove(_loggedUser.Movies[movieChoiceInt - 1]);
                }
            }
            Service.ClearConsole();
        }
    }
}
