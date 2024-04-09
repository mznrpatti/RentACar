using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Models.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<UserRole> Roles { get; set; }
        public virtual List<Rental> Rentals { get; set; }
    }
}
