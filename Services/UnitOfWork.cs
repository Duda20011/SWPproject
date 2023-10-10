
namespace Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;
       
        public UnitOfWork(AppDBContext context
           )
        {
            _context = context;
           
        }

        public Task<int> SaveChangeAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
