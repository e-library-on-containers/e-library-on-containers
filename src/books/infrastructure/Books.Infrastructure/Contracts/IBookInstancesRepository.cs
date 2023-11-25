﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Contracts
{
    public interface IBookInstancesRepository <T>
    {
        Task<int> Create(T _object);
        Task<int> Delete(string ISBN);
        Task<int> Delete(int id);
        Task<int> Update(T _object);
        Task<List<T>> GetAll(bool includePreview = false);
        Task<T> GetById(int id);
        Task<List<T>> GetByISBN(string isbn, bool isAvailable);
    }
}
