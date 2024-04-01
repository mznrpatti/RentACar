namespace RentACar.Models
{
    public class Rental
    {
        public int id {  get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set;}
        public DateTime created {  get; set; }
        public Car car { get; set; }
        public User user { get; set; }
    }
}
