using Books.Core.Responses;
using MediatR;

namespace Books.Core.GetById
{
    public class GetBookInstanceByIdQuery : IRequest<BookInstanceResponse>
    {
        public int Id { get; set; }
    }
}
