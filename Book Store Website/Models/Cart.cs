using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book_Store_Website.Models
{
    public class ItemsInCart
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string? Author { get; set; }

        [Required]
        public double Price { get; set; }
    }

    public class Cart
    {
        public List<ItemsInCart> Items { get; set; } = new List<ItemsInCart>();

        public List<ItemsInCart> GetCart()
        {
            return Items;
        }

        // Display the items in the cart to the console
        public void DisplayCart()
        {
            foreach (var item in Items)
            {
                Console.WriteLine($"Book ID: {item.BookId}\nTitle: {item.Title}\nQuantity: {item.Quantity}\nAuthor: {item.Author}\nPrice: {item.Price}\n");
            }
        }

        public void AddBookToCart(int bookId, string title, string author, int quantity, double price)
        {
            ItemsInCart item = new ItemsInCart
            {
                BookId = bookId,
                Title = title,
                Quantity = quantity,
                Author = author,
                Price = price
            };

            Items.Add(item);
        }

        public void RemoveFromCart(int bookId)
        {
            Items.RemoveAll(x => x.BookId == bookId);
        }

        public void ClearCart()
        {
            Items.Clear();
        }

        public void UpdateQuantity(int bookId, int quantity)
        {
            var item = Items.Find(x => x.BookId == bookId);
            if (item != null)
            {
                item.Quantity = quantity;

                if (quantity == 0)
                {
                    RemoveFromCart(bookId);
                }
            }
        }
    }
}