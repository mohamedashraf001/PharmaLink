namespace PharmaLink.DTOs.Cart
{
    public class CartItemCreateDto
    {
        public int PostId { get; set; }
        public int PharmacyId { get; set; }
        public int Quantity { get; set; }
    }
}
