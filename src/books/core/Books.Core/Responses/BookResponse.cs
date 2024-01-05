using Books.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Responses
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Authors { get; set; }
        public string CoverImg { get; set; }
        public bool IsAvailable { get; set; }
        public int CopiesCount { get; set; }
        public bool InPreview { get; set; }

        public BookResponse(BookRead book, bool isAvailable)
        {
            Id = book.BookId;
            Title = book.Title;
            Description = book.Description;
            Authors = book.Authors;
            CoverImg = book.CoverImg;
            ISBN = book.ISBN;
            CopiesCount = book.CopiesCount;
            IsAvailable = isAvailable;
            InPreview = book.InPreview;
        }
    }
}
