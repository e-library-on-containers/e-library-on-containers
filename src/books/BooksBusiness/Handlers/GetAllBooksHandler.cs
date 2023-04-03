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
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, List<BookResponse>>
    {
        private readonly IBookRepository<BookRead> booksReadRepository;
        private readonly IBookInstancesRepository<BookInstance> bookInstancesRepository;

        public GetAllBooksHandler(IBookRepository<BookRead> booksReadRepository, IBookInstancesRepository<BookInstance> bookInstancesRepository)
        {
            this.booksReadRepository = booksReadRepository;
            this.bookInstancesRepository = bookInstancesRepository;
        }

        public async Task<List<BookResponse>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            List<BookRead> books = await booksReadRepository.GetAll();
            List<BookResponse> result = new List<BookResponse>();
            foreach (BookRead book in books) 
            {
                List<BookInstance> bookInstances = await bookInstancesRepository.GetByISBN(book.ISBN, true);
                BookResponse bookResponse = new BookResponse(book, bookInstances.Count>0);
                result.Add(bookResponse);
            }
            return result;
        }
    }
}
