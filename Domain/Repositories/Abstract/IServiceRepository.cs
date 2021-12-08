using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstract
{
    public interface IServiceRepository 
    {
        IEnumerable<ServiceEntity> GetAll();
        ServiceEntity GetById(int id);
        ServiceEntity Insert(ServiceEntity entity);
        ServiceEntity Edit(ServiceEntity entity);
        void Delete(int id);
        void Save();
    }
}
