using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class Employee : Member
    {
        public Employee() { }
        public Employee(string first, string last, int age, string username, string password, int hours,int id)
        {
            DateOfRegistration = DateTime.Now;
            FirstName = first;
            LastName = last;
            Age = age;
            UserName = username;
            Password = password;
            Role = Role.Employee;
            HoursPerMonth = hours;
            Salary = 300;
            Id = id;
            SetBonus();
            SetSalary();
        }
        private int Salary { get; set; }
        public int HoursPerMonth { get; set; }
        public double? Bonus { get; set; }
        public void SetBonus()
        {
            if (HoursPerMonth > 160)
            {
                Bonus = 0.3;
            }
            else if (HoursPerMonth <= 160)
            {
                Bonus = null;
            }
        }
        public void SetSalary()
        {
            if (Bonus != null)
            {
                double? newSalary = (double)Salary + ((double)Salary * Bonus);
                Salary = (int)newSalary;
            }
        }
        public void GetSalary()
        {
            Console.WriteLine(Salary);
        }
    }
}
