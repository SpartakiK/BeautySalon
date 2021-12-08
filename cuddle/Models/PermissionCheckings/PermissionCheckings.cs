using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.PermissionCheckings
{
    static public class PermissionCheckings
    {
        static public bool CheckIfAdmin(string loginfo)
        {
            if (loginfo == null)
            {
                return false;
            }
            var userinfo = loginfo.Split(" ");
            if (userinfo[1] != "Admin")
            {
                return false;
            }
            return true;
        }
        static public bool CheckIfAdminOrEmployee(string loginfo)
        {
            if (loginfo == null)
            {
                return false;
            }
            var userinfo = loginfo.Split(" ");
            if (userinfo[1] != "Admin" && userinfo[1] != "Employee")
            {
                return false;
            }
            return true;
        }


    }
}
