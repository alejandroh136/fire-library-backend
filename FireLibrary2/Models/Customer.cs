using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FireLibrary2.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public bool CanBorrow { get; set; } = true;
        [Required]
        public double Fines { get; set; } = 0;
        [Required]
        public int BookCount { get; set; } = 0;

        public List<Order> Orders { get; set; } 

    }
}
