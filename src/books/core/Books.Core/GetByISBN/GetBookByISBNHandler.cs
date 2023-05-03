using Books.Core.Responses;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.GetByISBN
{
    public class GetBookByISBNHandler : IRequestHandler<GetBookByISBNQuery, BookResponse>
    {
        private readonly IBookRepository<BookRead> booksReadRepository;
        private readonly IBookInstancesRepository<BookInstance> bookInstancesRepository;

        public GetBookByISBNHandler(IBookRepository<BookRead> booksReadRepository, IBookInstancesRepository<BookInstance> bookInstancesRepository)
        {
            this.booksReadRepository = booksReadRepository;
            this.bookInstancesRepository = bookInstancesRepository;
        }

        public async Task<BookResponse> Handle(GetBookByISBNQuery request, CancellationToken cancellationToken)
        {
            BookRead book = await booksReadRepository.GetByISBN(request.ISBN);
            List<BookInstance> bookInstances = await bookInstancesRepository.GetByISBN(request.ISBN, true);
            BookResponse result = new BookResponse(book, bookInstances.Count>0);
            return result;
        }
    }
}
