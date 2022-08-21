using FireLibrary2.DTOs;
using FireLibrary2.Models;

namespace FireLibrary2.Data
{
    public class EFRepo : IRepository
    {
       
        private readonly DataContext _context;
        private readonly ILogger<EFRepo> _logger;

        public EFRepo(DataContext context, ILogger<EFRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Book Controller methods
        //Gets ALL books
        public async Task<List<BookDTO>> GetAllBooksAsync()
        {

            List<Book> books = await _context.Books.ToListAsync();
            List<BookDTO> result = BookDTO.CreateBookDTOs(books);

            return result;
        }

        //Get one book by isbn
        public async Task<BookDTO> GetBookAsync(string isbn)
        {
            var book = await _context.Books.FindAsync(isbn);
            BookDTO result = BookDTO.CreateBookDTO(book);

            return result;
        }
        //Gets ONE book
        public async Task<List<BookDTO>> SearchBooksGenreAsync(string genre)
        {
            List<BookDTO> result = new();
            List<Book> books = new();

            books = await _context.Books.Where(b => b.Genre.ToLower() == genre.ToLower()).ToListAsync();

            result = BookDTO.CreateBookDTOs(books);

            return result;
        }

        public async Task<List<BookDTO>> SearchBooksAuthorAsync(string author)
        {
            List<BookDTO> result = new List<BookDTO>();
            List<Book> books = new List<Book>();

            books = await _context.Books.Where(b => b.AuthorName.ToLower() == author.ToLower()).ToListAsync();

            result = BookDTO.CreateBookDTOs(books);

            return result;
        }

        public async Task<List<BookDTO>> SearchBooksTitleAsync(string title)
        {
            List<BookDTO> result = new List<BookDTO>();
            List<Book> books = new List<Book>();

            books = await _context.Books.Where(b => b.Title.ToLower() == title.ToLower()).ToListAsync();

            result = BookDTO.CreateBookDTOs(books);

            return result;
        }

        private bool BookExists(string id)
        {
            return (_context.Books?.Any(e => e.Isbn == id)).GetValueOrDefault();
        }


        //Customer Stuff
        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            List<CustomerDTO> result = new();
            var customers = await _context.Customers.ToListAsync();

            foreach (Customer customer in customers)
            {
                result.Add(CustomerDTO.CreateCustomerDTO(customer));
            }

            return result;
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(int customerId)
        {

            Customer customer = await _context.Customers.FindAsync(customerId);
            CustomerDTO result = CustomerDTO.CreateCustomerDTO(customer);

            return result;
        }

        public async Task<List<OrderDTO>> GetCustomerOrdersASync(int customerId)
        {
            //List of orders models from DB used to create the OrderDTO list for frontend
            List<Order> orders = new List<Order>();
            //List of OrderDTO's to send to front end
            List<OrderDTO> results = new List<OrderDTO>();

            //Gets list of orders from DB, serializes them right into orders list
            orders = await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();

            foreach (Order i in orders)
            {
                //Creates a temp orderDTO to then populate and be added to results list of orderDTOs to send off
                OrderDTO tempOrder = new OrderDTO();
                //Populates the temp orderDTO object
                tempOrder.orderId = i.OrderId;
                tempOrder.CustomerId = i.CustomerId;
                tempOrder.DateLent = i.DateLent;
                tempOrder.DateDue = i.DateDue;

                //Books must be converted from Book (model) to BookDTO
                List<BookDTO> bookDTOs = new();

                bookDTOs = BookDTO.CreateBookDTOs(i);

                tempOrder.Books = bookDTOs;

                //Adds tempOrder OrderDTO to list of OrderDTOs to send back
                results.Add(tempOrder);
            }
            return results;
        }

        public async Task UpdateCustomer(CustomerDTO request)
        {
            var customer = await _context.Customers.FirstAsync(cust => cust.Username == request.Username);

            customer.Fines = request.Fines;
            customer.CanBorrow = request.Canborrow;
            customer.BookCount = request.BookCount;

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.LogError(e, e.Message);
            }

            return;
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }

