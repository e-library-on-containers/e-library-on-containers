using Books.Core.Responses;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.GetByISBN
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
