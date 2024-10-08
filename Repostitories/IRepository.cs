using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace SkylerO_AmplifundProj.Repostitories
{

    public interface IRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetOne(int id);

        Task<T> Add(T entity);
        Task Update(int id, T entity);
        Task Delete(T entity);

    }
}