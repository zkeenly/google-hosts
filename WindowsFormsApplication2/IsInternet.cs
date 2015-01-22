using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
namespace WindowsFormsApplication2
{
    class IsInternet
    {
        public static bool Network()
        {
            try
            {
                new Ping().Send("www.baidu.com");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}