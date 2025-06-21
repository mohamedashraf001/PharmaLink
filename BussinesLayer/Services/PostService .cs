using BusinessLayer.Interfaces;
using DataLayer.Entities;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repo;

        public PostService(IPostRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Post> CreateAsync(Post post)
        {
            await _repo.AddAsync(post);
            await _repo.SaveChangesAsync();
            return post;
        }

        public async Task<Post?> UpdateAsync(int id, Post updated)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Title = updated.Title;
            existing.Description = updated.Description;
            existing.ExpiryDate = updated.ExpiryDate;
            existing.Quantity = updated.Quantity;

            _repo.Update(existing);
            await _repo.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var post = await _repo.GetByIdAsync(id);
            if (post == null) return false;

            _repo.Delete(post);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
