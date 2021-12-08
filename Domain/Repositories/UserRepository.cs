using Entities;
using EntityFrameworkCore.DbConnection;
using Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnect _db;
        public UserRepository(DbConnect db)
        {
            _db = db;
        }
        
        public void Delete(int id)
        {
            var user =  _db.Users.FirstOrDefault(a => a.Id == id);
            if (user != null)
            {
                _db.Users.Remove(user);
            }
        }

        public UserEntity Edit(UserEntity entity)
        {
            var user = _db.Users.FirstOrDefault(a => a.Id == entity.Id);
            user.Name = entity.Name;
            user.Surname = entity.Surname;
            user.PhoneNumber = entity.PhoneNumber;
            user.Password = entity.Password;
            user.AuthCode = entity.AuthCode;
            user.Status = entity.Status;
            return user;

        }

        public IEnumerable<UserEntity> GetAll()
        {
            return  _db.Users.ToList();
        }

        public UserEntity GetById(int id)
        {
            return  _db.Users.FirstOrDefault(a => a.Id == id);
        }

        public UserEntity GetByPhoneNubmer(string phonenumber)
        {
            return  _db.Users.FirstOrDefault(a => a.PhoneNumber == phonenumber);
        }

        public UserEntity Insert(UserEntity entity)
        {
            _db.Users.Add(entity);
            return entity;
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}