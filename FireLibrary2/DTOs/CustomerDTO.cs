using FireLibrary2.Models;

namespace FireLibrary2.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string Username { get; set; }
        public bool Canborrow { get; set; }
        public double Fines { get; set; }
        public int BookCount { get; set; }


        public static CustomerDTO CreateCustomerDTO(Customer customer)
        {
            CustomerDTO result = new();

            result.CustomerId = customer.CustomerId;
            result.Username = customer.Username;
            result.Canborrow = customer.CanBorrow;
            result.Fines = customer.Fines;
            result.BookCount = customer.BookCount;

            return result;
        }
    }
}
