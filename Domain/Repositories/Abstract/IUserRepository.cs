using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<UserEntity> GetAll();
        UserEntity GetById(int id);
        UserEntity GetByPhoneNubmer(string phonenumber);
        UserEntity Insert(UserEntity entity);
        UserEntity Edit(UserEntity entity);
        void Delete(int id);
        void Save();
    }
}
