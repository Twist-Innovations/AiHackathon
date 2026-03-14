using AiHackathon.ApiService.Interfaces;
using AiHackathon.ApiService.Models;

namespace AiHackathon.ApiService.DataAccess
{
    public class FarmProductRepository: IFarmProductsRepository
    {
        public FarmProductRepository()
        {

        }

        public Task<bool> AddAsync(FarmProduct item)
        {
            return Task.FromResult(true);
        }

        public Task<bool> AnyAsync(string id)
        {
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(params string[] Ids)
        {
            return Task.FromResult(true);
        }

        public Task<FarmProduct?> GetAsync(string id)
        {
            return null;
        }

        public Task<List<FarmProduct>> GetAsync(bool forceRefresh = false)
        {
            return Task.FromResult(new List<FarmProduct>());
        }

        public Task<bool> SaveAsync()
        {
            return Task.FromResult(true);
        }

        public Task<List<FarmProduct>> SearchAsync(string search)
        {
            return Task.FromResult(new List<FarmProduct>());
        }

        public Task<bool> UpdateAsync(FarmProduct item)
        {
            return Task.FromResult(true);
        }
    }
}
