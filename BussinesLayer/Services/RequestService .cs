using BussinesLayer.Interfaces;
using DataLayer.Entities;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _repo; 
    private readonly IPostRepository _postRepository;
    public RequestService(IRequestRepository repo, IPostRepository postRepository)
    {
        _repo = repo;
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<Request>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Request?> GetByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task<Request> CreateAsync(Request request)
    {
        request.RequestDate = DateTime.UtcNow;
        request.Status = RequestStatus.Pending;
        await _repo.AddAsync(request);
        await _repo.SaveChangesAsync();
        return request;
    }

    public async Task<IEnumerable<Request>> GetByPostIdAsync(int postId)
    {
        return await _repo.GetByPostIdAsync(postId);
    }

    public async Task<IEnumerable<Request>> GetByPharmacyIdAsync(int pharmacyId)
    {
        return await _repo.GetByPharmacyIdAsync(pharmacyId);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var request = await _repo.GetByIdAsync(id);
        if (request == null)
            return false;

        _repo.Delete(request);
        await _repo.SaveChangesAsync();
        return true;
    }

    public async Task<Request?> UpdateStatusAsync(int requestId, RequestStatus newStatus)
    {
        var request = await _repo.GetByIdAsync(requestId);
        if (request == null)
            return null;

        if (request.Status == RequestStatus.Accepted)
            return request; // لا تعيد القبول مرتين

        // لو الحالة الجديدة هي "Accepted" قلل الكمية من البوست
        if (newStatus == RequestStatus.Accepted)
        {
            var post = request.Post;
            if (post == null)
                return null;

            if (post.Quantity < request.Quantity)
                throw new InvalidOperationException("Not enough quantity in stock.");

            post.Quantity -= request.Quantity;
            _postRepository.Update(post); // لازم تمرر الـ PostRepo في Constructor
        }

        request.Status = newStatus;
        await _repo.SaveChangesAsync(); // لو حفظت البوست هنا برضو هيتم حفظ كل التغييرات

        return request;
    }


}
