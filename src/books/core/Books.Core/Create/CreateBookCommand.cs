using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.Create
{
    public class CreateBookCommand : IRequest<Book>
    {
        public string ISBN { get; set; }
        public string Authors { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImg { get; set; }
    }
}
