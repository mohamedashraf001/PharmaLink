using DataAccessLayer.Data;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

public class RequestRepository : IRequestRepository
{
    private readonly AppDbContext _context;

    public RequestRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Request>> GetAllAsync()
    {
        return await _context.Requests.Include(r => r.Post).Include(r => r.RequestingPharmacy).ToListAsync();
    }

    public async Task<Request?> GetByIdAsync(int id)
    {
        return await _context.Requests.Include(r => r.Post).Include(r => r.RequestingPharmacy)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddAsync(Request request)
    {
        await _context.Requests.AddAsync(request);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Request>> GetByPostIdAsync(int postId)
    {
        return await _context.Requests
            .Include(r => r.Post)
            .Include(r => r.RequestingPharmacy)
            .Where(r => r.PostId == postId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Request>> GetByPharmacyIdAsync(int pharmacyId)
    {
        return await _context.Requests
            .Include(r => r.Post)
             .ThenInclude(p => p.Pharmacy)
            .Include(r => r.RequestingPharmacy)
            .Where(r => r.RequestingPharmacyId == pharmacyId)
            .ToListAsync();
    }

    public void Delete(Request request)
    {
        _context.Requests.Remove(request);
    }

    public async Task<Request?> GetByPostAndPharmacyAsync(int postId, int pharmacyId)
    {
        return await _context.Requests.FirstOrDefaultAsync(r => r.PostId == postId && r.RequestingPharmacyId == pharmacyId);
    }

}
