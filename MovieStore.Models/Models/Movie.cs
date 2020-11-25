using MovieStore.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Models
{
    public class Movie
    {
        public Movie()
        {
            SetPrice();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Genre Genre { get; set; }
        private int Price { get; set; }
        public void SetPrice()
        {
            if(ReleaseDate.Year < 2000)
            {
                Price = new Random().Next(100, 200);
            }
            if(ReleaseDate.Year <= 2010)
            {
                Price = new Random().Next(200, 300);
            }
            else
            {
                Price = new Random().Next(300, 500);
            }
        }
    }
}
