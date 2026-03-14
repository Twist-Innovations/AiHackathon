namespace AiHackathon.ApiService.Interfaces
{
    public interface IRepositories<T>
    {
        Task<bool> AddAsync(T item);
        Task<bool> DeleteAsync(params string[] Ids);
        Task<T?> GetAsync(string id);
        Task<bool> AnyAsync(string id);
        Task<List<T>> GetAsync(bool forceRefresh = false);
        Task<List<T>> SearchAsync(string search);
        Task<bool> UpdateAsync(T item);
        Task<bool> SaveAsync();
    }
}
