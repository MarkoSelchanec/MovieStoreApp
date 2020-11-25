using MovieStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Models
{
    public class User : Member
    {
        public User()
        {
            DateOfRegistration = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Role = Role.User;
        }
        public User(string first, string last, int age, string username, string password, SubscriptionType subscriptionType)
        {
            DateOfRegistration = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            FirstName = first;
            LastName = last;
            Age = age;
            UserName = username;
            Password = password;
            Role = Role.User;
            SubscriptionType = subscriptionType;
        }
        public int MemberNumber { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public List<Movie> Movies = new List<Movie>();
    }
}
