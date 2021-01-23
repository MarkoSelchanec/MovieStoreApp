using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public Role Role { get; set; }
        public void DisplayInfo()
        {
            Console.WriteLine($"{FirstName} {LastName} | Registered on: {DateOfRegistration.ToShortDateString()}");
        }
    }
}
