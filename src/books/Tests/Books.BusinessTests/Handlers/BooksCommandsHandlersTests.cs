using Xunit;
using Moq;
using Books.Business.Commands;
using Books.Infrastructure.Models;
using Books.Infrastructure.Contracts;

namespace Books.Business.Handlers.Tests
{
    public class BooksCommandsHandlersTests
    {
        private readonly Mock<IBookRepository<Book>> _repositoryMock;

        public BooksCommandsHandlersTests()
        {
            _repositoryMock = new Mock<IBookRepository<Book>>();
        }

        [Fact()]
        public async void CreateBookHandlerTest()
        {
            // Arrange
            CreateBookCommand request = new CreateBookCommand("11111111111", "author", "title", "description", "coverImg");
            _repositoryMock.Setup(x => x.Create(It.IsAny<Book>()));
            CreateBookHandler handler = new CreateBookHandler(_repositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _repositoryMock.VerifyAll();
        }

        [Fact()]
        public async void DeleteBookHandlerTest()
        {
            // Arrange
            DeleteBookCommand request = new DeleteBookCommand("11111111111");
            Book book = new Book(1, "11111111111", "author", "title", "description", "coverImg");
            _repositoryMock.Setup(x => x.Delete(It.IsAny<string>()));
            _repositoryMock.Setup(x => x.GetByISBN(It.IsAny<string>())).ReturnsAsync(book);
            DeleteBookHandler handler = new DeleteBookHandler(_repositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _repositoryMock.VerifyAll();
        }
    }
}