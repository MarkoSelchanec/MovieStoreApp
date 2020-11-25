using DataAccess;
using MovieStore.Models;
using MovieStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Services
{
    public static class EmployeeService
    {
        public static void CheckMembers(this Employee employee)
        {
            Console.WriteLine("Users: ");
            StaticDb.users.ForEach(x => x.DisplayInfo());
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Employees: ");
            StaticDb.employees.ForEach(x => x.DisplayInfo());
            Console.WriteLine("--------------------------------------------------");
        }

        public static void AddUser(User user)
        {
            StaticDb.users.Add(user);
            Console.WriteLine($"User {user.FirstName} added");
        }
        public static void RemoveUser(User user)
        {
            StaticDb.users.Remove(user);
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
            while(releaseMonth < 0 && releaseMonth > 12)
            {
                Console.WriteLine("There are 12 months )");
                releaseMonth = int.Parse(Console.ReadLine());
            }
            Console.Write("Day:");
            int releaseDay = int.Parse(Console.ReadLine());
            while(releaseDay < 1 && releaseDay > 31)
            {
                Console.WriteLine("There are up to 31 days in a month )");
                releaseDay = int.Parse(Console.ReadLine());
            }
            newMovie.ReleaseDate = new DateTime(releaseYear, releaseMonth, releaseDay);

            StaticDb.Movies.Add(newMovie);
        }

    }
}
