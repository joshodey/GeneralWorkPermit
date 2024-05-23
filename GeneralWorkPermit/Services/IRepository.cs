namespace GeneralWorkPermit.Services
{
    public interface IRepository<T> where T : class
    {
        Task<T> UpdateAsync(T item);
        Task<T> AddAsync(T item);
        Task DeleteAsync(T item);
        IQueryable<T> GetAll();
        Task<T> Get(string Id);
    }
}
