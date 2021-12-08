using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public UserModel Customer { get; set; }
        public EmployeeModel Employee { get; set; }
        public string Stage { get; set; }
        public DateTime MakingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
