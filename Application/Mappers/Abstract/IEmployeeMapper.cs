using Domain.Mappers.Abstract;
using Domain.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract.Mappers
{
    public interface IEmployeeMapper : IMapper<EmployeeEntity, EmployeeModel>
    {

    }
}
