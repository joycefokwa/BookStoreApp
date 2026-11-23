using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookStoreApp.DAL
{
    public class DatabaseConnection
    {
        // add my connection string
        // Address
        private string connectionString = ConfigurationManager.ConnectionStrings["BookDb"].ConnectionString;

        // Other functions They need SqlConnection such as Insert, Delete, Edit , Create , GetBookByID
        // Can I create a function to send Connection string Then I can reuse it multiple times

        public SqlConnection getConnection()
        {
            // object of Sqlconnection
            // I will send the data for connection to database
            // TO DO
            return new SqlConnection(connectionString);
        }
    }
}