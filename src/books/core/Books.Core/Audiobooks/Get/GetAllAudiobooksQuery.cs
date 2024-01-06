using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.Audiobooks.Get;

public class GetAllAudiobooksQuery : IRequest<IEnumerable<Audiobook>>
{
    
}

public class GetAllAudiobooksQueryHandler : IRequestHandler<GetAllAudiobooksQuery, IEnumerable<Audiobook>>
{
    private readonly IRepository<Audiobook> _repository;

    public GetAllAudiobooksQueryHandler(IRepository<Audiobook> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Audiobook>> Handle(GetAllAudiobooksQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll();
    }
}