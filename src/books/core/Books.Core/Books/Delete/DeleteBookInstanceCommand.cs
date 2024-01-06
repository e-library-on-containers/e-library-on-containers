using MediatR;

namespace Books.Core.Delete
{
    public class DeleteBookInstanceCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
