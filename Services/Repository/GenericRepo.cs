using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using Services.Commons;
using Services.Entity;
using Services.Enum;
using Services.Service;
using Services.Service.Interface;
using System;
using System.Linq.Expressions;

namespace Services.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _dbSet;
        private readonly ICurrentTimeService _currentTime;
        private readonly IClaimsServices _claimsServices;

        public GenericRepo(AppDBContext context, ICurrentTimeService currentTime, IClaimsServices claimsServices)
        {
            _dbSet = context.Set<T>();
            _currentTime = currentTime;
            _claimsServices = claimsServices;
        }
        public async Task CreateAsync(T entity)
        {
            entity.CreationDate = _currentTime.GetCurrentTime();
            var id = _claimsServices.GetCurrentUser();
            entity.CreatedBy = !string.IsNullOrEmpty(id) ? int.Parse(id) : null;
            await _dbSet.AddAsync(entity);
        }
        public async Task CreateRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreationDate = _currentTime.GetCurrentTime();
                var id = _claimsServices.GetCurrentUser();
                entity.CreatedBy = !string.IsNullOrEmpty(id) ? int.Parse(id) : null;
            }
            await _dbSet.AddRangeAsync(entities);
        }
        public void UpdateAsync(T entity)
        {
            entity.ModificationDate = _currentTime.GetCurrentTime();
            var id = _claimsServices.GetCurrentUser();
            entity.ModificationBy = int.Parse(id);
            _dbSet.Update(entity);
        }
        public void DeleteAsync(T entity)
        {
            entity.DeletionDate = _currentTime.GetCurrentTime();
            var id = _claimsServices.GetCurrentUser();
            entity.DeleteBy = int.Parse(id);
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();
        public async Task<T> GetEntityByIdAsync(int id) => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }
}