using Books.Core.Responses;
using MediatR;

namespace Books.Core.GetAll
{
    public class GetAllBooksQuery : IRequest<List<BookResponse>>
    {
        public bool IncludeInPreview { get; set; }
    }
}
