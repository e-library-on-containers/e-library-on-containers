using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.GetById;

public class GetBookByInstanceIdQuery : IRequest<Book>
{
    public int InstanceId { get; set; }
}

public class GetBookByInstanceIdQueryHandler : IRequestHandler<GetBookByInstanceIdQuery, Book>
{
    private readonly IBookInstancesRepository<BookInstance> _bookInstancesRepository;

    public GetBookByInstanceIdQueryHandler(IBookInstancesRepository<BookInstance> bookInstancesRepository)
    {
        _bookInstancesRepository = bookInstancesRepository;
    }
    
    public async Task<Book> Handle(GetBookByInstanceIdQuery request, CancellationToken cancellationToken)
    {
        return await _bookInstancesRepository.GetInfoByInstanceId(request.InstanceId);
    }
}
