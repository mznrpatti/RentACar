namespace RentACar.Models
{
    public class Car
    {
        public int Id {  get; set; }
        public Category Category { get; set; }
        public string Brand {  get; set; }
        public string Model { get; set; }
        public int DailyPrice { get; set; }
    }
}
