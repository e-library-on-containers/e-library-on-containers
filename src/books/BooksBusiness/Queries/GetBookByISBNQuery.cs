﻿using Books.Business.Responses;
using Books.Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Business.Queries
{
    public class GetBookByISBNQuery : IRequest<BookResponse>
    {
        public string ISBN { get; set; }
    }
}
