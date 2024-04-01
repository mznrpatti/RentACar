namespace RentACar.Models
{
    public class Sale
    {
        public int id { get; set; }
        public string description { get; set; }
        public int percent { get; set; }
        public Car car { get; set; }
    }
}
