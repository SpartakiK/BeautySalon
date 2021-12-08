using Domain.Mappers.Abstract;
using Domain.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers
{
   public class ServiceMapper : IServiceMapper
    {
        public ServiceEntity ToEntity(ServiceModel values)
        {
            var userentity = new ServiceEntity
            {
                Id = values.Id,
                Name = values.Name,
                Price = values.Price,
                Comission = values.Comission,
                CreationDate = values.CreationDate,
                Description = values.Description
            };
            return userentity;
        }
        public  ServiceModel ToModel(ServiceEntity values)
        {
            var usermodel = new ServiceModel
            {
                Id = values.Id,
                Name = values.Name,
                Price = values.Price,
                Comission = values.Comission,
                CreationDate = values.CreationDate,
                Description = values.Description
                
            };
            return usermodel;
        }
        public  IEnumerable<ServiceEntity> ToEntityList(IEnumerable<ServiceModel> modellist)
        {
            var entitylist = new List<ServiceEntity>();
            foreach (var item in modellist)
            {
                entitylist.Add(ToEntity(item));
            }
            return entitylist;
        }

        public  IEnumerable<ServiceModel> ToModelList(IEnumerable<ServiceEntity> entitylist)
        {
            var modellist = new List<ServiceModel>();
            foreach (var item in entitylist)
            {
                modellist.Add(ToModel(item));
            }

            return modellist;
        }
    }
}
