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
    public class GetBookByISBNQuery : IRequest<BookResponse>
    {
        public string ISBN { get; private set; }

        public GetBookByISBNQuery(string ISBN)
        {
            this.ISBN = ISBN;
        }
    }
}
