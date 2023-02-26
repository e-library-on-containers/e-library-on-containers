using Books.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Business.Commands
{
    public class UpdateBookInstanceCommand : IRequest<BookInstance>
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }
    }
}
