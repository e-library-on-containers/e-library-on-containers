using Books.Core.Responses;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.GetAll
{
    public class GetAllBookInstancesHandler : IRequestHandler<GetAllBookInstancesQuery, List<BookInstanceResponse>>
    {
        private readonly IBookInstancesRepository<BookInstance> _bookInstancesRepository;

        public GetAllBookInstancesHandler(IBookInstancesRepository<BookInstance> bookInstancesRepository)
        {
            _bookInstancesRepository = bookInstancesRepository;
        }

        public async Task<List<BookInstanceResponse>> Handle(GetAllBookInstancesQuery request, CancellationToken cancellationToken)
        {
            List<BookInstance> bookInstances = await _bookInstancesRepository.GetAll();
            List<BookInstanceResponse> response = new List<BookInstanceResponse>();
            foreach (BookInstance bookInstance in bookInstances)
            {
                response.Add(new BookInstanceResponse(bookInstance));
            }
            return response;
        }
    }
}
