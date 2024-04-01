﻿namespace RentACar.Models
{
    public class Rental
    {
        public int Id {  get; set; }
        public User User { get; set; }
        public Car Car { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set;}
        public DateTime Created {  get; set; }
    }
}
