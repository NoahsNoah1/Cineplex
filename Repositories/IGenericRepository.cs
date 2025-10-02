using CinePlex.models;

namespace CinePlex.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Result<T> Add(T entity);
        Result<List<T>> GetAll();
        Result<T> GetById(int id);
        Result<T> Update(T entity);
        Result<T> Delete(int id);
        
    }
}