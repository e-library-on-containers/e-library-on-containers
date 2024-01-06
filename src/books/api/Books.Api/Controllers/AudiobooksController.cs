using Books.Core.Audiobooks.Create;
using Books.Core.Audiobooks.Get;
using Books.Core.Publish;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AudiobooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public AudiobooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllAudiobooksQuery()));
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int id)
    {
        return Ok(await _mediator.Send(new GetAudiobookByIdQuery
        {
            Id = id
        }));
    }
    
    [HttpPost("{id}/publish")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Publish(int id)
    {
        return Ok(await _mediator.Send(new PublishAudiobookCommand
        {
            Id = id
        }));
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Publish(CreateAudiobookCommand request)
    {
        return StatusCode(201, await _mediator.Send(request));
    }
    
    
}