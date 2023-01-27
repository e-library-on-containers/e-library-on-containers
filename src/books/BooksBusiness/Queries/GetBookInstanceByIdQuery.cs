﻿using Books.Business.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Business.Queries
{
    public class GetBookInstanceByIdQuery : IRequest<BookInstanceResponse>
    {
        public int Id { get; private set; }
        public GetBookInstanceByIdQuery(int Id)
        {
            Id = Id;
        }
    }
}