using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.Books;

public class PublishBookCommand : IRequest<Unit>
{
    public string ISBN { get; set; }
}

public class PublishBookCommandHandler : IRequestHandler<PublishBookCommand, Unit>
{
    private readonly IBookRepository<BookRead> _booksReadRepository;

    public PublishBookCommandHandler(IBookRepository<BookRead> booksReadRepository)
    {
        _booksReadRepository = booksReadRepository;
    }
    
    public async Task<Unit> Handle(PublishBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _booksReadRepository.GetByISBN(request.ISBN);

        book.InPreview = false;
        await _booksReadRepository.Update(book);

        return Unit.Value;
    }
}