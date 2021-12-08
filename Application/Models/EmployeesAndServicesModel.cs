using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class EmployeesAndServicesModel
    {
        public int Id { get; set; }
        public EmployeeModel Employee { get; set; }
        public ServiceModel Service { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
