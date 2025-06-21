using DataAccessLayer.Data;
using DataLayer.Entities;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class PharmacyRepository : IPharmacyRepository
    {
        private readonly AppDbContext _context;

        public PharmacyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pharmacy>> GetAllAsync()
        {
            return await _context.Pharmacies.ToListAsync();
        }

        public async Task<Pharmacy?> GetByIdAsync(int id)
        {
            return await _context.Pharmacies.FindAsync(id);
        }

        public async Task AddAsync(Pharmacy pharmacy)
        {
            await _context.Pharmacies.AddAsync(pharmacy);
        }

        public void Update(Pharmacy pharmacy)
        {
            _context.Pharmacies.Update(pharmacy);
        }

        public void Delete(Pharmacy pharmacy)
        {
            _context.Pharmacies.Remove(pharmacy);
        }

        public async Task<Pharmacy?> UpdateStatusAsync(int pharmacyId, PharmacyStatus status, DateTime? approvedUntil)
        {
            var pharmacy = await _context.Pharmacies.FindAsync(pharmacyId);
            if (pharmacy == null)
                return null;

            pharmacy.Status = status;
            pharmacy.ApprovedUntil = approvedUntil;
            await _context.SaveChangesAsync();
            return pharmacy;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pharmacy>> GetByStatusAsync(PharmacyStatus status)
        {
            return await _context.Pharmacies
                .Where(p => p.Status == status)
                .ToListAsync();
        }

    }
}
