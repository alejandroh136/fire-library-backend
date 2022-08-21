using FireLibrary2.DTOs;

namespace FireLibrary2.Data
{
    public interface IRepository
    {
        //Book stuff
        Task<List<BookDTO>> GetAllBooksAsync();
        Task<BookDTO> GetBookAsync(string isbn);
        Task<List<BookDTO>> SearchBooksGenreAsync(string genre);
        Task<List<BookDTO>> SearchBooksAuthorAsync(string filter);
        Task<List<BookDTO>> SearchBooksTitleAsync(string filter);

        //Customer stuff
        Task<List<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO> GetCustomerByIdAsync(int id);
        Task<List<OrderDTO>> GetCustomerOrdersASync(int customerId);
        Task UpdateCustomer(CustomerDTO request);

        //Order Stuff
        Task<string> PostOrderAsync(OrderDTO request);
        Task<OrderDTO> GetOrderAsync(int id);
        Task ReturnBooksAsync(OrderDTO request);
        Task ReturnOneBookAsync(ReturnDTO request);


    }
}
