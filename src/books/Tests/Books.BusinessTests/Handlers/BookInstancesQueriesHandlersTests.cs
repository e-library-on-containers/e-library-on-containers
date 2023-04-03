using Books.Business.Handlers;
using Books.Business.Queries;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using Moq;
using Xunit;

namespace Books.BusinessTests.Handlers
{
    public class BookInstancesQueriesHandlersTests
    {
        private readonly Mock<IBookInstancesRepository<BookInstance>> _bookInstanceRepositoryMock;

        public BookInstancesQueriesHandlersTests() 
        {
            _bookInstanceRepositoryMock = new Mock<IBookInstancesRepository<BookInstance>>();
        }

        [Fact()]
        public async void GetBookInstancesByISBNHandlerTest()
        {
            // Arrange
            GetBookInstancesByISBNQuery request = new GetBookInstancesByISBNQuery("11111111111", true);
            List<BookInstance> bookInstances = new List<BookInstance> {
                new BookInstance(),
                new BookInstance()
            };
            _bookInstanceRepositoryMock.Setup(x => x.GetByISBN(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(bookInstances);
            GetBookInstancesByISBNHandler handler = new GetBookInstancesByISBNHandler(_bookInstanceRepositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _bookInstanceRepositoryMock.VerifyAll();
        }

        [Fact()]
        public async void GetBookInstanceByIdHandlerTest()
        {
            // Arrange
            GetBookInstanceByIdQuery request = new GetBookInstanceByIdQuery(1);
            BookInstance bookInstance = new BookInstance();
            _bookInstanceRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(bookInstance);
            GetBookInstanceByIdHandler handler = new GetBookInstanceByIdHandler(_bookInstanceRepositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _bookInstanceRepositoryMock.VerifyAll();
        }

        [Fact()]
        public async void GetAllBookInstancesHandlerTest()
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
            _bookInstanceRepositoryMock.VerifyAll();
        }
    }
}
