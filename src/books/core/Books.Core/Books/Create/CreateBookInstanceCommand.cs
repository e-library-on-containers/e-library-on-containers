using MediatR;

namespace Books.Core.Create
{
    public class CreateBookInstanceCommand : IRequest<string>
    {
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }
    }
}
