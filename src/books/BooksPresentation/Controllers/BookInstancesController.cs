using Books.Business.Commands;
using Books.Business.Queries;
using Books.Business.RabitMQ;
using Books.Business.Responses;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookInstancesController : ControllerBase
    {
        private readonly IBookRepository<Book> booksRepository;
        private readonly IBookRepository<BookRead> booksReadRepository;
        private readonly IMediator _mediator;
        private readonly IRabitMQProducer _rabitMQProducer;

        public BookInstancesController(IBookRepository<Book> booksRepository, IBookRepository<BookRead> booksReadRepository, IMediator mediator, IRabitMQProducer rabitMQProducer)
        {
            this.booksRepository = booksRepository;
            this.booksReadRepository = booksReadRepository;
            _mediator = mediator;
            _rabitMQProducer = rabitMQProducer;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByISBN(string ISBN=null, bool available = false)
        {
            try
            {
                if (ISBN == null)
                {
                    return Ok(await _mediator.Send(new GetAllBookInstancesQuery()));
                }
                else
                {
                    return Ok(await _mediator.Send(new GetBookInstancesByISBNQuery(ISBN)));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _mediator.Send(new GetBookInstanceByIdQuery(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] CreateBookInstanceCommand createBookInstanceCommand)
        {
            try
            {
                var result = await _mediator.Send(createBookInstanceCommand);
                _rabitMQProducer.SendBookInstanceAddedMessage(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// This endpont deletes a book form the database by ISBN
        /// </summary>
        /// <param name="ISBN"></param>
        /// <returns>Status for deletion</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                string ISBN = await _mediator.Send(new DeleteBookInstanceCommand(id));
                _rabitMQProducer.SendBookInstanceDeletedMessage(ISBN);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// This endpoint updates a book by ISBN
        /// </summary>
        /// <param name="bookInstance"></param>
        /// <returns>Status for update</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UpdateBookInstanceCommand updateBookInstanceCommand)
        {
            var result = await _mediator.Send(updateBookInstanceCommand);
            return Ok(result);
        }
    }
}
