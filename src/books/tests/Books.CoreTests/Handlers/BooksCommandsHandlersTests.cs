using Xunit;
using Moq;
using Books.Core.Commands;
using Books.Infrastructure.Models;
using Books.Infrastructure.Contracts;

namespace Books.Core.Handlers.Tests
{
    public class BooksCommandsHandlersTests
    {
        private readonly Mock<IBookRepository<Book>> _bookRepositoryMock;

        public BooksCommandsHandlersTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository<Book>>();
        }

        [Fact]
        public async void CreateBookHandler_Calls_BooksRepository_Delete()
        {
            // Arrange
            CreateBookCommand request = new CreateBookCommand { ISBN = "11111111111", Authors = "author", Title = "title", Description = "description", CoverImg = "coverImg" };
            _bookRepositoryMock.Setup(x => x.Create(It.IsAny<Book>()));
            CreateBookHandler handler = new CreateBookHandler(_bookRepositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _bookRepositoryMock.Verify(x => x.Create(It.IsAny<Book>()), Times.Once());
        }

        [Fact]
        public async void DeleteBookHandler_Calls_BooksRepository_Delete()
        {
            // Arrange
            DeleteBookCommand request = new DeleteBookCommand { ISBN = "11111111111" };
            Book book = new Book { BookId = 1, ISBN = "11111111111", Authors = "author", Title = "title", Description = "description", CoverImg = "coverImg" };
            _bookRepositoryMock.Setup(x => x.Delete(It.IsAny<string>()));
            _bookRepositoryMock.Setup(x => x.GetByISBN(It.IsAny<string>())).ReturnsAsync(book);
            DeleteBookHandler handler = new DeleteBookHandler(_bookRepositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _bookRepositoryMock.Verify(x => x.Delete(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async void DeleteBookHandler_Calls_BooksRepository_GetByISBN()
        {
            // Arrange
            DeleteBookCommand request = new DeleteBookCommand { ISBN = "11111111111" };
            Book book = new Book { BookId = 1, ISBN = "11111111111", Authors = "author", Title = "title", Description = "description", CoverImg = "coverImg" };
            _bookRepositoryMock.Setup(x => x.Delete(It.IsAny<string>()));
            _bookRepositoryMock.Setup(x => x.GetByISBN(It.IsAny<string>())).ReturnsAsync(book);
            DeleteBookHandler handler = new DeleteBookHandler(_bookRepositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _bookRepositoryMock.Verify(x => x.GetByISBN(It.IsAny<string>()), Times.Once());
        }
    }
}