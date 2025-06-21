using DataLayer.Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public int Quantity { get; set; }

    public int PharmacyId { get; set; }
    public Pharmacy Pharmacy { get; set; }

    public ICollection<Request> Requests { get; set; }

    // 👇 نوع العرض: بيع أو تبديل
    public OfferType OfferType { get; set; }

    // 👇 في حالة البيع: النسبة (دايمًا 15%)
    public decimal? ProfitPercentage { get; set; }

    // 👇 في حالة التبديل: اسم الدواء المطلوب وفرق السعر لو فيه
    public string? ExchangeWith { get; set; }
    public decimal? PriceDifference { get; set; }
    public string? Plusorminus { get; set; }
}
