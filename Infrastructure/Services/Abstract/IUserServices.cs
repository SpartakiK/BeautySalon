using Domain.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IUserServices
    {
        UserEntity EditUserAccount(UserModel newvalues);

        void Create(UserModel values);

        bool PasswordCheck(string phonenumber, string password);

        UserModel GetByPhoneNubmer(string phonenumber);

        UserModel GetById(int id);
        public void Save();
    }
}
