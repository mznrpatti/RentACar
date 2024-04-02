using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Models.Entities
{
    public class Sale
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public string Description { get; set; }
        public int Percentage { get; set; }
    }
}
