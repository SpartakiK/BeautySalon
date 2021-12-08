using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Abstract
{
    public interface IServiceServices
    {
        IEnumerable<ServiceModel> GetList();

        ServiceModel GetById(int id);
        void Edit(ServiceModel values);
        void Create(ServiceModel values);
        void Delete(int id);
        void Save();

    }
}
