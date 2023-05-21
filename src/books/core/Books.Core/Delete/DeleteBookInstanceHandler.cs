using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;

namespace Books.Core.Delete
{
    public class DeleteBookInstanceHandler : IRequestHandler<DeleteBookInstanceCommand, string>
    {
        private readonly IBookInstancesRepository<BookInstance> _bookInstancesRepository;
        public DeleteBookInstanceHandler(IBookInstancesRepository<BookInstance> bookInstancesRepository)
        {
            _bookInstancesRepository = bookInstancesRepository;
        }
        public async Task<string> Handle(DeleteBookInstanceCommand request, CancellationToken cancellationToken)
        {
            string ISBN;
            try
            {
                BookInstance bookInstance = await _bookInstancesRepository.GetById(request.Id);
                ISBN = bookInstance.ISBN;
                await _bookInstancesRepository.Delete(request.Id);
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }

            return ISBN;
        }
    }
}
