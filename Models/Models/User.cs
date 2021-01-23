using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class User : Member
    {
        public User()
        {
            DateOfRegistration = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Role = Role.User;
        }
        public User(string first, string last, int age, string username, string password, SubscriptionType subscriptionType, int id)
        {
            DateOfRegistration = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            FirstName = first;
            LastName = last;
            Age = age;
            UserName = username;
            Password = password;
            Role = Role.User;
            SubscriptionType = subscriptionType;
            Id = id;
        }
        public SubscriptionType SubscriptionType { get; set; }
        public List<string> Movies = new List<string>();
    }
}
