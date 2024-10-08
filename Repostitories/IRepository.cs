using System.Linq.Expressions;

namespace SkylerO_AmplifundProj.Repostitories
{

    public interface IRepository<T> : IDisposable where T : class
    {
        List<T> GetAll();
        T GetOne(int id);

        void Add(T entity);
        void Update(int id, T entity);
        void Delete(T entity);

    }
}