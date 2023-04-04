﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Business.Commands
{
    public class DeleteBookCommand : IRequest<int>
    {
        public string ISBN { get; set; }
    }
}
