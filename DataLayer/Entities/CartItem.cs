using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public int PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; }

        public int Quantity { get; set; } 
    }
}
