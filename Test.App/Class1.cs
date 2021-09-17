using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test.App
{
    public class MyApplication : ApplicationContext
    {

        static int WindowsCount;
        private Form frmSplash;
        private Form frmMain;
        public MyApplication()
        {
            WindowsCount = 0;
            /*实例化Windows1*/
            frmSplash = new Form1();
            frmSplash.FormClosed += OnMainFormClosed;//处理事件（窗口关闭的处理事件）
            WindowsCount += 1;//窗口总数加一

            frmMain = new Test.Chrome.Form1();
            frmMain.FormClosed += OnMainFormClosed;//处理事件（窗口关闭的处理事件）
            WindowsCount += 1;//窗口总数加一

            frmSplash.Show();
            frmMain.Show();
        }
        private void OnMainFormClosed(object sender, FormClosedEventArgs e)
        {
            WindowsCount -= 1;
            if (WindowsCount == 0)
                ExitThread();//调用ExitThead终止消息循环

        }
    }
}
