﻿using MovieStore.Models;
using MovieStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public static class StaticDb
    {
        public static List<Movie> Movies = new List<Movie>()
        {
            new Movie()
            {
                Title = "Harry Potter and the philosophers stone",
                Description = "Harry learns that his parents were wizards and were killed by the evil wizard Voldemort (Richard Bremmer), a truth that was hidden from him all these years. He embarks for his new life as a student, gathering two good friends Ron Weasley (Rupert Grint) and Hermione Granger (Emma Watson) along the way.",
                ReleaseDate = new DateTime(2001,11,04),
                Genre = Genre.Fiction
            },            
            new Movie()
            {
                Title = "Harry Potter and the chamber of secrets",
                Description = "The second instalment of boy wizard Harry Potter's adventures at Hogwarts School of Witchcraft and Wizardry, based on the novel by JK Rowling. A mysterious elf tells Harry to expect trouble during his second year at Hogwarts, but nothing can prepare him for trees that fight back, flying cars, spiders that talk and deadly warnings written in blood on the walls of the school.",
                ReleaseDate = new DateTime(2002,11,03),
                Genre = Genre.Fiction
            },            
            new Movie()
            {
                Title = "Harry Potter and the prisoner of Azkaban",
                Description = "Harry Potter's (Daniel Radcliffe) third year at Hogwarts starts off badly when he learns deranged killer Sirius Black (Gary Oldman) has escaped from Azkaban prison and is bent on murdering the teenage wizard. While Hermione's (Emma Watson) cat torments Ron's (Rupert Grint) sickly rat, causing a rift among the trio, a swarm of nasty Dementors is sent to protect the school from Black. A mysterious new teacher helps Harry learn to defend himself, but what is his secret tie to Sirius Black?",
                ReleaseDate = new DateTime(2004,05,31),
                Genre = Genre.Fiction
            }
        };
        public static List<User> users = new List<User>()
        {
            new User("Jim", "Beasly", 30, "JimBim", "pass", SubscriptionType.Annually),
            new User("Bob", "Smith", 50, "Bob", "pass", SubscriptionType.Annually),
            new User("Mike", "Johnsonson", 20, "Mike123", "pass", SubscriptionType.Annually),
            new User("Marko", "Selchanec", 28, "Mar", "Mar", SubscriptionType.Annually),
        };

        public static List<Employee> employees = new List<Employee>()
        {
            new Employee("Mike", "Mikovich", 25, "MikeBaby", "12345", 160),
            new Employee("John", "Malkovich", 35, "Mar", "Mar", 159)
        };
    }
}
