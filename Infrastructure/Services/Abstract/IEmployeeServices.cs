using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Abstract
{
    public interface IEmployeeServices
    {
        IEnumerable<EmployeeModel> GetList();

        EmployeeModel GetById(int id);
        EmployeeModel GetByUserId(int id);

        void Create(EmployeeModel values);
        void Delete(int id);
        void Save();
    }
}
