using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml;
namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {

        string n = Environment.NewLine;
        public Form1()
        {
            InitializeComponent();
        }
        string HTMLstr;
        string hoststime = "hosts更新时间：";
        private void button1_Click(object sender, EventArgs e)
        {


            state.Text = null;
            HTMLstr = WEB.request(websrc.Text);  //捕捉网站

            if (HTMLstr.Length == 0)
            {
                altertime.Text += "获取失败！";
                state.Text = "网络连接失败或者程序目前不可用，请联系作者获取最新版本。" + n + state.Text;
            }
            else
            {
                try
                {
                    string[] date = draw.drawDate(HTMLstr);
                    altertime.Text = hoststime+"20" + date[0];
                }
                catch (Exception)
                { altertime.Text = hoststime+"获取失败！"; }
            }
            string text = "";
            try
            {
                text = HTMLstr.Replace("<br />", "\r\n");
                if (text.Length != 0)
                {
                    int begin = text.IndexOf(hostsstart.Text);   //记录开始位置和结束位置，截取中间字符串。
                    int end = text.IndexOf(hostsend.Text);
                    text = text.Substring(begin, end - begin + 2);

                    string text1 = NoHtml.NoHTML(text);


                    string dnspath = @"c:\windows\system32\drivers\etc\dns.bat";
                    try
                    {
                        StreamWriter sw = new StreamWriter(dnspath);
                        state.Text = "已经创建清除DNS文件" + n + state.Text;
                        sw.WriteLine("ipconfig /flushdns");
                        state.Text = "已经加载代码" + n + state.Text;
                        sw.Close();
                        Process process = new Process();
                        process.StartInfo.FileName = dnspath;
                        process.StartInfo.UseShellExecute = true;


                        process.Start();
                        state.Text = "清理DNS缓存完毕。" + n + state.Text;

                    }
                    catch (Exception)
                    {
                        state.Text = "清理DNS缓存失败" + n + state.Text;
                    }
                    string path = hostspath.Text;  //host文件地址。
                    FileInfo fileInfo = new FileInfo(path);
                    try
                    {
                        fileInfo.Attributes &= ~FileAttributes.ReadOnly;
                        state.Text = "已为hosts文件清除只读属性。"+ n + state.Text;
                        File.Delete(path);
                        state.Text = "已经清空原hosts文件。"+ n + state.Text ;
                    }
                    catch (Exception)
                    {
                        state.Text = "清空原hosts文件失败"+ n + state.Text;
                    }
                    try
                    {

                        System.IO.File.WriteAllText(path, text1);
                        state.Text = "已创建新的hosts文件并写入" + n + state.Text;

                        if (checkBox1.Checked == true)
                        {
                            fileInfo.Attributes |= FileAttributes.ReadOnly;
                            state.Text = "已为hosts文件设置只读属性。"+ n + state.Text;
                        }
                        label1.Text = "修改状态：成功！畅游吧。";

                    }
                    catch (Exception)
                    {

                        label1.Text = "修改状态：失败~请检查权限！";
                    }


                }
                else
                {
                    label1.Text = "修改状态：失败~请检查网络！";
                    state.Text = "网络连接失败或者程序目前不可用，请联系作者获取最新版本。" + n + state.Text;
                }
            }
            catch (Exception)
            { state.Text = "修改失败。获取hosts异常。"+ n + state.Text;
            label1.Text = "修改状态：修改失败。";
            }



        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //设置显示是否获取管理员权限。
            if (isAdmin.IsAdministrator())
                label3.Text += "已获取。";
            else
                label3.Text += "未获取。";
            //设置显示hosts 更新时间。

            HTMLstr = WEB.request(websrc.Text);  //捕捉网站
            if (HTMLstr.Length == 0)
            { altertime.Text = hoststime+"获取失败！";
              state.Text = "网络连接失败或者程序目前不可用，请联系作者获取最新版本。"+ n + state.Text;   
            }
            else
            {
                try
                {
                    string[] date = draw.drawDate(HTMLstr);
                    altertime.Text = hoststime+"20" + date[0];
                }
                catch (Exception)
                { altertime.Text = hoststime+"获取失败！"; }
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {

            
        }

        private void websrc_TextChanged(object sender, EventArgs e)
        {
            HTMLstr = WEB.request(websrc.Text);  //捕捉网站
            if (HTMLstr.Length == 0)
            {
                altertime.Text = hoststime+"获取失败！";
                state.Text = "网络连接失败或者程序目前不可用，请联系作者获取最新版本。" + n + state.Text;
            }
            else
            {
                try
                {
                    string[] date = draw.drawDate(HTMLstr);
                    altertime.Text = hoststime+"20" + date[0];
                }
                catch (Exception)
                { altertime.Text = hoststime+"获取失败！"; }
            }
        }


    }
}
