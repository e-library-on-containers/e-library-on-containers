using Books.Core.Responses;
using MediatR;

namespace Books.Core.GetByISBN
{
    public class GetBookByISBNQuery : IRequest<BookResponse>
    {
        public string ISBN { get; set; }
    }
}
