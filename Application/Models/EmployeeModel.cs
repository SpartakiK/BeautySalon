using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
