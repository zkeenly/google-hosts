using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Web;
namespace WindowsFormsApplication2
{
    public static class WEB
    {
        //请求一个web 网站的数据
        public static string request()
        {
            string text;
            try
            {

                WebRequest request = WebRequest.Create("http://www.360kb.com/kb/2_122.html" );

                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));

                text = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                response.Close();
            }
            catch (Exception )
            {
                return "";
            }
            
            return text;
        }

    }
}
