using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Commands
{
    public class DeleteBookCommand : IRequest<int>
    {
        public string ISBN { get; private set; }

        public DeleteBookCommand(string ISBN)
        {
            this.ISBN = ISBN;
        }
    }
}