        //Order Stuff

        public async Task<string> PostOrderAsync(OrderDTO request)
        {
            //New Order to insert into DB
            var order = new Order();
            var books = new List<Book>();
            //Finds customer that came in from the OrderDTO customerID, returns its Customer Model from the DB
            var customer = await _context.Customers.FirstAsync(cust => cust.CustomerId == request.CustomerId);

            //checking if order would put user over 10 borrowed books
            if ((customer.BookCount + request.Books.Count()) > 10)
            {
                return "toomany";
            }

            //checking to see if there are duplicate books on the order
            if (request.Books.Count() != request.Books.Distinct().Count())
            {
                return "duplicate";
            }

            //Sets customerId of new order to the CustomerId that came in from the DTO
            order.CustomerId = request.CustomerId;

            //Puts book found in BookDTO list into a list of book models stored in the object model,
            //to then send to database. Additionally, it updates the books and decrements
            //the available copies by 1.
            foreach (BookDTO i in request.Books)
            {
                //finds book in DB
                var book = await _context.Books.FindAsync(i.Isbn);
                book.AvalableCopies -= 1;
                //Makes sure there are availble copies of the book
                if (book.AvalableCopies == 0)
                {
                    return "availability";
                }

                _context.Books.Update(book);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {

                }

                //Adds the book to the List<Book> that we will insert into the new order
                books.Add(book);
            }

            //Populating the Order model
            order.Books = books;
            order.DateLent = DateTime.Now;
            order.DateDue = order.DateLent.AddDays(14);

            //incrementing customer bookcount           
            customer.BookCount += order.Books.Count();



            //Adding the order model, updating the customer 
            _context.Orders.Add(order);
            _context.Customers.Update(customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {

            }

            return "success";
        }

        //get a specific order
        public async Task<OrderDTO> GetOrderAsync(int id)
        {
            Order order = await _context.Orders.Include(o => o.Books).FirstAsync(o => o.OrderId == id);

            OrderDTO result = new OrderDTO();

            result.orderId = order.OrderId;

            List<BookDTO> bookDTOs = new();

            bookDTOs = BookDTO.CreateBookDTOs(order);

            result.Books = bookDTOs;

            return result;
        }

        public async Task ReturnBooksAsync(OrderDTO request)
        {
            //Get the actual order from the DB
            Order order = await _context.Orders.FindAsync(request.orderId);
            //Get custtomer who's order it is
            Customer customer = await _context.Customers.FindAsync(order.CustomerId);
            //Cant remove entries in a foreach loop
            List<Book> booksToRemove = new();

            foreach (var i in order.Books)
            {
                //Gets the actual book from the db
                Book tmpBook = await _context.Books.FindAsync(i.Isbn);

                //Adds available copy to tmpBook
                tmpBook.AvalableCopies += 1;

                //Adds the book to the list of books to remove
                booksToRemove.Add(tmpBook);

                //Remove book from bookcount.
                customer.BookCount -= 1;

                _context.Books.Update(tmpBook);

                    try
                {
                   await _context.SaveChangesAsync();
                }
                    catch (DbUpdateException e)
                {
                    _logger.LogError(e, e.Message);
                }
                
            }

            //Removing books to remove from the order book list
            order.Books = order.Books.Except(booksToRemove).ToList();

            //Stages changes to order and customer to DB
            _context.Orders.Update(order);
            _context.Customers.Update(customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        public async Task ReturnOneBookAsync(ReturnDTO bookToReturn)
        {
            Book book = await _context.Books.FindAsync(bookToReturn.isbn);
            Order order = await _context.Orders.FindAsync(bookToReturn.orderId);
            Customer customer = await _context.Customers.FindAsync(order.CustomerId);

            book.AvalableCopies += 1;
            order.Books.RemoveAll(b => b.Isbn == bookToReturn.isbn);
            customer.BookCount -= 1;
            
            _context.Books.Update(book);
            _context.Orders.Update(order);
            _context.Customers.Update(customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, e.Message);
            }

        }

    }
}

