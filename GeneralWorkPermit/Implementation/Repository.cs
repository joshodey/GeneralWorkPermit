using GeneralWorkPermit.Context;
using GeneralWorkPermit.Services;

namespace GeneralWorkPermit.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _context;
        public Repository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<T> Get(string Id)
        {
           return await _context.Set<T>().FindAsync(Id);
        }


        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public async Task<T> AddAsync(T item)
        {
            var data = await _context.AddAsync<T>(item);
            await SaveChangesAsync();
            return data.Entity;
        }

        public async Task<T> UpdateAsync(T item)
        {
            var data = _context.Update<T>(item);
            await SaveChangesAsync(); 
            return data.Entity;
        }


        public async Task DeleteAsync(T item)
        {
            _context.Remove<T>(item);
            await SaveChangesAsync();
        }



        #region: Helper

        private async Task<int> SaveChangesAsync()
        {
            return _context.SaveChanges();
        }

        
        #endregion
    }
}
