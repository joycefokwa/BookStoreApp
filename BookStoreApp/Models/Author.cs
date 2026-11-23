using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreApp.Models
{
    public class Author
    {
        private int authorId;
        private string name;
        public Author() 
        {

        }

        public Author(int authorId, string name)
        { 
       
            this.authorId = authorId;
            this.name = name;

        }
         public int AuthorId { get => authorId; set => authorId=value; }
        public string Name { get => name; set => name=value; }
    }
}