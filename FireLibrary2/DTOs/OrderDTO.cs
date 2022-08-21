namespace FireLibrary2.DTOs
{
    public class OrderDTO
    { 
        public int orderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateLent { get; set; }
        public DateTime DateDue { get; set; }

        //DONT FORGET TO INSTANTIATE THE LIST
        public List<BookDTO> Books { get; set; } = new List<BookDTO>();
    }
}
