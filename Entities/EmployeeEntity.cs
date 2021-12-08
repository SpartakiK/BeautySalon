using Entities.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EmployeeEntity : BaseEntity
    {
        public int UserId { get; set; }
    }
}
