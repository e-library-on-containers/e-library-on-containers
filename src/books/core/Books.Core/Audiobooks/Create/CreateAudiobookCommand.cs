using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.Audiobooks.Create;

public class CreateAudiobookCommand : IRequest<Audiobook>
{
    public int BookId { get; set; }
    public int Duration { get; set; }
}

public class CreateAudiobookCommandHandler : IRequestHandler<CreateAudiobookCommand, Audiobook>
{
    private readonly IRepository<Audiobook> _repository;

    public CreateAudiobookCommandHandler(IRepository<Audiobook> repository)
    {
        _repository = repository;
    }
    
    public async Task<Audiobook> Handle(CreateAudiobookCommand request, CancellationToken cancellationToken)
    {
        var audiobook = new Audiobook
        {
            BookId = request.BookId,
            InPreview = true,
            Duration = request.Duration
        };
        var id = await _repository.Add(audiobook);
        audiobook.Id = id;

        return audiobook;
    }
}