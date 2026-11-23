
using BookStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Xml.Linq;

namespace BookStoreApp.DAL
{
    public class BookRepository
    {
        // communication with database
        // Select *
        // Select by id
        // Insert
        // delete
        // update

        private DatabaseConnection _connection = new DatabaseConnection();

        public void AddBook(Book book)
        {
            SqlConnection conn = _connection.getConnection();

            string insertQuery = "INSERT INTO Books (Title, Price, Stock, AuthorId) Values(@Title, @Price, @Stock, @AuthorId)";

            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@Price", (object)book.Price ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Stock", (object)book.Stock ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AuthorId", (object)book.AuthorId ?? DBNull.Value);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Book> GetAllBooks()
        {
            var books = new List<Book>();
            string selectQuery = "SELECT BookId, Title, Price, Stock, AuthorId FROM Books";

            using (SqlConnection conn = _connection.getConnection())
            using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var book = new Book
                        {
                            BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("Price")),
                            Stock = reader.IsDBNull(reader.GetOrdinal("Stock")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Stock")),
                            AuthorId = reader.IsDBNull(reader.GetOrdinal("AuthorId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("AuthorId"))
                        };

                        books.Add(book);
                    }
                }
            }

            return books;
        }

        // Search by ID
        public Book GetBookById(int id)
        {
            Book book = null;

            SqlConnection conn = _connection.getConnection();

            string searchQuery = "SELECT BookId, Title, Price, Stock, AuthorId FROM Books WHERE BookId = @Id";

            SqlCommand cmd = new SqlCommand(searchQuery, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                book = new Book
                {
                    BookId = Convert.ToInt32(reader["BookId"]),
                    Title = reader["Title"].ToString(),
                    Price = reader["Price"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["Price"]),
                    Stock = reader["Stock"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["Stock"]),
                    AuthorId = reader["AuthorId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["AuthorId"])
                };
            }

            return book;
        }

        public void UpdateBook(Book book)
        {
            string updateQuery = "UPDATE Books SET Title = @Title, Price = @Price, Stock = @Stock, AuthorId = @AuthorId WHERE BookId = @Id";

            SqlConnection conn = _connection.getConnection();

            SqlCommand cmd = new SqlCommand(updateQuery, conn);
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@Price", (object)book.Price ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Stock", (object)book.Stock ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@AuthorId", (object)book.AuthorId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Id", book.BookId);

            conn.Open();

            cmd.ExecuteNonQuery();
        }

        public void DeleteBook(int id)
        {
            string deleteQuery = "DELETE FROM Books WHERE BookId = @Id";

            SqlConnection conn = _connection.getConnection();
            SqlCommand cmd = new SqlCommand(deleteQuery, conn);

            cmd.Parameters.AddWithValue("@Id", id);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
