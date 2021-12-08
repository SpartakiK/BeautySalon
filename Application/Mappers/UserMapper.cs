using Domain.Models;
using Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories.Abstract;
using Domain.Models.AuthorizationInstruments;
using Domain.Mappers.Abstract;

namespace Domain.Mappers
{
   public  class UserMapper : IUserMapper
    {
        private readonly IOrderRepository _orderRepository;
        public UserMapper(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public UserEntity ToEntity( UserModel values)
        {
            if (values == null)
            {
                return null;
            }
            var userentity = new UserEntity
            {   Id = values.Id,            
                Name = values.Name,
                Surname = values.Surname,
                Status = values.Status,
                PhoneNumber = values.PhoneNumber,
                Password = values.Password,
                CreationDate = values.CreationDate,
            };

            return userentity;
        }
        public  UserModel ToModel(UserEntity values)
        {
            if (values == null)
            {
                return null;
            }
            var usermodel = new UserModel
            {
                Id = values.Id,
                Name = values.Name,
                Surname = values.Surname,
                Status = values.Status,
                PhoneNumber = values.PhoneNumber,
                CreationDate = values.CreationDate,
                Password = values.Password           
            };
            return usermodel;
        }
        public  IEnumerable<UserEntity> ToEntityList( IEnumerable<UserModel> modellist)
        {
            var entitylist = new List<UserEntity>();
            foreach (var item in modellist)
            {                
                entitylist.Add(ToEntity(item));
            }
            return entitylist;
        }

        public  IEnumerable<UserModel> ToModelList(IEnumerable<UserEntity> entitylist)
        {
            var modellist = new List<UserModel>();
            foreach (var item in entitylist)
            {
                modellist.Add(ToModel(item));
            }

            return modellist;
        }
    }
}
