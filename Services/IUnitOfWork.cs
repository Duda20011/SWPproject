

using Services.Repository;
using Services.Repository.Interface;

namespace Services
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangeAsync();
        public IUserRepo userRepo { get; }
        public ICourseRepo courseRepo { get; }
        public IPostRepo postRepo { get; }
        public IMaterialRepo materialRepo { get; }


    }
}
