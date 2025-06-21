using DataLayer.Entities;
using PharmaLink.DTOs.Pharmacy;
using PharmaLink.DTOs.Request;

namespace PharmaLink.DTOs.Post
{
    public class PostResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExpiryDate { get; set; }
        public int Quantity { get; set; }

        public PharmacyResponseDto Pharmacy { get; set; }

        // معلومات العرض
        public OfferType OfferType { get; set; }
        public decimal? ProfitPercentage { get; set; }
        public string? ExchangeWith { get; set; }
        public decimal? PriceDifference { get; set; }
        public string? Plusorminus { get; set; }

    }
}
