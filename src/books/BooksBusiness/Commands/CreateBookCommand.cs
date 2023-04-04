using Books.Business.Responses;
using Books.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Books.Business.Commands
{
    public class CreateBookCommand : IRequest<Book>
    {
        public string ISBN { get; set; }
        public string Authors { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImg { get; set; }
    }
}
