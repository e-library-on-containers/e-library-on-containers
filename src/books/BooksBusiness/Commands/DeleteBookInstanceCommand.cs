﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Business.Commands
{
    public class DeleteBookInstanceCommand : IRequest<string>
    {
        public int Id { get; private set; }

        public DeleteBookInstanceCommand(int id)
        {
            Id = id;
        }
    }
}
