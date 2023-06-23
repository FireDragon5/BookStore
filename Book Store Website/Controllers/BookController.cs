using Book_Store_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store_Website.Controllers
{
    public class BookController : Controller
    {
        private readonly Cart? _cart;

        private readonly Book? _book;

        public BookController()
        {
            _cart = new Cart();
            _book = new Book();
        }

        public IActionResult Index()
        {
            Book book = new Book();

            ViewBag.Books = book.Books;

            //return the list of books
            return View(book.Books);
        }

        public IActionResult Search()
        {
            List<Book> books = Book.LoadBooksFromJson("./Data/books.json");

            return View(books);
        }

        // Search for book title or author
        [HttpPost]
        public IActionResult Search(string search)
        {
            List<Book>? books = Book.LoadBooksFromJson("./Data/books.json");
            ViewBag.Search = search;

            if (!string.IsNullOrEmpty(search))
            {
                books = books.Where(x => x.Title.ToLower().Contains(search.ToLower()) || x.Author.ToLower().Contains(search.ToLower())).ToList();
            }
            else
            {
                books = new List<Book>();
            }
            return View(books);
        }

        [HttpPost]
        public IActionResult GetAllBooks()
        {
            List<Book> books = Book.LoadBooksFromJson("./Data/books.json");
            return Json(books);
        }

        // AddToCart
        public IActionResult Cart()
        {
            //Get the list
            var cart = _cart.GetCart();

            if (cart == null)
            {
                return Content("Cart is empty");
            }

            return View(cart);
        }

        // AddToCart
        public IActionResult AddToCart(int id)
        {
            //Get the book
            var book = new Book().GetBook(id);

            //If book is null
            if (book == null)
            {
                return Content("Book not found");
            }

            //Add book to cart
            _cart.AddBookToCart(book.Id, book.Author, book.Title, 1, book.Price);

            //Get the cart list
            var cart = _cart.GetCart();

            return RedirectToAction("Cart", cart);
        }

        [HttpPost]
        public IActionResult Details()
        {
            return View();
        }

        //Details
        public IActionResult Details(int id)
        {
            var book = new Book().GetBook(id);

            ViewBag.Book = book;

            // If book is null
            if (book == null)
            {
                return Content("Book not found");
            }

            return View(book);
        }
    }
}