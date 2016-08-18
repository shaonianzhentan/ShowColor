using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using GoodHelper.WinAPIHelper;
using System.Threading;
using System.Text.RegularExpressions;

namespace ShowColor
{
  
    public partial class Form1 : Form
    {
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WindowsAPI.WM_HOTKEY:
                    switch ((int)m.WParam)
                    {
                        case 1991:
                            WindowsAPI.keybd_event(WindowsAPI.VK_CONTROL, 0, 0, 0);
                            WindowsAPI.keybd_event(WindowsAPI.VK_C, 0, 0, 0);
                            WindowsAPI.keybd_event(WindowsAPI.VK_C, 0, 2, 0);
                            WindowsAPI.keybd_event(WindowsAPI.VK_CONTROL, 0, 2, 0);
                            Thread.Sleep(100);
                            string color = Clipboard.GetText();
                            Regex reg = new Regex("#(([0-9a-fA-F]{6})|([0-9a-fA-F]{3}))");
                            listView1.Items.Clear();
                            foreach (Match match in reg.Matches(color)) {
                                try
                                {
                                    ListViewItem lvi = new ListViewItem();
                                    lvi.Text = match.Value;
                                    lvi.ForeColor = Color.White;
                                    lvi.BackColor = ColorTranslator.FromHtml(match.Value);                                    
                                    
                                    listView1.Items.Add(lvi);
                                }
                                catch (Exception) { 

                                }
                            }                            
                            WindowsAPI.SetForegroundWindow(this.Handle);
                            break;
                    }
                    break;
            }
        }

        public Form1()
        {
            InitializeComponent();            
        }
      
        private void Form1_Load(object sender, EventArgs e)
        {
            WindowsAPI.RegisterHotKey(this.Handle, 1991, WindowsAPI.None, WindowsAPI.VK_F1);
        }
    }
}
