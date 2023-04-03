using Books.Business.Responses;
using Books.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Books.Business.Commands
{
    public class CreateBookInstanceCommand : IRequest<string>
    {
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }

        public CreateBookInstanceCommand(string iSBN, bool isAvailable)
        {
            ISBN = iSBN;
            IsAvailable = isAvailable;
        }
    }
}
