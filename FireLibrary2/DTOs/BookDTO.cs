using FireLibrary2.Models;

namespace FireLibrary2.DTOs
{
    public class BookDTO
    {

        public string Isbn { get; set; }

        public string? Title { get; set; }

        public string? Publisher { get; set; }

        public string? Language { get; set; }

        public int? Pages { get; set; }

        public string? AuthorName { get; set; }

        public string? Synopsys { get; set; }

        public string? Excerpt { get; set; }
        public string? Genre { get; set; }

        public int? TotalCopies { get; set; }

        public int? AvalableCopies { get; set; }

        //Creates bookDTOs from order object
        public static List<BookDTO> CreateBookDTOs(Order order)
        {
            List<BookDTO> result = new List<BookDTO>();

            foreach (var book in order.Books)
            {
                BookDTO tmpBook = new BookDTO();
                tmpBook.Title = book.Title;
                tmpBook.Isbn = book.Isbn;
                tmpBook.Publisher = book.Publisher;
                tmpBook.Language = book.Language;
                tmpBook.Pages = book.Pages;
                tmpBook.AuthorName = book.AuthorName;
                tmpBook.Synopsys = book.Synopsis;
                tmpBook.Excerpt = book.Excerpt;
                tmpBook.Genre = book.Genre;
                tmpBook.AvalableCopies = book.AvalableCopies;
                tmpBook.TotalCopies = book.TotalCopies;

                result.Add(tmpBook);
            }

            return result;
        }

        //Creates bookDTO from List<Books> list
        public static List<BookDTO> CreateBookDTOs(List<Book> books)
        {
            List<BookDTO> result = new List<BookDTO>();

            foreach (var book in books)
            {
                BookDTO tmpBook = new BookDTO();
                tmpBook.Title = book.Title;
                tmpBook.Isbn = book.Isbn;
                tmpBook.Publisher = book.Publisher;
                tmpBook.Language = book.Language;
                tmpBook.Pages = book.Pages;
                tmpBook.AuthorName = book.AuthorName;
                tmpBook.Synopsys = book.Synopsis;
                tmpBook.Excerpt = book.Excerpt;
                tmpBook.Genre = book.Genre;
                tmpBook.AvalableCopies = book.AvalableCopies;
                tmpBook.TotalCopies = book.TotalCopies;

                result.Add(tmpBook);
            }

            return result;
        }

        public static BookDTO CreateBookDTO(Book book)
        {
            BookDTO result = new BookDTO();

            result.Title = book.Title;
            result.Isbn = book.Isbn;
            result.Publisher = book.Publisher;
            result.Language = book.Language;
            result.Pages = book.Pages;
            result.AuthorName = book.AuthorName;
            result.Synopsys = book.Synopsis;
            result.Excerpt = book.Excerpt;
            result.Genre = book.Genre;
            result.AvalableCopies = book.AvalableCopies;
            result.TotalCopies = book.TotalCopies;

            return result;
        }


    }
}
