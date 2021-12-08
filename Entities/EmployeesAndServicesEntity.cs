using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EmployeesAndServicesEntity : BaseEntity
    {
        public int EmployeeId { get; set; }
        public int ServiceId { get; set; }
    }
}
