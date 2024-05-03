namespace RentACar.Models
{
    public class SaleModel
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string Description { get; set; }
        public int Percentage { get; set; }
        public double ChangedPrice { get; set; }
        
    }
}
