using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OrderStatusHistoryEntity : BaseEntity
    {
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }
        public string Status { get; set; }
    }
}
