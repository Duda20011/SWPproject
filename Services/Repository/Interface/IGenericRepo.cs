using Microsoft.EntityFrameworkCore.Query;
using Services.Commons;
using Services.Entity;
using Services.Enum;
using System.Linq.Expressions;

namespace Services.Repository
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entities);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
        Task<T> GetEntityByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
