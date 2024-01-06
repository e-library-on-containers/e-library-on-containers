namespace Books.Infrastructure.Contracts;

public interface IRepository<T>
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T> GetById(int id);
    public Task<int> Add(T person);
    public Task Update(T person);
    public Task Delete(int id);
}