using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OrderDetailEntity : BaseEntity
    {
        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        public int Comission { get; set; }
        public decimal Price { get; set; }
    }
}
