using GiayDep.Models;

namespace GiayDep.Areas.Admin.InterfacesRepositories
{
    public interface ICategoryRepositorycs
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task CreateAsync(Category Category);
        Task UpdateAsync(Category Category);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
