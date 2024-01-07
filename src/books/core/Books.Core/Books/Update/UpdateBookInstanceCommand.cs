using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.Update
{
    public class UpdateBookInstanceCommand : IRequest<BookInstance>
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }
    }
}
