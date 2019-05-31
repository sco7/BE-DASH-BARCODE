using FontaineVerificationProject.Models;
using FontaineVerificationProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FontaineVerificationProject.Processers
{
    public class UserProcessor
    {
        public static bool ProcessAddUser(User user)
        {
            //Processing, Validating, Formating

            return UserRepository.AddUserToDB(user);
        }

        public static bool ProcessDeleteUser(User user)
        {
            //Processing, Validating, Formating

            return UserRepository.DeleteUserFromDB(user);
        }
    }
}
