using DataAccess;
using Models.Enums;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services2
{
    public static class EmployeeService
    {
        private static Repository _repository = new Repository();
        public static void CheckMembers(this Employee employee)
        {
            //DONE
            List<User> users = _repository.GetUsers().Result;
            Console.WriteLine("Users: ");
            users.ForEach(x => x.DisplayInfo());
            Console.WriteLine("--------------------------------------------------");
            List<Employee> employees = _repository.GetEmployees().Result;
            Console.WriteLine("Employees: ");
            employees.ForEach(x => x.DisplayInfo());
            Console.WriteLine("--------------------------------------------------");
        }

        public static void AddUser(User user)
        {
            //DONE
            _repository.InsertUser(user);
            Console.WriteLine($"User {user.FirstName} added");
        }
        public static void RemoveUser(User user)
        {
            //Done
            _repository.RemoveUser(user);
            Console.WriteLine($"User {user.FirstName} removed");
        }
        public static void AddEmployee(Employee employee)
        {
            StaticDb.employees.Add(employee);
            Console.WriteLine($"Employee {employee.FirstName} added");
        }
        public static void RemoveEmployee(Employee employee)
        {
            StaticDb.employees.Remove(employee);
            Console.WriteLine($"Employee {employee.FirstName} removed");
        }
        public static void AddMovie()
        {
            Movie newMovie = new Movie();
            Console.Clear();
            Console.WriteLine("Adding movie");
            Console.Write("Title:");
            string movieTitle = Console.ReadLine();
            while (movieTitle.Length < 3 || movieTitle.Length > 50)
            {
                Console.WriteLine("Invalid movie title length, please try again.");
                movieTitle = Console.ReadLine();
            }
            newMovie.Title = movieTitle;

            Console.Write("Description:");
            string movieDescription = Console.ReadLine();
            while (movieDescription.Length < 20 || movieDescription.Length > 255)
            {
                Console.WriteLine("Invalid movie description length, description needs to be between 20 and 255 characters, please try again");
                movieDescription = Console.ReadLine();
            }
            newMovie.Description = movieDescription;

            Console.WriteLine("Genre:");
            var enums = Enum.GetValues(typeof(Models.Enums.Genre));
            int counter = 1;
            foreach (var item in enums)
            {
                Console.WriteLine($"{counter}) {item}");
                counter++;
            }
            int movieGenreChoice = int.Parse(Console.ReadLine());

            switch (movieGenreChoice)
            {
                case 1:
                    newMovie.Genre = Genre.Comedy;
                    break;
                case 2:
                    newMovie.Genre = Genre.Horror;
                    break;
                case 3:
                    newMovie.Genre = Genre.Drama;
                    break;
                case 4:
                    newMovie.Genre = Genre.Fiction;
                    break;
                case 5:
                    newMovie.Genre = Genre.Documentary;
                    break;

                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }


            Console.Write("Release Year:");
            int releaseYear = int.Parse(Console.ReadLine());
            while (releaseYear < 1900 && releaseYear > DateTime.Now.Year)
            {
                Console.WriteLine("Just input a correct year please...");
                releaseYear = int.Parse(Console.ReadLine());
            }
            Console.Write("Month:");
            int releaseMonth = int.Parse(Console.ReadLine());
            while (releaseMonth < 0 && releaseMonth > 12)
            {
                Console.WriteLine("There are 12 months )");
                releaseMonth = int.Parse(Console.ReadLine());
            }
            Console.Write("Day:");
            int releaseDay = int.Parse(Console.ReadLine());
            while (releaseDay < 1 && releaseDay > 31)
            {
                Console.WriteLine("There are up to 31 days in a month )");
                releaseDay = int.Parse(Console.ReadLine());
            }
            newMovie.ReleaseDate = new DateTime(releaseYear, releaseMonth, releaseDay);

            StaticDb.Movies.Add(newMovie);
        }
        public static void RemoveMovie()
        {
            Console.Clear();
            var movies = Service.CheckAvailableMovies();
            Console.WriteLine("Press X to go back.");
            var movieChoice = Console.ReadLine();
            Movie rentedMovie = null;
            if (movieChoice.ToUpper() != "X")
            {
                int movieChoiceInt = int.Parse(movieChoice);
                var movieToRemove = movies[movieChoiceInt - 1];
                if(!movieToRemove.IsRented)
                {
                _repository.RemoveMovie(movieToRemove);
                } 
                else
                {
                    Console.WriteLine($"{movieToRemove.Title} is rented and can't be removed until it is returned.");
                }
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
    }
}
