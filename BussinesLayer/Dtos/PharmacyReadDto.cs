using DataLayer.Entities;

namespace PharmaLink.DTOs
{
    public class PharmacyReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string DoctorNumber { get; set; }
        public PharmacyStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; }

        public DateTime? ApprovedUntil { get; set; }
    }
}
