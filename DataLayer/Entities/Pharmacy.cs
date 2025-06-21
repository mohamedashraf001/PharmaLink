using DataLayer.Entities;

public class Pharmacy
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

    public PharmacyStatus Status { get; set; } = PharmacyStatus.Pending;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime? ApprovedUntil { get; set; }

    public ICollection<Post> Posts { get; set; }
}
