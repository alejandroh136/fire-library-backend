using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FireLibrary2.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public DateTime DateLent { get; set; }
        [Required]
        public DateTime DateDue { get; set; }

        //Navigation Property
        public List<Book> Books { get; set; }

    }
}
