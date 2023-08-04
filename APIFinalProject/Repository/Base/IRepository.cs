using System.Linq.Expressions;

namespace APIFinalProject.Repository.Base
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetOne(int id);
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> GetAll(params string[] args);
        void Add(T item);

        void Update(T item);

        void Delete(int id);

        Task<IEnumerable<T>> SelectGroup(Expression<Func<T,bool>>match);


    }
}
