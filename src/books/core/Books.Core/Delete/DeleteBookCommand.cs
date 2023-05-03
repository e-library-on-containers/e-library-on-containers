using MediatR;

namespace Books.Core.Delete
{
    public class DeleteBookCommand : IRequest<int>
    {
        public string ISBN { get; set; }
    }
}
