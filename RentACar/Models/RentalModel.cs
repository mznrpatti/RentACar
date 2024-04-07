namespace RentACar.Models
{
    public class RentalModel
    {
        public int CarId { get; set; }
        public int UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime Created {  get; set; }
    }
}
