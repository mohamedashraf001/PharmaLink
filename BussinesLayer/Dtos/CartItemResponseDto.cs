using PharmaLink.DTOs.Pharmacy;
using PharmaLink.DTOs.Post;

namespace PharmaLink.DTOs.Cart
{
    public class CartItemResponseDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public PostResponseDto Post { get; set; }
        public PharmacyResponseDto Pharmacy { get; set; }
    }
}
