namespace RentACar.Models
{
    public class Car
    {
        public int id {  get; set; }
        public string brand {  get; set; }
        public string model { get; set; }
        public int daily_price { get; set; }
        public Category category { get; set; }
    }
}
