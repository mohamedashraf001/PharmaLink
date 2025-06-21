using DataLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IPharmacyService
    {
        Task<IEnumerable<Pharmacy>> GetAllAsync();
        Task<Pharmacy?> GetByIdAsync(int id);
        Task<Pharmacy> CreateAsync(Pharmacy pharmacy);
        Task<Pharmacy?> UpdateAsync(int id, Pharmacy pharmacy);
        Task<bool> DeleteAsync(int id);
        Task<Pharmacy?> UpdateStatusAsync(int pharmacyId, PharmacyStatus status, DateTime? approvedUntil);
        Task<IEnumerable<Pharmacy>> GetByStatusAsync(PharmacyStatus status);

    }
}
