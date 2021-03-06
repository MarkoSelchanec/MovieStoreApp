﻿using DataAccess;
using Models.Enums;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services2
{
    public static class Service
    {
        private static Task<User> _loggedUser = null;
        private static Task<Employee> _loggedEmployee = null;
        private static Repository _repository = new Repository();
        public static List<Movie> CheckAvailableMovies()
        {
            var movies = _repository.GetMovies();
            Console.WriteLine("Movies in stock: ");
            foreach (Movie movie in movies.Result)
            {
                if(movie.IsRented)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{movie.Id + 1}) {movie.Title} | Rented");
                    Console.ForegroundColor = ConsoleColor.White;
                } else
                {
                Console.WriteLine($"{movie.Id + 1}) {movie.Title}");
                }
            }
            return movies.Result;
        }
        public static User RegisterUser()
        {
            Console.Clear();
            Console.WriteLine("User registration");
            User user = new User();

            Console.Write("First name:");
            string fName = Console.ReadLine();
            while (fName.Length <= 3 || fName.Length > 30 || fName.Any(char.IsDigit))
            {
                Console.WriteLine("Invalid name, please try again:");
                fName = Console.ReadLine();
            }

            Console.Write("Last name:");
            string lName = Console.ReadLine();
            while (lName.Length <= 3 || lName.Length > 30 || lName.Any(char.IsDigit))
            {
                Console.WriteLine("Invalid last name, please try again:");
                lName = Console.ReadLine();
            }

            Console.Write("Age:");
            int age = int.Parse(Console.ReadLine());
            try
            {
                while (age == 0 || age < 12 || age > 100)
                {

                    Console.WriteLine("Invalid age, please try again:");
                    age = int.Parse(Console.ReadLine());
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input, please try again: ");
            }


            Console.Write("UserName:");
            string userName = Console.ReadLine();
            while (userName.Length < 3 || userName.Length > 30)
            {
                Console.WriteLine("Invalid username, please try again:");
                userName = Console.ReadLine();
            }
            if (StaticDb.users.Any(x => x.UserName == userName))
            {
                Console.WriteLine("That UserName is taken, please try again:");
                userName = Console.ReadLine();
            }

            Console.Write("Password:");
            string password = Console.ReadLine();
            while (password.Length < 3 || password.Length > 30)
            {
                Console.WriteLine("Invalid password, please try again:");
                password = Console.ReadLine();
            }

            Console.WriteLine("Subscription type:");
            Console.WriteLine($"1) {nameof(SubscriptionType.Monthly)}");
            Console.WriteLine($"2) {nameof(SubscriptionType.Annually)}");
            int subscriptionType = int.Parse(Console.ReadLine());
            if (subscriptionType == 1)
            {
                user.SubscriptionType = SubscriptionType.Monthly;
            }
            if (subscriptionType == 2)
            {
                user.SubscriptionType = SubscriptionType.Annually;
            }

            user.FirstName = fName;
            user.LastName = lName;
            user.Age = age;
            user.UserName = userName;
            user.Password = password;
            user.Id = _repository.GetUsers().Result.Count;
            Console.WriteLine("Registration Successful!");
            Console.WriteLine($"Welcome {user.FirstName}");
            ClearConsole();
            return user;
        }

        public static Employee RegisterEmployee()
        {
            Console.Clear();
            Console.WriteLine("Employee Registration");
            Console.Write("First name:");
            string fName = Console.ReadLine();
            while (fName.Length <= 3 || fName.Length > 30 || fName.Any(char.IsDigit))
            {
                Console.WriteLine("Invalid name, please try again:");
                fName = Console.ReadLine();
            }

            Console.Write("Last name:");
            string lName = Console.ReadLine();
            while (lName.Length <= 3 || lName.Length > 30 || lName.Any(char.IsDigit))
            {
                Console.WriteLine("Invalid last name, please try again:");
                lName = Console.ReadLine();
            }

            Console.Write("Age:");
            int age = int.Parse(Console.ReadLine());
            try
            {
                while (age == 0 || age < 12 || age > 100)
                {

                    Console.WriteLine("Invalid age, please try again:");
                    age = int.Parse(Console.ReadLine());
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input, please try again: ");
            }

            Console.Write("UserName:");
            string userName = Console.ReadLine();
            while (userName.Length < 3 || userName.Length > 30)
            {
                Console.WriteLine("Invalid username, please try again:");
                userName = Console.ReadLine();
            }
            if (StaticDb.employees.Any(x => x.UserName == userName))
            {
                Console.WriteLine("That UserName is taken, please try again:");
                userName = Console.ReadLine();
            }

            Console.Write("Password:");
            string password = Console.ReadLine();
            while (password.Length < 3 || password.Length > 30)
            {
                Console.WriteLine("Invalid password, please try again:");
                password = Console.ReadLine();
            }

            Console.WriteLine("Hours per month:");
            int hours = int.Parse(Console.ReadLine());
            while (hours < 40 || hours > 200)
            {
                Console.WriteLine("Weekly hours can't be under 40 or over 200");
                hours = int.Parse(Console.ReadLine());
            }


            Employee employee = new Employee(fName, lName, age, userName, password, hours, 0);
            return employee;
        }

        public static Task<User> UserLogIn(string username, string password)
        {
            var loggedUser = _repository.GetUser(username, password);
            if (loggedUser == null)
            {
                Console.Clear();
                Console.WriteLine("That username and password combination do not match any registered user.");
                return null;
            }
            else
            {
                return loggedUser;
            }
        }
        public static Task<Employee> EmployeeLogIn(string username, string password)
        {
            var loggedEmployee = _repository.GetEmployee(username, password);
            if (loggedEmployee == null)
            {
                Console.Clear();
                Console.WriteLine("That username and password combination do not match any registered employee.");
                return null;
            }
            else
            {
                return loggedEmployee;
            }
        }
        public static void ClearConsole()
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
            Console.Clear();
        }
        public static bool UserConsole()
        {
            bool successfulLog = false;
            while (!successfulLog)
            {
                Console.Clear();
                Console.WriteLine("User login");
                Console.Write("Username:");
                string userName = Console.ReadLine();
                Console.Write("Password:");
                string password = Console.ReadLine();
                _loggedUser = UserLogIn(userName, password);
                if (_loggedUser.Result != null)
                {
                    bool userConsole = false;
                    while (!userConsole)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine($"{_loggedUser.Result.FirstName}, would you like to: ");
                            Console.WriteLine("1) Check your subscription status");
                            Console.WriteLine("2) Rent a movie");
                            Console.WriteLine("3) Check your rented movies");
                            Console.WriteLine("4) Return a movie");
                            Console.WriteLine("X) Back to start");
                            var userValidatedChoice = Console.ReadLine();
                            if (userValidatedChoice.ToUpper() != "X")
                            {
                                var userValidatedChoiceInt = int.Parse(userValidatedChoice);
                                if (userValidatedChoiceInt == 1)
                                {
                                    _loggedUser.Result.CheckSubscription();
                                }
                                if (userValidatedChoiceInt == 2)
                                {
                                    Console.Clear();
                                    _loggedUser.Result.RentMovie();
                                }
                                if (userValidatedChoiceInt == 3)
                                {
                                    _loggedUser.Result.CheckRentedMovies();
                                }
                                if (userValidatedChoiceInt == 4)
                                {
                                    _loggedUser.Result.ReturnMovie();
                                }
                            }
                            if (userValidatedChoice.ToUpper() == "X")
                            {
                                Console.Clear();
                                userConsole = true;
                                successfulLog = true;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Wrong input.");
                        }
                    }
                }
                if (_loggedUser.Result == null)
                {
                    Console.WriteLine("Would you like to try again? Y/N");
                    char retryLogin = char.Parse(Console.ReadLine().ToUpper());
                    if (retryLogin == 'N')
                    {
                        Console.Clear();
                        successfulLog = true;
                    }
                }
            }
            return successfulLog;
        }
        public static bool EmployeeConsole()
        {
            bool successfulLog = false;
            while (!successfulLog)
            {
                Console.Clear();
                Console.WriteLine("Employee login");
                Console.Write("Username:");
                string userName = Console.ReadLine();
                Console.Write("Password:");
                string password = Console.ReadLine();
                _loggedEmployee = EmployeeLogIn(userName, password);
                if (_loggedEmployee != null)
                {
                    Console.Clear();
                    bool employeeConsole = false;
                    while (!employeeConsole)
                    {
                        Console.WriteLine($"{_loggedEmployee.Result.FirstName}, would you like to: ");
                        Console.WriteLine("1) Check all registered members");
                        Console.WriteLine("2) Add a new Member");
                        Console.WriteLine("3) Remove a Member");
                        Console.WriteLine("4) Check all available movies");
                        Console.WriteLine("5) Add a new Movie");
                        Console.WriteLine("6) Remove Movie");
                        Console.WriteLine("X) Back to start");
                        var employeeValidatedChoice = Console.ReadLine();
                        if (employeeValidatedChoice.ToUpper() != "X")
                        {
                            var employeeValidatedChoiceInt = int.Parse(employeeValidatedChoice);
                            if (employeeValidatedChoiceInt == 1)
                            {
                                _loggedEmployee.Result.CheckMembers();
                                ClearConsole();
                            }
                            if (employeeValidatedChoiceInt == 2)
                            {
                                Console.Clear();
                                Console.WriteLine("Would you like to add a:");
                                Console.WriteLine("1) User");
                                Console.WriteLine("2) Employee");
                                Console.WriteLine("Press X to go back.");
                                var addMemberChoice = Console.ReadLine();
                                if (addMemberChoice.ToUpper() != "X")
                                {
                                    int addMemberChoiceInt = int.Parse(addMemberChoice);
                                    if (addMemberChoiceInt == 1)
                                    {
                                        EmployeeService.AddUser(RegisterUser());
                                    }
                                    if (addMemberChoiceInt == 2)
                                    {
                                        EmployeeService.AddEmployee(RegisterEmployee());
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                }
                            }
                            if (employeeValidatedChoiceInt == 3)
                            {
                                Console.Clear();
                                Console.WriteLine("Would you like to remove a:");
                                Console.WriteLine("1) User");
                                Console.WriteLine("2) Employee");
                                Console.WriteLine("Press X to go back.");
                                var removeMemberChoice = Console.ReadLine();
                                if (removeMemberChoice.ToUpper() != "X")
                                {
                                    int removeMemberChoiceInt = int.Parse(removeMemberChoice);
                                    int counter = 1;
                                    if (removeMemberChoiceInt == 1)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Users: ");
                                        StaticDb.users.ForEach(x => {
                                            Console.Write(counter + ") ");
                                            x.DisplayInfo();
                                            counter++;
                                        });
                                        Console.WriteLine("Press X to go back.");
                                        var userToRemove = Console.ReadLine();
                                        if (userToRemove.ToUpper() != "X")
                                        {
                                            Console.Clear();
                                            var userToRemoveInt = int.Parse(userToRemove);
                                            Console.WriteLine($"User {StaticDb.users[userToRemoveInt - 1].FirstName} was removed.");
                                            StaticDb.users.Remove(StaticDb.users[userToRemoveInt - 1]);
                                        }
                                    }
                                    if (removeMemberChoiceInt == 2)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Employees: ");
                                        StaticDb.employees.ForEach(x => {
                                            if (x != _loggedEmployee.Result)
                                            {
                                                Console.Write(counter + ") ");
                                                x.DisplayInfo();
                                                counter++;
                                            }
                                        });
                                        Console.WriteLine("Press X to go back.");
                                        var employeeToRemove = Console.ReadLine();
                                        if (employeeToRemove.ToUpper() != "X")
                                        {
                                            Console.Clear();
                                            var employeeToRemoveInt = int.Parse(employeeToRemove);
                                            Console.WriteLine($"Employee {StaticDb.employees[employeeToRemoveInt - 1].FirstName} was removed.");
                                            StaticDb.employees.Remove(StaticDb.employees[employeeToRemoveInt - 1]);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.Clear();
                                }
                            }
                            if (employeeValidatedChoiceInt == 4)
                            {
                                CheckAvailableMovies();
                                ClearConsole();
                            }
                            if (employeeValidatedChoiceInt == 5)
                            {
                                EmployeeService.AddMovie();
                            }                            
                            if (employeeValidatedChoiceInt == 6)
                            {
                                EmployeeService.RemoveMovie();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            employeeConsole = true;
                            successfulLog = true;
                        }
                    }
                }
                if (_loggedEmployee == null)
                {
                    Console.WriteLine("Would you like to try again? Y/N");
                    char retryLogin = char.Parse(Console.ReadLine().ToUpper());
                    if (retryLogin == 'N')
                    {
                        Console.Clear();
                        successfulLog = true;
                    }
                }
            }
            return successfulLog;
        }
        public static void App()
        {

            Console.WriteLine("Welcome to the movie store!");
            bool runningValidate = false;
            while (!runningValidate)
            {
                bool userValidate = false;
                while (!userValidate)
                {
                    Console.WriteLine("Select an action: ");
                    Console.WriteLine("1) Log in");
                    Console.WriteLine("2) Register");
                    Console.WriteLine("X) Exit");
                    var userInp = Console.ReadLine();
                    if (userInp.ToUpper() != "X")
                    {
                        try
                        {
                            Console.WriteLine(userInp);
                            var userInpInt = int.Parse(userInp);
                            if (userInpInt == 1)
                            {
                                bool loggedIn = false;
                                while (!loggedIn)
                                {
                                    Console.Clear();
                                    Console.WriteLine("1) User login");
                                    Console.WriteLine("2) Employee login");
                                    Console.WriteLine("X) Back to start");
                                    var memberValidate = Console.ReadLine();
                                    if (memberValidate.ToUpper() == "X")
                                    {
                                        Console.Clear();
                                        loggedIn = true;
                                    }
                                    if(int.Parse(memberValidate) == 1)
                                    {
                                        loggedIn = UserConsole();
                                    }
                                    if(int.Parse(memberValidate) == 2)
                                    {
                                        loggedIn = EmployeeConsole();
                                    }
                                }
                            }
                            else if (userInpInt == 2)
                            {
                                StaticDb.users.Add(RegisterUser());
                                userValidate = true;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Invalid input, please try again:");
                            }
                        }
                        catch
                        {
                            Console.Clear();
                        }
                    }
                    if (userInp.ToUpper() == "X")
                    {
                        userValidate = true;
                        runningValidate = true;
                    }
                  
                }
            }
        }
    }
}
