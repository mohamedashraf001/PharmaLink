using AutoMapper;
using BusinessLayer;
using BusinessLayer.Interfaces;
using DataLayer.Entities;
using DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PharmaLink.DTOs.Cart;

namespace PharmaLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartService _service;
        private readonly IMapper _mapper;

        public CartController(
            IPostRepository postRepository,
            IPharmacyRepository pharmacyRepository,
            ICartRepository cartRepository,
            ICartService service, IMapper mapper)
        {
            _postRepository = postRepository;
            _pharmacyRepository = pharmacyRepository;
            _cartRepository = cartRepository;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemResponseDto>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CartItemResponseDto>>(items));
        }

        [HttpPost]
        public async Task<ActionResult<CartItemResponseDto>> Create(CartItemCreateDto dto)
        {
            var item = _mapper.Map<CartItem>(dto);
            var result = await _service.CreateAsync(item);
            return Ok(_mapper.Map<CartItemResponseDto>(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart(CartItemCreateDto dto)
        {
            var post = await _postRepository.GetByIdAsync(dto.PostId);
            if (post == null)
                return NotFound("Post not found");

            var pharmacy = await _pharmacyRepository.GetByIdAsync(dto.PharmacyId);
            if (pharmacy == null)
                return NotFound("Pharmacy not found");

            if (dto.Quantity <= 0)
                return BadRequest("Quantity must be greater than zero");

            if (dto.Quantity > post.Quantity)
                return BadRequest("Requested quantity exceeds available quantity");

            var existingItem = await _cartRepository.GetByPharmacyAndPostAsync(dto.PharmacyId, dto.PostId);
            if (existingItem != null)
                return BadRequest("This post is already in the cart");

            var cartItem = new CartItem
            {
                PostId = dto.PostId,
                PharmacyId = dto.PharmacyId,
                Quantity = dto.Quantity
            };

            await _cartRepository.AddAsync(cartItem);
            await _cartRepository.SaveChangesAsync();

            return Ok("Item added to cart");
        }
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart(int pharmacyId, int postId)
        {
            var success = await _service.RemoveFromCartAsync(pharmacyId, postId);
            if (!success)
                return NotFound("Item not found in cart.");

            return Ok("Item removed from cart successfully.");
        }

        [HttpGet("pharmacy/{pharmacyId}")]
        public async Task<ActionResult<IEnumerable<CartItemResponseDto>>> GetCartByPharmacyId(int pharmacyId)
        {
            var cartItems = await _service.GetCartByPharmacyIdAsync(pharmacyId);
            var result = _mapper.Map<IEnumerable<CartItemResponseDto>>(cartItems);
            return Ok(result);
        }

    }
}
