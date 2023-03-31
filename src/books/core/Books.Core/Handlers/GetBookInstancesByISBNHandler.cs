using Books.Core.Queries;
using Books.Core.Responses;
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
    public class GetBookInstancesByISBNHandler : IRequestHandler<GetBookInstancesByISBNQuery, List<BookInstanceResponse>>
    {
        private readonly IBookInstancesRepository<BookInstance> _bookInstancesRepository;

        public GetBookInstancesByISBNHandler(IBookInstancesRepository<BookInstance> bookInstancesRepository)
        {
            _bookInstancesRepository = bookInstancesRepository;
        }

        public async Task<List<BookInstanceResponse>> Handle(GetBookInstancesByISBNQuery request, CancellationToken cancellationToken)
        {
            List<BookInstance> bookInstances = await _bookInstancesRepository.GetByISBN(request.ISBN, request.isAvailable);
            List<BookInstanceResponse> response = new List<BookInstanceResponse>();
            foreach (BookInstance bookInstance in bookInstances)
            {
                response.Add(new BookInstanceResponse(bookInstance));
            }
            return response;
        }
    }
}
