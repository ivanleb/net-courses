using System.ComponentModel.DataAnnotations;

namespace ORM.Core.Model
{
    public class Listing
    {
        [Key]
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
