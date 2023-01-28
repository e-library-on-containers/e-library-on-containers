using Books.Business.Queries;
using Books.Business.Responses;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using Books.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Business.Handlers
{
    public class GetBookInstanceByIdHandler : IRequestHandler<GetBookInstanceByIdQuery, BookInstanceResponse>
    {
        private readonly IBookInstancesRepository<BookInstance> _bookInstancesRepository;

        public GetBookInstanceByIdHandler(IBookInstancesRepository<BookInstance> bookInstancesRepository)
        {
            _bookInstancesRepository = bookInstancesRepository;
        }

        public async Task<BookInstanceResponse> Handle(GetBookInstanceByIdQuery request, CancellationToken cancellationToken)
        {
            BookInstance bookInstance = await _bookInstancesRepository.GetById(request.Id);
            BookInstanceResponse response = new BookInstanceResponse(bookInstance);
            return response;
        }
    }
}
