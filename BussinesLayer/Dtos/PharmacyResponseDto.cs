using DataLayer.Entities;

namespace PharmaLink.DTOs.Pharmacy
{
    public class PharmacyResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string LicenseNumber { get; set; }
        public string City { get; set; }
        public string WorkingTime { get; set; }
        public string DoctorEmail { get; set; }
        public string DoctorNumber { get; set; }

    }
}
