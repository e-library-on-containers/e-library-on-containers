﻿using Books.Core.Books;
using Books.Core.Create;
using Books.Core.Delete;
using Books.Core.GetAll;
using Books.Core.GetByISBN;
using Books.Core.RabitMQ;
using Books.Core.Update;
using Books.Infrastructure.Contracts;
using Books.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository<Book> booksRepository;
        private readonly IBookRepository<BookRead> booksReadRepository;
        private readonly IMediator _mediator;
        private readonly IRabitMQProducer _rabitMQProducer;
        public BooksController(IBookRepository<Book> booksRepository, IBookRepository<BookRead> booksReadRepository, IMediator mediator, IRabitMQProducer rabitMQProducer) { 
            this.booksRepository = booksRepository;
            this.booksReadRepository = booksReadRepository;
            _mediator = mediator;
            _rabitMQProducer = rabitMQProducer;
        }
        
        [HttpGet("get-preview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPreview()
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllBooksQuery
                {
                    IncludeInPreview = true
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{ISBN}/publish")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Publish(string ISBN)
        {
            try
            {
                return Ok(await _mediator.Send(new PublishBookCommand
                {
                    ISBN = ISBN
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllBooksQuery()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{ISBN}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByISBN(string ISBN)
        {
            try
            {
                return Ok(await _mediator.Send(new GetBookByISBNQuery { ISBN = ISBN }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] CreateBookCommand createBookCommand)
        {
            try
            {
                var result = await _mediator.Send(createBookCommand);
                _rabitMQProducer.SendBookAddedMessage(new BookRead(result));
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
        [HttpDelete("{ISBN}")]
        public async Task<IActionResult> Delete(string ISBN)
        {
            try
            {
                int id = await _mediator.Send(new DeleteBookCommand { ISBN = ISBN });
                _rabitMQProducer.SendBookDeletedMessage(ISBN);
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
        /// <param name="book"></param>
        /// <returns>Status for update</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UpdateBookCommand updateBookCommand)
        {
            try
            {
                var result = await _mediator.Send(updateBookCommand);
                _rabitMQProducer.SendBookUpdatedMessage(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
