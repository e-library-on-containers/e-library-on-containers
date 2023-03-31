using Books.Core.Queries;
using Books.Core.Responses;
using Books.Core.Commands;
using Books.Core.Mapper;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Handlers
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
            await _booksRepository.Create(book);
            Book response = await _booksRepository.GetByISBN(book.ISBN);
            return response;
        }
    }
}
