using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OrderEntity : BaseEntity
    {
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public string Stage { get; set; }
        public DateTime MakingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string AdditionalInfo { get; set; }

    }
}
