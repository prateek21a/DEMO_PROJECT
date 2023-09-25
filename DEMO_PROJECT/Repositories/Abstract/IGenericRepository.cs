using System.Linq.Expressions;

namespace DEMO_PROJECT.Repositories.Abstract
{
    public interface IBaseRepository<T>
    {
        //used to retrieve all entities of type T from the data storage.
        IQueryable<T> GetAll();
        // allows you to pass in a filter expression as a parameter.
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
        //used to find an entity of type T by its unique identifier 
        T? FindById(int id);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
