using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Commands
{
    public class DeleteBookInstanceCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}
