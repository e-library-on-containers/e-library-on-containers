using Books.Business.Handlers;
using Books.Business.Queries;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using Moq;
using Xunit;

namespace Books.Business.Handlers.Tests
{
    public class BookInstancesQueriesHandlersTests
    {
        private readonly Mock<IBookInstancesRepository<BookInstance>> _bookInstanceRepositoryMock;

        public BookInstancesQueriesHandlersTests() 
        {
            _bookInstanceRepositoryMock = new Mock<IBookInstancesRepository<BookInstance>>();
        }

        [Fact]
        public async void GetBookInstancesByISBNHandler_Calls_BookInstancesRepository_GetByISBN()
        {
            // Arrange
            GetBookInstancesByISBNQuery request = new GetBookInstancesByISBNQuery { ISBN = "11111111111", isAvailable = true };
            List<BookInstance> bookInstances = new List<BookInstance> {
                new BookInstance(),
                new BookInstance()
            };
            _bookInstanceRepositoryMock.Setup(x => x.GetByISBN(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(bookInstances);
            GetBookInstancesByISBNHandler handler = new GetBookInstancesByISBNHandler(_bookInstanceRepositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _bookInstanceRepositoryMock.Verify(x => x.GetByISBN(It.IsAny<string>(), It.IsAny<bool>()), Times.Once());
        }

        [Fact]
        public async void GetBookInstanceByIdHandler_Calls_BookInstancesRepository_GetById()
        {
            // Arrange
            GetBookInstanceByIdQuery request = new GetBookInstanceByIdQuery{ Id = 1 };
            BookInstance bookInstance = new BookInstance();
            _bookInstanceRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(bookInstance);
            GetBookInstanceByIdHandler handler = new GetBookInstanceByIdHandler(_bookInstanceRepositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _bookInstanceRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void GetAllBookInstancesHandler_Calls_BookInstancesRepository_GetAll()
        {
            // Arrange
            GetAllBookInstancesQuery request = new GetAllBookInstancesQuery();
            List<BookInstance> bookInstances = new List<BookInstance> {
                new BookInstance(),
                new BookInstance()
            };
            _bookInstanceRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(bookInstances);
            GetAllBookInstancesHandler handler = new GetAllBookInstancesHandler(_bookInstanceRepositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _bookInstanceRepositoryMock.Verify(x => x.GetAll(), Times.Once());
        }
    }
}
