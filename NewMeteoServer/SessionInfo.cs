using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMeteoServer
{
    public static class SessionInfo
    {
        public static List<User> ActiveUsers;

        static SessionInfo()
        {
            ActiveUsers = new List<User>();
        }
    }
}
