using DataLayer.Entities;

namespace DataLayer.Repositories.Interfaces
{
    public interface IPharmacyRepository
    {
        Task<IEnumerable<Pharmacy>> GetAllAsync();
        Task<Pharmacy?> GetByIdAsync(int id);
        Task AddAsync(Pharmacy pharmacy);
        void Update(Pharmacy pharmacy);
        void Delete(Pharmacy pharmacy);
        Task SaveChangesAsync();
        Task<Pharmacy?> UpdateStatusAsync(int pharmacyId, PharmacyStatus status, DateTime? approvedUntil);
        Task<IEnumerable<Pharmacy>> GetByStatusAsync(PharmacyStatus status);

    }
}
