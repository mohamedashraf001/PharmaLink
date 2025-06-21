using DataLayer.Entities;

namespace PharmaLink.DTOs
{
    public class PharmacyUpdateStatusDto
    {
        public PharmacyStatus Status { get; set; }
        public DateTime? ApprovedUntil { get; set; }
    }
}
