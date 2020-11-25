using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Models
{
    public class Member
    {
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
