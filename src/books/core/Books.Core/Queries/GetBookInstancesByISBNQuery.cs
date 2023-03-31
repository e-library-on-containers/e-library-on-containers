using Books.Core.Responses;
using Books.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Queries
{
    public class GetBookInstancesByISBNQuery : IRequest<List<BookInstanceResponse>>
    {
        public string ISBN { get; private set; }
        public bool isAvailable { get; private set; }
        public GetBookInstancesByISBNQuery(string ISBN, bool isAvailable)
        {
            this.ISBN = ISBN;
            this.isAvailable = isAvailable;
        }
    }
}
