using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstract
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeEntity> GetAll();
        EmployeeEntity GetById(int id);
        EmployeeEntity Insert(EmployeeEntity entity);
        EmployeeEntity Edit(EmployeeEntity entity);

        public EmployeeEntity GetByUserId(int id);
        void Delete(int id);
        void Save();
    }
}
