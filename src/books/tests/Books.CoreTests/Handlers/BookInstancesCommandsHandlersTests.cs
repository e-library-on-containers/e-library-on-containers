using Xunit;
using Moq;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using Books.Core.Create;
using Books.Core.Delete;

namespace Books.Core.Handlers.Tests
{
    public class BookInstancesCommandsHandlersTests
    {
        private readonly Mock<IBookInstancesRepository<BookInstance>> _bookInstanceRepositoryMock;

        public BookInstancesCommandsHandlersTests()
        {
            _bookInstanceRepositoryMock = new Mock<IBookInstancesRepository<BookInstance>>();
        }

        [Fact]
        public async void CreateBookInstanceHandler_Calls_BookInstancesRepository_Create()
        {
            // Arrange
            CreateBookInstanceCommand request = new CreateBookInstanceCommand { ISBN = "11111111111", IsAvailable = true };
            _bookInstanceRepositoryMock.Setup(x => x.Create(It.IsAny<BookInstance>()));
            CreateBookInstanceHandler handler = new CreateBookInstanceHandler(_bookInstanceRepositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _bookInstanceRepositoryMock.Verify(x => x.Create(It.IsAny<BookInstance>()), Times.Once());
        }

        [Fact]
        public async void DeleteBookInstanceHandler_Calls_BookInstancesRepository_Delete()
        {
            // Arrange
            DeleteBookInstanceCommand request = new DeleteBookInstanceCommand { Id = 1 };
            Book book = new Book { BookId = 1, ISBN = "11111111111", Authors = "author", Title = "title", Description = "description", CoverImg = "coverImg"};
            BookInstance bookInstance = new BookInstance();
            _bookInstanceRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()));
            _bookInstanceRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(bookInstance);
            DeleteBookInstanceHandler handler = new DeleteBookInstanceHandler(_bookInstanceRepositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _bookInstanceRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void DeleteBookInstanceHandler_Calls_BookInstancesRepository_GetById()
        {
            // Arrange
            DeleteBookInstanceCommand request = new DeleteBookInstanceCommand { Id = 1 };
            Book book = new Book { BookId = 1, ISBN = "11111111111", Authors = "author", Title = "title", Description = "description", CoverImg = "coverImg" };
            BookInstance bookInstance = new BookInstance();
            _bookInstanceRepositoryMock.Setup(x => x.Delete(It.IsAny<int>()));
            _bookInstanceRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(bookInstance);
            DeleteBookInstanceHandler handler = new DeleteBookInstanceHandler(_bookInstanceRepositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _bookInstanceRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
        }
    }
}
