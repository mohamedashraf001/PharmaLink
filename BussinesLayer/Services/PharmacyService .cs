using BusinessLayer.Interfaces;
using DataLayer.Entities;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer
{
    public class PharmacyService : IPharmacyService
    {
        private readonly IPharmacyRepository _repo;

        public PharmacyService(IPharmacyRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Pharmacy>> GetAllAsync()
        {
            var pharmacies = await _repo.GetAllAsync();

            foreach (var pharmacy in pharmacies)
            {
                if (pharmacy.Status == PharmacyStatus.Approved &&
                    pharmacy.ApprovedUntil.HasValue &&
                    pharmacy.ApprovedUntil.Value < DateTime.UtcNow)
                {
                    pharmacy.Status = PharmacyStatus.Pending;
                }
            }

            await _repo.SaveChangesAsync();
            return pharmacies;
        }

        public async Task<Pharmacy?> GetByIdAsync(int id)
        {
            var pharmacy = await _repo.GetByIdAsync(id);
            if (pharmacy == null)
                return null;

            if (pharmacy.Status == PharmacyStatus.Approved &&
                pharmacy.ApprovedUntil.HasValue &&
                pharmacy.ApprovedUntil.Value < DateTime.UtcNow)
            {
                pharmacy.Status = PharmacyStatus.Pending;
                await _repo.SaveChangesAsync();
            }

            return pharmacy;
        }

        public async Task<Pharmacy> CreateAsync(Pharmacy pharmacy)
        {
            if (pharmacy.Status == PharmacyStatus.Approved)
            {
                pharmacy.ApprovedUntil = DateTime.UtcNow.AddMonths(1);
            }

            await _repo.AddAsync(pharmacy);
            await _repo.SaveChangesAsync();
            return pharmacy;
        }

        public async Task<Pharmacy?> UpdateAsync(int id, Pharmacy updated)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Name = updated.Name;
            existing.Address = updated.Address;
            existing.Phone = updated.Phone;
            existing.LicenseNumber = updated.LicenseNumber;
            existing.City = updated.City;
            existing.WorkingTime = updated.WorkingTime;
            existing.DoctorEmail = updated.DoctorEmail;
            existing.DoctorNumber = updated.DoctorNumber;
            existing.Status = updated.Status;

            _repo.Update(existing);
            await _repo.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            _repo.Delete(existing);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<Pharmacy?> UpdateStatusAsync(int pharmacyId, PharmacyStatus status, DateTime? approvedUntil)
        {
            var pharmacy = await _repo.GetByIdAsync(pharmacyId);
            if (pharmacy == null)
                return null;

            pharmacy.Status = status;

            if (status == PharmacyStatus.Approved)
            {
                pharmacy.ApprovedUntil = approvedUntil ?? DateTime.UtcNow.AddMonths(1);
            }
            else
            {
                pharmacy.ApprovedUntil = approvedUntil;
            }

            await _repo.SaveChangesAsync();
            return pharmacy;
        }

        public async Task<IEnumerable<Pharmacy>> GetByStatusAsync(PharmacyStatus status)
        {
            return await _repo.GetByStatusAsync(status);
        }

    }
}
