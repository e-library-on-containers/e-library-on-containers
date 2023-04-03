using Books.Business.Commands;
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
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, int>
    {
        private readonly IBookRepository<Book> _bookRepository;
        //private readonly IBookInstancesRepository<BookInstance> bookInstancesRepository;

        public DeleteBookHandler(IBookRepository<Book> bookRepository/*, IBookInstancesRepository<BookInstance> bookInstancesRepository*/)
        {
            _bookRepository = bookRepository;
            //this.bookInstancesRepository = bookInstancesRepository;
        }

        public async Task<int> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            int id;
            try
            {
                Book book = await _bookRepository.GetByISBN(request.ISBN);
                id = book.BookId;
                //await bookInstancesRepository.Delete(request.ISBN);
                await _bookRepository.Delete(request.ISBN);
            }
            catch(Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return id;
        }
    }
}
