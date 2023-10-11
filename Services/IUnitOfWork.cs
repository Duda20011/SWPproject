

using Services.Repository;

namespace Services
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangeAsync();
        public IUserRepo userRepo { get; }

    }
}
