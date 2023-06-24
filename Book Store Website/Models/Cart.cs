using Newtonsoft.Json;
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
        public string? Title { get; set; }

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

        // Return the json file
        public List<ItemsInCart> getItemsFormJson()
        {
            string jsonContent = File.ReadAllText("Data/cart.json");
            List<ItemsInCart>? items = JsonConvert.DeserializeObject<List<ItemsInCart>>(jsonContent);

            foreach (var item in items)
            {
                Items.Add(new ItemsInCart
                {
                    BookId = item.BookId,
                    Title = item.Title,
                    Author = item.Author,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }

            return Items;
        }

        public void AddBookToCart(int bookId, string title, string author, int quantity, double price)
        {
            //Save the items to the cart.json
            Items.Add(new ItemsInCart
            {
                BookId = bookId,
                Title = title,
                Author = author,
                Quantity = quantity,
                Price = price
            });

            //Save the items to the cart.json
            SaveItemsToCart();
        }

        private void SaveItemsToCart()
        {
            string file = "Data/cart.json";

            string json = File.ReadAllText(file);
            List<ItemsInCart>? itemsInCarts = JsonConvert.DeserializeObject<List<ItemsInCart>>(json);

            bool isItemInJson = false;

            //If item is in json just update the quantity
            foreach (var item in itemsInCarts)
            {
                if (item.BookId == Items[Items.Count - 1].BookId)
                {
                    item.Quantity = Items[Items.Count - 1].Quantity + 1;
                    isItemInJson = true;
                    break;
                }
            }

            //If item is not in json add it
            if (!isItemInJson)
            {
                itemsInCarts.Add(new ItemsInCart
                {
                    BookId = Items[Items.Count - 1].BookId,
                    Title = Items[Items.Count - 1].Title,
                    Author = Items[Items.Count - 1].Author,
                    Quantity = Items[Items.Count - 1].Quantity,
                    Price = Items[Items.Count - 1].Price
                });
            }

            string newJsonResult = JsonConvert.SerializeObject(itemsInCarts, Formatting.Indented);

            File.WriteAllText(file, newJsonResult);

            Console.WriteLine("Items saved to cart.json");
        }

        public void RemoveFromCart(int bookId)
        {
            //Remove the items from the cart.json
            string file = "Data/cart.json";
            string json = File.ReadAllText(file);

            List<ItemsInCart>? itemsInCarts = JsonConvert.DeserializeObject<List<ItemsInCart>>(json);

            foreach (var item in itemsInCarts)
            {
                if (item.BookId == bookId)
                {
                    itemsInCarts.Remove(item);
                    break;
                }
            }

            string newJsonResult = JsonConvert.SerializeObject(itemsInCarts, Formatting.Indented);

            File.WriteAllText(file, newJsonResult);

            Console.WriteLine("Items removed from cart.json");
        }

        public void ClearCart()
        {
            Items.Clear();
        }

        public void UpdateQuantity(int bookId, int quantity)
        {
            //Update the quantity in the cart.json
            string file = "Data/cart.json";
            string json = File.ReadAllText(file);

            List<ItemsInCart>? itemsInCarts = JsonConvert.DeserializeObject<List<ItemsInCart>>(json);

            foreach (var item in itemsInCarts)
            {
                if (item.BookId == bookId)
                {
                    item.Quantity = quantity;
                    break;
                }
            }

            string newJsonResult = JsonConvert.SerializeObject(itemsInCarts, Formatting.Indented);

            File.WriteAllText(file, newJsonResult);

            Console.WriteLine("Items quantity updated in cart.json");
        }
    }
}