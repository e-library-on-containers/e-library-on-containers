using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.Update
{
    public class UpdateBookCommand : IRequest<BookRead>
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImg { get; set; }
    }
}
