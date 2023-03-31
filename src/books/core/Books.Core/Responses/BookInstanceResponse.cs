using Books.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Responses
{
    public class BookInstanceResponse
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }

        public BookInstanceResponse(BookInstance bookInstance)
        {
            Id = bookInstance.InstanceId;
            ISBN = bookInstance.ISBN;
            IsAvailable = bookInstance.IsAvailable;
        }
    }
}
