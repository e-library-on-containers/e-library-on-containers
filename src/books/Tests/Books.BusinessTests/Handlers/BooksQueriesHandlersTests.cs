using Xunit;
using Moq;
using Books.Infrastructure.Models;
using Books.Infrastructure.Contracts;
using Books.Business.Handlers;
using Books.Business.Queries;

namespace Books.Business.Handlers.Tests
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

        [Fact]
        public async void GetBookByISBNHandler_Calls_BooksReadRepository_GetByISBN()
        {
            // Arrange
            GetBookByISBNQuery request = new GetBookByISBNQuery { ISBN = "11111111111" };
            BookRead book = new BookRead { BookId = 1, ISBN = "11111111111", Authors = "author", Title = "title", Description = "description", CoverImg = "coverImg", CopiesCount = 1 };
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
            _bookReadRepositoryMock.Verify(x => x.GetByISBN(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async void GetBookByISBNHandler_Calls_BookInstancesRepository_GetByISBN()
        {
            // Arrange
            GetBookByISBNQuery request = new GetBookByISBNQuery { ISBN = "11111111111" };
            BookRead book = new BookRead { BookId = 1, ISBN = "11111111111", Authors = "author", Title = "title", Description = "description", CoverImg = "coverImg", CopiesCount = 1 };
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
            _bookInstanceRepositoryMock.Verify(x => x.GetByISBN(It.IsAny<string>(), It.IsAny<bool>()), Times.Once());
        }

        [Fact]
        public async void GetAllBooksHandler_Calls_BooksReadRepository_GetAll()
        {
            // Arrange
            GetAllBooksQuery request = new GetAllBooksQuery();
            List<BookRead> books = new List<BookRead> { 
                new BookRead{ BookId = 1, ISBN = "11111111111", Authors = "author", Title = "title", Description = "description", CoverImg = "coverImg", CopiesCount = 1 }
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
            _bookReadRepositoryMock.Verify(x => x.GetAll(), Times.Once());
        }

        [Fact]
        public async void GetAllBooksHandler_Calls_BookInstancesRepository_GetByISBN()
        {
            // Arrange
            GetAllBooksQuery request = new GetAllBooksQuery();
            List<BookRead> books = new List<BookRead> {
                new BookRead{ BookId = 1, ISBN = "11111111111", Authors = "author", Title = "title", Description = "description", CoverImg = "coverImg", CopiesCount = 1 }
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
            _bookInstanceRepositoryMock.Verify(x => x.GetByISBN(It.IsAny<string>(), It.IsAny<bool>()), Times.Once());
        }
    }
}
