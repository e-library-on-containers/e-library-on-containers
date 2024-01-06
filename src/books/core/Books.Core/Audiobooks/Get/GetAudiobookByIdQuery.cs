using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.Audiobooks.Get;

public class GetAudiobookByIdQuery : IRequest<Audiobook>
{
    public int Id { get; set; }
}

public class GetAudiobookByIdQueryHandler : IRequestHandler<GetAudiobookByIdQuery, Audiobook>
{
    private readonly IRepository<Audiobook> _repository;

    public GetAudiobookByIdQueryHandler(IRepository<Audiobook> repository)
    {
        _repository = repository;
    }
    
    public async Task<Audiobook> Handle(GetAudiobookByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetById(request.Id);
    }
}