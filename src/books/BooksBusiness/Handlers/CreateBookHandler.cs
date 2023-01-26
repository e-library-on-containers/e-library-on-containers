using Books.Business.Commands;
using Books.Business.Mapper;
using Books.Business.Queries;
using Books.Business.Responses;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Business.Handlers
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IBookRepository<Book> _booksRepository;

        public CreateBookHandler(IBookRepository<Book> booksRepository)
        {
            this._booksRepository = booksRepository;
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
