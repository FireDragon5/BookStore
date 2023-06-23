using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Book_Store_Website.Models
{
    public class Book
    {
        // Must be a int
        [Key]
        public int Id { get; set; }

        // Must be a string
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        // Must be a string
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        // Must be a string
        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        //Price
        [Required]
        [Range(1, 1000)]
        public double Price { get; set; }

        public List<Book> Books { get; private set; }

        public Book()
        {
        }

        public Book(int id, string title, string description, string author, double price)
        {
            Id = id;
            Title = title;
            Description = description;
            Author = author;
            Price = price;
        }

        public static class BookSearch
        {
            public static List<Book>? getSearchedBook()
            {
                string jsonContent = File.ReadAllText("Data/books.json");
                List<Book>? books = JsonConvert.DeserializeObject<List<Book>>(jsonContent);
                return books;
            }

            public static List<Book>? books = LoadBooksFromJson("./Data/books.json");

            //Search for book title or author
            public static List<Book>? Search(string? title, string? author)
            {
                if (title != null)
                {
                    books = books?.Where(book => book.Title.Contains(title)).ToList();
                }

                if (author != null)
                {
                    books = books?.Where(book => book.Author.Contains(author)).ToList();
                }

                return books;
            }
        }

        //Get the books info throught its id form the json file

        public Book? GetBook(int id)
        {
            List<Book>? books = LoadBooksFromJson("./Data/books.json");
            Book? book = books?.Find(x => x.Id == id);

            return book;
        }

        public static List<Book>? LoadBooksFromJson(string filePath)
        {
            string jsonData = File.ReadAllText(filePath);
            List<Book>? books = JsonConvert.DeserializeObject<List<Book>>(jsonData);
            return books;
        }
    }

    //Recommended books exsept the books that is being viewed

    public class RecommendedBooks
    {
        public static List<Book>? GetRecommendedBooks(int id)
        {
            //Only show 3 books

            List<Book>? books = Book.LoadBooksFromJson("./Data/books.json");
            books = books?.Where(x => x.Id != id).Take(3).ToList();
            return books;
        }
    }
}