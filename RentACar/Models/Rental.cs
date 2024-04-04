using RentACar.Models.Entities;

namespace RentACar.Models
{
    public class Rental
    {
        public int Id {  get; set; }
        public User User { get; set; }
        public CarModel Car { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set;}
        public DateTime Created {  get; set; }
    }
}
