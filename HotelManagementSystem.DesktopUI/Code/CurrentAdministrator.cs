using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManagementSystem.Entities;

namespace HotelManagementSystem.DesktopUI.Code
{
    public class CurrentAdministrator
    {
        private static Administrator currentAdmin;

        public static void SignIn(Administrator admin)
        {
            currentAdmin = admin;
        }

        public static int Id
        {
            get
            {
                return currentAdmin.Id;
            }
        }

        public static string Name
        {
            get
            {
                return currentAdmin.Name;
            }
        }

        public static string Login
        {
            get
            {
                return currentAdmin.Login;
            }
        }
    }
}
