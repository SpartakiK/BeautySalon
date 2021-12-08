using Features.Commands.UserCommands;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Business.Interfaces;

namespace Business.Services
{
    public class EditUserAccount : IService<User>
    {
        private readonly  UserRepository userRepository;
        private readonly CheckIfEmployeeCommand checkIfEmployee;
        private readonly MakeCustomerCommand makeCustomer;
        private readonly MakeAdminCommand makeAdmin;
        private readonly MakeEmployeeCommand makeEmployee;
        private readonly BlockUserCommand blockUser;
        public EditUserAccount
            (
            UserRepository _userRepository,
            CheckIfEmployeeCommand _checkIfemployee,
            MakeCustomerCommand _makeCustomer,
            MakeAdminCommand _makeAdmin,
            MakeEmployeeCommand _makeEmployee,
            BlockUserCommand _blockUser
            )
        {
            userRepository = _userRepository;
            makeAdmin = _makeAdmin;
            makeEmployee = _makeEmployee;
            checkIfEmployee = _checkIfemployee;
            makeCustomer = _makeCustomer;
            blockUser = _blockUser;

        }

        public async Task<User> Action(User newvalues)
        {
            User user = await userRepository.GetById(newvalues.Id);
            var isemployee = Convert.ToBoolean(checkIfEmployee.Action(newvalues.Id));

            if (user != null)
            {
                if (user.Status == newvalues.Status)
                {
                    await userRepository.Edit(newvalues);
                }
                if (newvalues.Status == "Customer")
                {
                    await makeCustomer.Action(user.Id);
                    await userRepository.Edit(newvalues);
                }
                if (newvalues.Status == "Employee")
                {
                    await makeEmployee.Action(user.Id);
                    await userRepository.Edit(newvalues);
                }
                if (newvalues.Status == "Admin")
                {
                    await makeAdmin.Action(user.Id);
                    await userRepository.Edit(newvalues);
                }
                if (newvalues.Status == "Blocked")
                {
                    await blockUser.Action(user.Id);
                }
            }
            return user;
        }       
        
    }
}