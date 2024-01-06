using Books.Core.Mapper;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.Create
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IBookRepository<Book> _booksRepository;

        public CreateBookHandler(IBookRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = BookMapper.Mapper.Map<Book>(request);
            book.InPreview = true;
            await _booksRepository.Create(book);
            Book response = await _booksRepository.GetByISBN(book.ISBN);
            return response;
        }
    }
}
