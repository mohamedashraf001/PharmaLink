using BusinessLayer.Interfaces;
using DataLayer.Entities;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repo;
        private readonly IPostRepository _postRepository;
        public CartService(ICartRepository repo, IPostRepository postRepository)
        {
            _repo = repo;
            _postRepository = postRepository;

        }

        public async Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<CartItem?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<CartItem> CreateAsync(CartItem item)
        {
            await _repo.AddAsync(item);
            await _repo.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            _repo.Delete(existing);
            await _repo.SaveChangesAsync();
            return true;
        }
        public async Task<CartItem?> AddPostToCartAsync(int pharmacyId, int postId, int quantity)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null || quantity > post.Quantity)
                return null;

            // Check if the item already exists in the cart
            var existingItem = await _repo.GetByPharmacyAndPostAsync(pharmacyId, postId);
            if (existingItem != null)
            {
                var newTotalQuantity = existingItem.Quantity + quantity;
                if (newTotalQuantity > post.Quantity)
                    return null;

                existingItem.Quantity = newTotalQuantity;
                _repo.Update(existingItem);
                await _repo.SaveChangesAsync();
                return existingItem;
            }

            // Else: create new cart item
            var item = new CartItem
            {
                PharmacyId = pharmacyId,
                PostId = postId,
                Quantity = quantity
            };

            await _repo.AddAsync(item);
            await _repo.SaveChangesAsync();
            return item;
        }

        public async Task<bool> RemoveFromCartAsync(int pharmacyId, int postId)
        {
            var item = await _repo.GetCartItemAsync(pharmacyId, postId);
            if (item == null)
                return false;

            _repo.Delete(item);
            await _repo.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<CartItem>> GetCartByPharmacyIdAsync(int pharmacyId)
        {
            return await _repo.GetCartByPharmacyIdAsync(pharmacyId);
        }

    }
}
