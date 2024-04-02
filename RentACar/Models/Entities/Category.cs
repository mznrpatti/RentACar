using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Models.Entities
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
