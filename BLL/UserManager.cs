using Model;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserManager
    {
        public UserManager() { }

        public int CheckUser(User user)
        {
            UserService userService = new UserService();
            try
            {
                return userService.getUser(user);
            }
            catch (Exception e)

            {

                throw new DataExceptions("Error checking user", e);

            }

            finally

            {

                userService = null;

            }
        }
    }
}
