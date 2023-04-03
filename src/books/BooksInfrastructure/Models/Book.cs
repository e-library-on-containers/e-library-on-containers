using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }  
        public string Description { get; set; }
        public string Authors { get; set; }
        public string CoverImg { get; set; }

        public Book() {}

        public Book(int bookId, string iSBN, string title, string description, string authors, string coverImg)
        {
            BookId = bookId;
            ISBN = iSBN;
            Title = title;
            Description = description;
            Authors = authors;
            CoverImg = coverImg;
        }
    }
}
