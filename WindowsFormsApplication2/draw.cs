using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace WindowsFormsApplication2
{
    class draw
    {

        /**
         返回所有的IP数据*/
        public static string[] drawIP(string strHTML)
        {
            string pattern = "\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}";
            return drawAll(strHTML, pattern);

        }
        /*
         * 返回所有的url数据**/
       public static string[] drawUrl(string strHTML)
        {

            string pattern = "([a-zA-Z0-9]([a-zA-Z0-9\\-]{0,61}[a-zA-Z0-9])?\\.)+[a-zA-Z]{2,6}";
            return drawAll(strHTML, pattern);

        }
        /*
         * 返回所有的日期数据**/
        public static string[] drawDate(string strHTML)
        {
            string pattern = "\\d{2}[\\.-/][01]{0,1}\\d[\\.-/][0123]{0,1}\\d";
            return drawAll(strHTML, pattern);
        }

        public static string[] drawAll(string strHtml,string strPattern)
        {

            string[] a = new string[3000];
            Regex reg = new Regex(strPattern, RegexOptions.IgnoreCase);

            MatchCollection mc = reg.Matches(strHtml);
            for (int i = 0; i < mc.Count; i++)
            {
                a[i] = mc[i].Value;

            }

            return a;

        }

    }
}
