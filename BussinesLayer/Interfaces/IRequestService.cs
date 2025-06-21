using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Interfaces
{
    public interface IRequestService
    {
        Task<IEnumerable<Request>> GetAllAsync();
        Task<Request?> GetByIdAsync(int id);
        Task<Request> CreateAsync(Request request);
        Task<IEnumerable<Request>> GetByPostIdAsync(int postId);
        Task<IEnumerable<Request>> GetByPharmacyIdAsync(int pharmacyId);
        Task<bool> DeleteAsync(int id);
        Task<Request?> UpdateStatusAsync(int id, RequestStatus newStatus);
      
    }

}
