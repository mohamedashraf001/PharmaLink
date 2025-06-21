using DataLayer.Entities;

namespace PharmaLink.DTOs.Post
{
    public class PostCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public int PharmacyId { get; set; }

        // نوع العرض: بيع أو تبادل
        public OfferType OfferType { get; set; }

        // لو بيع بنسبة
        public decimal? ProfitPercentage { get; set; }

        // لو تبادل
        public string? ExchangeWith { get; set; }
        public decimal? PriceDifference { get; set; }
        public string? Plusorminus { get; set; }

    }
}
