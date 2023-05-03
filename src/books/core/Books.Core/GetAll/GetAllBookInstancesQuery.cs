using Books.Core.Responses;
using MediatR;

namespace Books.Core.GetAll
{
    public class GetAllBookInstancesQuery : IRequest<List<BookInstanceResponse>>
    {

    }
}
