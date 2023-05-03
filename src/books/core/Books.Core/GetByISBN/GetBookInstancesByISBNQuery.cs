using Books.Core.Responses;
using MediatR;

namespace Books.Core.GetByISBN
{
    public class GetBookInstancesByISBNQuery : IRequest<List<BookInstanceResponse>>
    {
        public string ISBN { get; set; }
        public bool isAvailable { get; set; }
    }
}
