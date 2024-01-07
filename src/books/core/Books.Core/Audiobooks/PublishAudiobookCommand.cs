using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.Audiobooks;

public class PublishAudiobookCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class PublishAudiobookCommandHandler : IRequestHandler<PublishAudiobookCommand, Unit>
{
    private readonly IRepository<Audiobook> _audiobookRepository;

    public PublishAudiobookCommandHandler(IRepository<Audiobook> audiobookRepository)
    {
        _audiobookRepository = audiobookRepository;
    }
    
    public async Task<Unit> Handle(PublishAudiobookCommand request, CancellationToken cancellationToken)
    {
        var audiobook = await _audiobookRepository.GetById(request.Id);
        
        audiobook.InPreview = false;
        await _audiobookRepository.Update(audiobook);

        return Unit.Value;
    }
}