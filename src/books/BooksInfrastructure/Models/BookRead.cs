using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Models
{
    public class BookRead
    {
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Authors { get; set; }
        public string CoverImg { get; set; }
        public int CopiesCount { get; set; }
        public BookRead(Book book)
        {
            this.ISBN = book.ISBN;
            this.Title = book.Title;
            this.Description = book.Description;
            this.Authors = book.Authors;
            this.CoverImg = book.CoverImg;
            this.CopiesCount = 0;
        } 
        public BookRead() { }
    }
}
