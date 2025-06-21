using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Request
    {
        public int Id { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
        public int Quantity { get; set; }
        public int RequestingPharmacyId { get; set; }  
        public Pharmacy RequestingPharmacy { get; set; }

        public RequestStatus Status { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
