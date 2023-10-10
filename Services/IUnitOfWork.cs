

namespace Services
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangeAsync();

    }
}
