namespace RentACar.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public CarModel Car { get; set; }
        public string Description { get; set; }
        public int Percent { get; set; }
    }
}
