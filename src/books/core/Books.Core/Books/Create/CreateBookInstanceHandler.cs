using Books.Core.Mapper;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.Create
{
    public class CreateBookInstanceHandler : IRequestHandler<CreateBookInstanceCommand, string>
    {
        private readonly IBookInstancesRepository<BookInstance> _bookInstancesRepository;
        //private readonly IBookRepository<BookRead> _booksReadRepository;

        public CreateBookInstanceHandler(IBookInstancesRepository<BookInstance> bookInstancesRepository)
        {
            _bookInstancesRepository = bookInstancesRepository;
            //_booksReadRepository = booksReadRepository;
        }

        public async Task<string> Handle(CreateBookInstanceCommand request, CancellationToken cancellationToken)
        {
            var bookInstance = BookMapper.Mapper.Map<BookInstance>(request);
            await _bookInstancesRepository.Create(bookInstance);
            return request.ISBN;
        }
    }
}
