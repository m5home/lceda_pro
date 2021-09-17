using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Configuration;

namespace Test.Chrome
{
    public partial class Form1 : Form
    {
        public ChromiumWebBrowser chromeBrowser;
        
        private string file = System.Windows.Forms.Application.ExecutablePath;
        private Configuration config;

        public Form1()
        {
            InitializeComponent();
            // Start the browser after initialize global component
            InitializeChromium();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            config = ConfigurationManager.OpenExeConfiguration(file);
            if(config.AppSettings.Settings["WindowState"].Value == "Maximized")
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                int tmpWidth = int.Parse(config.AppSettings.Settings["WindowWidth"].Value);
                int tmpHeight = int.Parse(config.AppSettings.Settings["WindowHeight"].Value);
                int tmpTop = int.Parse(config.AppSettings.Settings["WindowTop"].Value);
                int tmpLeft = int.Parse(config.AppSettings.Settings["WindowLeft"].Value);

                if (tmpWidth < 1024) tmpWidth = 1024;
                if (tmpHeight < 768) tmpHeight = 768;
                if (tmpTop < 0 || tmpTop > Screen.PrimaryScreen.Bounds.Height - 768) tmpTop = (Screen.PrimaryScreen.Bounds.Height- tmpHeight)/2;
                if (tmpLeft < 0 || tmpLeft > Screen.PrimaryScreen.Bounds.Width - 1024) tmpLeft = (Screen.PrimaryScreen.Bounds.Width-tmpWidth)/2;

                this.Top = tmpTop;
                this.Left = tmpLeft;
                this.Width = tmpWidth;
                this.Height = tmpHeight;
            }
        }

        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            settings.AcceptLanguageList = "zh-CN";
            settings.Locale = "zh-CN";
            settings.MultiThreadedMessageLoop = true;
            Cef.Initialize(settings);
            // Create a browser component
            // Add it to the form and fill it to the form window.
            chromeBrowser = new ChromiumWebBrowser(null);
            this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
            chromeBrowser.Load("https://pro.lceda.cn/editor");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("请确认所有内容已保存，未保存的内容将丢失。\r\n确定要退出本程序吗？","即将退出程序",MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            if (this.WindowState==FormWindowState.Normal )
            {
                config.AppSettings.Settings["WindowState"].Value = "Normal";
                config.AppSettings.Settings["WindowWidth"].Value = this.Width.ToString();
                config.AppSettings.Settings["WindowHeight"].Value = this.Height.ToString();
                config.AppSettings.Settings["WindowTop"].Value = this.Top.ToString();
                config.AppSettings.Settings["WindowLeft"].Value = this.Left.ToString();
            }
            else if(this.WindowState == FormWindowState.Maximized)
            {
                config.AppSettings.Settings["WindowState"].Value = "Maximized";
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            Cef.Shutdown();
        }
    }
}