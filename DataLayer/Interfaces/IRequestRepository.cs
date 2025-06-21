using DataLayer.Entities;

public interface IRequestRepository
{
    Task<IEnumerable<Request>> GetAllAsync();
    Task<Request?> GetByIdAsync(int id);
    Task AddAsync(Request request);
    Task SaveChangesAsync();

    Task<IEnumerable<Request>> GetByPostIdAsync(int postId);
    Task<IEnumerable<Request>> GetByPharmacyIdAsync(int pharmacyId);
    void Delete(Request request);
    Task<Request?> GetByPostAndPharmacyAsync(int postId, int pharmacyId);
}
