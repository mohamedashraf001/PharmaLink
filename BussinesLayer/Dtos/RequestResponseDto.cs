using PharmaLink.DTOs.Pharmacy;
using PharmaLink.DTOs.Post;

namespace PharmaLink.DTOs.Request
{
    public class RequestResponseDto
    {
        public int Id { get; set; }
        public PostResponseDto Post { get; set; }
        public PharmacyResponseDto Pharmacy { get; set; }
        public string Status { get; set; }
        public string RequestDate { get; set; }
        public int Quantity { get; set; }
    }
}
 