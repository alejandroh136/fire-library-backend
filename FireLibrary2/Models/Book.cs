using System.ComponentModel.DataAnnotations;

namespace FireLibrary2.Models
{
    public class Book
    {
        [Key]
        public string? Isbn { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Publisher { get; set; }
        [Required]
        public string? Language { get; set; }
        [Required]
        public int Pages { get; set; }
        [Required]
        public string? AuthorName { get; set; }
        [Required]
        public string? Synopsis { get; set; }
        [Required]
        public string? Excerpt { get; set; }
        [Required]
        public int TotalCopies { get; set; } = 5;
        [Required]
        public int AvalableCopies { get; set; } = 5;
        [Required]
        public string Genre { get; set; }

        public List<Order> Orders { get; set; }

    }
}
