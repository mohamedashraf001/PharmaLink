using DataLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(int id);
        Task<Post> CreateAsync(Post post);
        Task<Post?> UpdateAsync(int id, Post updated);
        Task<bool> DeleteAsync(int id);
    }
}
