using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Contracts
{
    public interface IBookRepository <T>
    {
        Task<int> Create(T _object);
        Task<int> Delete(string ISBN);
        Task<int> Update(T _object);
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> GetByISBN(string isbn);
    }
}
