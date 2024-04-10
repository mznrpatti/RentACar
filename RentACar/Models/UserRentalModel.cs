namespace RentACar.Models
{
    public class UserRentalModel
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public int UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime Created { get; set; }
    }
}
