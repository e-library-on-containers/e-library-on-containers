using Xunit;
using Moq;
using Books.Business.Commands;
using Books.Infrastructure.Models;
using Books.Infrastructure.Contracts;
using Books.Business.Handlers;
using Books.Business.Queries;
using NSubstitute;

namespace Books.BusinessTests.Handlers
{
    public class BooksQueriesHandlersTests
    {
        private readonly Mock<IBookRepository<BookRead>> _bookReadRepositoryMock;
        private readonly Mock<IBookInstancesRepository<BookInstance>> _bookInstanceRepositoryMock;

        public BooksQueriesHandlersTests()
        {
            _bookReadRepositoryMock = new Mock<IBookRepository<BookRead>>();
            _bookInstanceRepositoryMock = new Mock<IBookInstancesRepository<BookInstance>>();
        }

        [Fact()]
        public async void GetBookByISBNHandlerTest()
        {
            // Arrange
            GetBookByISBNQuery request = new GetBookByISBNQuery("11111111111");
            BookRead book = new BookRead(1, "11111111111", "author", "title", "description", "coverImg", 1);
            List<BookInstance> bookInstances = new List<BookInstance> { 
                new BookInstance(),
                new BookInstance()
            };
            _bookReadRepositoryMock.Setup(x => x.GetByISBN(It.IsAny<string>())).ReturnsAsync(book);
            _bookInstanceRepositoryMock.Setup(x => x.GetByISBN(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(bookInstances);
            GetBookByISBNHandler handler = new GetBookByISBNHandler(_bookReadRepositoryMock.Object, _bookInstanceRepositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _bookReadRepositoryMock.VerifyAll();
            _bookInstanceRepositoryMock.VerifyAll();
        }

        [Fact()]
        public async void GetAllBooksHandlerTest()
        {
            // Arrange
            GetAllBooksQuery request = new GetAllBooksQuery();
            List<BookRead> books = new List<BookRead> { 
                new BookRead(1, "11111111111", "author", "title", "description", "coverImg", 1) 
            };
            List<BookInstance> bookInstances = new List<BookInstance> {
                new BookInstance(),
                new BookInstance()
            };
            _bookReadRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(books);
            _bookInstanceRepositoryMock.Setup(x => x.GetByISBN(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(bookInstances);
            GetAllBooksHandler handler = new GetAllBooksHandler(_bookReadRepositoryMock.Object, _bookInstanceRepositoryMock.Object);

            // Act
            await handler.Handle(request, default);

            // Assert
            _bookReadRepositoryMock.VerifyAll();
            _bookInstanceRepositoryMock.VerifyAll();
        }
    }
}
