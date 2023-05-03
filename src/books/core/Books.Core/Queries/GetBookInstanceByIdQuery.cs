using Books.Core.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Queries
{
    public class GetBookInstanceByIdQuery : IRequest<BookInstanceResponse>
    {
        public int Id { get; set; }
    }
}
