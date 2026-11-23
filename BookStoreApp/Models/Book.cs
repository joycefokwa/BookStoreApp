
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreApp.Models
{
    public class Book
    {
        private int bookId;
        private string title;
        private decimal? price;
        private int? stock;
        private int? authorId;

        public Book()
        {

        }

        public Book(int bookId, string title, decimal? price, int? stock, int? authorId)
        {
            this.bookId = bookId;
            this.title = title;
            this.price = price;
            this.stock = stock;
            this.authorId = authorId;
        }

        public int BookId { get => bookId; set => bookId = value; }
        public string Title { get => title; set => title = value; }
        public decimal? Price { get => price; set => price = value; }
        public int? Stock { get => stock; set => stock = value; }
        public int? AuthorId { get => authorId; set => authorId = value; }
    }
}
