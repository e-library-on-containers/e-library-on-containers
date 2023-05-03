using Books.Core.Responses;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.GetById
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
