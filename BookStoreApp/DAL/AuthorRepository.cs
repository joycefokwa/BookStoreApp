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
    public class AuthorRepository
    {
        // communication with database
        // Select *
        // Select by id
        // Insert
        //delete
        //update

        // variableName means It will be used only in this class
        private DatabaseConnection _connection = new DatabaseConnection();
        public void AddAuthor(Author author)
        {
            // Get the address
            // Now I have Address, I will try to connect , I will hit the road
            SqlConnection conn = _connection.getConnection();
            string insertQuery = "INSERT INTO Authors (Name) Values(@Name)";

            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@Name", author.Name);

            conn.Open();
            cmd.ExecuteNonQuery();
        }


        public List<Author> GetAllAuthors()
        {

            var authors = new List<Author>();
            string selectQuery = "SELECT AuthorId, Name FROM Authors";

            using (SqlConnection conn = _connection.getConnection())
            using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var author = new Author
                        {
                            AuthorId = reader.GetInt32(reader.GetOrdinal("AuthorId")),
                            Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name"))
                        };
                        authors.Add(author);
                    }
                }
            }
            return authors;
        }
        // filter -- Search by ID

        public Author GetAuthorBYId(int id)
        {
            // return an object of Id
            Author author = null;

            SqlConnection conn = _connection.getConnection();

            string searchQuery = "SELECT AuthorId, Name FROM Authors WHERE AuthorId= @id";

            SqlCommand cmd = new SqlCommand(searchQuery, conn);
            cmd.Parameters.AddWithValue("Id", id);
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                author = new Author
                {
                    AuthorId = Convert.ToInt32(reader["AuthorId"]),
                    Name = reader["Name"].ToString()
                };
            }
            // search Author based on Id
            return author;
        }

        //Search by name --- instead of id i will use name 
        public void UpdateAuthor(Author author)
        {
            //primary key is unique, we cannot update it
            string updateQuery = "UPDATE Authors Set Name = @name WHERE AuthorId = @Id";

            SqlConnection conn = _connection.getConnection();

            SqlCommand cmd = new SqlCommand(updateQuery, conn);
            cmd.Parameters.AddWithValue("@Name", author.Name);
            cmd.Parameters.AddWithValue("@Id", author.AuthorId);

            conn.Open();

            cmd.ExecuteNonQuery();
            // row affected == 0 or 1
        }

        public void DeleteAuthor(int id)
        {
            string deleteQuery = "DELETE FROM Authors WHERE AuthorId = @Id";

            SqlConnection conn = _connection.getConnection();
            SqlCommand cmd = new SqlCommand(deleteQuery, conn);

            cmd.Parameters.AddWithValue("@Id", id);

            conn.Open();
            cmd.ExecuteNonQuery();

        }

    }
}