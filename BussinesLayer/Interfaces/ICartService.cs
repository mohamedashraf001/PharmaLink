using DataLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartItem>> GetAllAsync();
        Task<CartItem?> GetByIdAsync(int id);
        Task<CartItem> CreateAsync(CartItem item);
        Task<bool> DeleteAsync(int id);
        Task<CartItem> AddPostToCartAsync(int pharmacyId, int postId, int quantity);
        Task<bool> RemoveFromCartAsync(int pharmacyId, int postId);
        Task<IEnumerable<CartItem>> GetCartByPharmacyIdAsync(int pharmacyId);

    }
}
