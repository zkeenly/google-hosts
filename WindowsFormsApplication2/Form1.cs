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
namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        string n = Environment.NewLine;
        public Form1()
        {
            InitializeComponent();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            state.Text = null;


            string text = WEB.request();
            text = text.Replace("<br />", "\r\n");
            if (text != "")
            {
            int begin = text.IndexOf("#base services");   //记录开始位置和结束位置，截取中间字符串。
            int end = text.IndexOf("#google hosts 2015 end");
            text = text.Substring(begin,end-begin+2);

            string text1 = NoHtml.NoHTML(text);
           // text1 = text1.Replace("\r","\r\n");

            //string textall = "";

            //string[] drawip = draw.drawIP(text);
            //string[] drawurl = draw.drawUrl(text);

            //for (int i = 0; i < drawip.Length; i++)
            //{
            //    textall += drawip[i] + " " + drawurl[i]+Environment.NewLine+"\r\n";
            //}


   
                string dnspath = @"c:\windows\system32\drivers\etc\dns.bat";
                try
                {
                    StreamWriter sw = new StreamWriter(dnspath);
                    state.Text += "已经创建清除DNS文件"+n;
                    sw.WriteLine("ipconfig /flushdns");
                    state.Text += "已经加载代码"+n;
                    sw.Close();
                    Process process = new Process();
                    process.StartInfo.FileName = dnspath;
                    process.StartInfo.UseShellExecute = true;


                    process.Start();
                    state.Text += "清理DNS缓存完毕。"+n;

                }
                catch (Exception)
                {
                    state.Text += "清理DNS缓存失败" + n;
                }
                string path = @"c:\windows\system32\drivers\etc\HOSTS";  //host文件地址。
                FileInfo fileInfo = new FileInfo(path);
                try
                {
                    fileInfo.Attributes &= ~FileAttributes.ReadOnly;
                    state.Text += "已为hosts文件清除只读属性。\r\n";
                    File.Delete(path);
                    state.Text += "已经清空原hosts文件。\r\n";
                }
                catch (Exception)
                {
                    state.Text += "清空原hosts文件失败\r\n";
                }
                try
                {

                    System.IO.File.WriteAllText(path, text1);
                    state.Text += "已创建新的hosts文件并写入" + n;


                    fileInfo.Attributes |= FileAttributes.ReadOnly;
                    state.Text += "已为hosts文件设置只读属性。";
                    label1.Text = "修改状态：成功！畅游吧。";

                }
                catch (Exception )
                {

                    label1.Text = "修改状态：失败~请检查权限！";
                }


            }
            else
            {
                label1.Text = "修改状态：失败~请检查网络！";
                state.Text += "网络连接失败或者程序目前不可用，请联系作者获取最新版本。" + n;   
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //设置显示是否获取管理员权限。
            if (isAdmin.IsAdministrator())
                label3.Text += "已获取。";
            else
                label3.Text += "未获取。";
            //设置显示hosts 更新时间。

            string text = WEB.request();  //捕捉网站
            if (text == "")
            { altertime.Text += "获取失败！";
              state.Text += "网络连接失败或者程序目前不可用，请联系作者获取最新版本。"+n;   
            }
            else
            {
                string[] date = draw.drawDate(text);
                altertime.Text += date[0];
 
            }
            if (IsInternet.Network())
            {
 
            }


        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {

                e.Cancel= true;         //取消关闭窗体事件
                notifyIcon1.Visible = true;
                this.Hide();
                this.ShowInTaskbar = false;
            }
            
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.ShowInTaskbar == false)
            {
                notifyIcon1.Visible = true;
                this.ShowInTaskbar = false;
                this.Show();
                this.Activate();
                this.WindowState = FormWindowState.Normal;
            }
        }
    }
}
