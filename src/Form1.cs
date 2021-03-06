﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace mailbox_desktop
{
    public class GeneralSettings
    {
        public string enableWebRTC { get; set; }
        public string agent { get; set; }
        public string noproxyserver { get; set; }
        public string disablecanvas { get; set; }
        public string disablegpu { get; set; }
        public string disablewebgl { get; set; }
        public string externalbrowser { get; set; }

        public GeneralSettings(){
        }
    }

    public partial class Form1 : Form
    {
        GeneralSettings settings = new GeneralSettings();

        #region " APIS "
        //
        [DllImport("winmm.dll", EntryPoint = "PlaySound", SetLastError = true, CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern bool PlaySound(
            string szSound,
            System.IntPtr hMod,
            PlaySoundFlags flags);

        public enum PlaySoundFlags : int
        {
            SND_SYNC = 0x0,     // play synchronously (default)
            SND_ASYNC = 0x1,    // play asynchronously
            SND_NODEFAULT = 0x2,    // silence (!default) if sound not found
            SND_MEMORY = 0x4,       // pszSound points to a memory file
            SND_LOOP = 0x8,     // loop the sound until next sndPlaySound
            SND_NOSTOP = 0x10,      // don't stop any currently playing sound
            SND_NOWAIT = 0x2000,    // don't wait if the driver is busy
            SND_ALIAS = 0x10000,    // name is a registry alias
            SND_ALIAS_ID = 0x110000,// alias is a predefined ID
            SND_FILENAME = 0x20000, // name is file name
            SND_RESOURCE = 0x40004, // name is resource name or atom
            SND_SYSTEM = 0x00200000 //  treat this as a system sound
        };
        //

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        public const int SWP_SHOWWINDOW = 0x40;
        public const int SWP_NOSIZE = 0x1;
        #endregion

        internal string agent_safari = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Safari/537.36 Edge/15.15063";

        public Form1()
        {
            InitializeComponent();

            trayIcon.Icon = mailbox_desktop.Properties.Resources.gmail_blue;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.Text = Application.CompanyName + "." + Application.ProductName + " v" + Application.ProductVersion;

            iniParser parser = new iniParser(Application.StartupPath + "\\settings.ini");

            if (!File.Exists(Application.StartupPath + "\\settings.ini"))
            {

                parser.AddSetting("general", "enableWebRTC", "1");
                parser.AddSetting("general", "agent", agent_safari);
                parser.AddSetting("general", "noproxyserver", "1");
                parser.AddSetting("general", "disablecanvas", "1");
                parser.AddSetting("general", "disablegpu", "1");
                parser.AddSetting("general", "disablewebgl", "1");
                parser.AddSetting("general", "externalbrowser", "");


                parser.AddSetting("url0", "url", "https://mail.google.com/mail/u/0/#inbox");
                parser.AddSetting("url0", "title", "gmail");
                parser.AddSetting("url0", "cookies_jar", "gmail342342D");
                parser.AddSetting("url0", "notification_keyword", "(");
                parser.AddSetting("url0", "notification_icon", "");
                parser.AddSetting("url0", "notification_show_window", "0");
                parser.AddSetting("url0", "no_at_startup", "0");

                parser.AddSetting("cookies", "exclude", "google;messenger;linkedin;yahoo");
                parser.SaveSettings();

                MessageBox.Show(Application.StartupPath + "\\settings.ini\r\n created, adjust the settings and reload the application.");
                return;
            }

            ////toolstrip @ tab
            //ToolStripMenuItem ts;

            //GENERAL
            settings.externalbrowser = parser.GetSetting("general", "externalbrowser");
            settings.enableWebRTC = parser.GetSetting("general", "enableWebRTC");
            settings.agent = parser.GetSetting("general", "agent");

            // Don't use a proxy server, always make direct connections. Overrides any other proxy server flags that are passed.
            // Slightly improves Cef initialize time as it won't attempt to resolve a proxy https://github.com/cefsharp/CefSharp/wiki/General-Usage#proxy-resolution
            settings.noproxyserver = parser.GetSetting("general", "noproxyserver");

            //fingerprints
            settings.disablecanvas = parser.GetSetting("general", "disablecanvas");
            settings.disablegpu = parser.GetSetting("general", "disablegpu");
            settings.disablewebgl = parser.GetSetting("general", "disablewebgl");

            //tray options
            ToolStripMenuItem ts = null;
            ts = new ToolStripMenuItem();
            ts.Tag = ((settings.enableWebRTC == "1") ? "0" : "1");
            ts.Text = ((settings.enableWebRTC == "1") ? "disabled WebRTC" : "enabled WebRTC");
            toolStripRestart.DropDownItems.Add(ts);
            ts.Click += new System.EventHandler(toolstripRestartWebRTC_Clicked);

            ts = new ToolStripMenuItem();
            ts.Tag = (settings.agent.ToLower().Contains(") s") ? "c" : "s");
            ts.Text = (settings.agent.ToLower().Contains(") s") ? "agent chrome" : "agent safari");
            toolStripRestart.DropDownItems.Add(ts);
            ts.Click += new System.EventHandler(toolstripRestartAgent_Clicked);


            //enumerate ini - max acceptable 10 entries
            for (int i = 0; i < 10; i++)
            {
                if (parser.EnumSection("url" + i).Length == 7)
                {
                    parse_url_item(parser, i.ToString(), true);
                }

                tabControl1.SelectedIndex = 0;
            }

            //add separator
            toolStripOpen.DropDownItems.Add(new ToolStripSeparator());

            //add webRTC
            ts = new ToolStripMenuItem();
            ts.Tag = "WebRTC";
            ts.Text = "test webRTC";
            toolStripOpen.DropDownItems.Add(ts);
            ts.Click += new System.EventHandler(toolstripOpenChildWebRTC_Clicked);


            //add Agent
            ts = new ToolStripMenuItem();
            ts.Tag = "TestAgent";
            ts.Text = "test agent";
            toolStripOpen.DropDownItems.Add(ts);
            ts.Click += new System.EventHandler(toolstripOpenChildTestAgent_Clicked);

            //add Agent
            ts = new ToolStripMenuItem();
            ts.Tag = "FingerPrint";
            ts.Text = "test fingerprint";
            toolStripOpen.DropDownItems.Add(ts);
            ts.Click += new System.EventHandler(toolstripOpenChildTestFingerPrint_Clicked);

        }



        private void parse_url_item(iniParser parser, string url_no, bool add2toolstrip)
        {
            string url = parser.GetSetting("url" + url_no, "url");
            string cookies_jar = parser.GetSetting("url" + url_no, "cookies_jar");
            string notification_keyword = parser.GetSetting("url" + url_no, "notification_keyword");
            string notification_icon = parser.GetSetting("url" + url_no, "notification_icon");
            string title = parser.GetSetting("url" + url_no, "title");
            string no_at_startup = parser.GetSetting("url" + url_no, "no_at_startup");
            string notification_show_window = parser.GetSetting("url" + url_no, "notification_show_window");

            //create instance of CefSharp + add new tab [start]
            if (no_at_startup == "0" || !add2toolstrip)
            {
                add_tab(title, cookies_jar, url, notification_keyword, notification_icon, notification_show_window);
            }
            //create instance of CefSharp + add new tab [end]

            //add to toolstrip open [start]
            if (add2toolstrip)
            {
                ToolStripMenuItem ts = new ToolStripMenuItem();
                ts.Tag = url_no;
                ts.Text = title;
                toolStripOpen.DropDownItems.Add(ts);
                ts.Click += new System.EventHandler(toolstripOpenChild_Clicked);
            }
            //add to toolstrip open [end]

        }

        private void add_tab(string title, string cookies_jar, string url, string notification_keyword, string notification_icon, string notification_show_window)
        {
            TabPage tp = new TabPage(title);
            tabControl1.TabPages.Add(tp);

            CefControl1 x = new CefControl1(tabControl1.TabPages.Count - 1, cookies_jar, url, settings, notification_keyword, notification_icon, notification_show_window);
            x.StatusChanged += new CefControl1.status_changed(StatusUpdated);
            x.TabText += new CefControl1.tab_text(TabText);
            x.OpenChild += new CefControl1.open_child(OpenChildTab);

            x.Dock = DockStyle.Fill;
            tp.Controls.Add(x);

            //CEF start render when is visible
            tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
        }


        #region " CEF control events "

        private delegate void SafeCallDelegate(int tabindex, string value);
        private void TabText(int tabindex, string value)
        {
            if (tabControl1.InvokeRequired)
            {
                this.BeginInvoke(new SafeCallDelegate(this.TabText), new object[] { tabindex, value });
                return;
            }

            if (tabindex < tabControl1.TabPages.Count) //fix error when previously 'close tab' and invoking this method
                tabControl1.TabPages[tabindex].Text = value;
        }

        private long sec_ticks=0; 
        private void StatusUpdated(string value, string icon_filepath, string notification_show_window)
        {
            if (value == null)
            {
                trayIcon.Text = "PipisCrew mail desktop";
                //restore normal icon
                trayIcon.Icon = mailbox_desktop.Properties.Resources.gmail_blue;
            }
            else
            {
                if (icon_filepath != null)
                {
                    if (File.Exists(Application.StartupPath + "\\" + icon_filepath))
                        trayIcon.Icon = Icon.ExtractAssociatedIcon(Application.StartupPath + "\\" + icon_filepath);
                    else
                        trayIcon.Icon = mailbox_desktop.Properties.Resources.gmail_red;
                }

                long ticks = DateTime.Now.Ticks;

                //when notification coming on the next 4sec - sound nothing (fix for messenger)
                if (sec_ticks != 0 && ((ticks - sec_ticks) < 40000000))
                {
                    sec_ticks = DateTime.Now.Ticks;
                    return;
                }
                
                sec_ticks = DateTime.Now.Ticks;

                PlaySound("MailBeep", new System.IntPtr(), PlaySoundFlags.SND_SYSTEM | PlaySoundFlags.SND_NODEFAULT | PlaySoundFlags.SND_ALIAS);

                trayIcon.Text = value;
                if (notification_show_window == "1")
                {
                    this.Show();
                    this.WindowState = FormWindowState.Normal;

                    int x = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
                    int y = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;
                    SetWindowPos(this.Handle, 0, x, y, 0, 0, SWP_SHOWWINDOW | SWP_NOSIZE);
                }
            }


        }

        private delegate void open_child_delegate(string url, string cache_dir, bool use_the_same_cookies);
        private void OpenChildTab(string url, string cache_dir, bool use_the_same_cookies)
        {
            if (tabControl1.InvokeRequired)
            {
                this.BeginInvoke(new open_child_delegate(OpenChildTab), new object[] { url, cache_dir, use_the_same_cookies });
                return;
            }

            add_tab("child", use_the_same_cookies ? cache_dir : "", url, "", "", "");

        }
        #endregion


        #region " TRAY "

        private void toolstripRestartWebRTC_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem tmp = (sender as ToolStripMenuItem);
            string webRTC_new_value = tmp.Tag.ToString();
            iniParser parser = new iniParser(Application.StartupPath + "\\settings.ini");
            parser.AddSetting("general", "enableWebRTC", webRTC_new_value);
            parser.SaveSettings();

            //https://stackoverflow.com/questions/95098/why-is-application-restart-not-reliable
            //adds property to exe.config

            Application.Exit();
        }

        private void toolstripRestartAgent_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem tmp = (sender as ToolStripMenuItem);
            string agent_new_value = tmp.Tag.ToString();

            if (agent_new_value == "c")
                agent_new_value = "";
            else
                agent_new_value = agent_safari;

            iniParser parser = new iniParser(Application.StartupPath + "\\settings.ini");
            parser.AddSetting("general", "agent", agent_new_value);
            parser.SaveSettings();

            Application.Exit();
        }


        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                show_form();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                hide2tray();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                hide2tray();
            }
            else
            {
                for (int i = 0; i < tabControl1.TabPages.Count-1; i++)
                {
                    tabControl1.TabPages[i].Dispose();
                }
            }
        }

        private void show_form()
        {
            //this.ShowInTaskbar = true;
            this.Show();
            this.WindowState = FormWindowState.Normal;

            // Set foreground window.
            SetForegroundWindow(this.Handle);
        }

        private void hide2tray()
        {
            //to minimize window
            this.WindowState = FormWindowState.Minimized;

            //to hide from taskbar
            // this.ShowInTaskbar = false;
            this.Hide();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            show_form();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            trayIcon.Text = "PipisCrew mail desktop";
            trayIcon.Icon = mailbox_desktop.Properties.Resources.gmail_blue;
        }

        #endregion


        #region " TAB CONTEXT "

        private void toolstripOpenChild_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem tmp = (sender as ToolStripMenuItem);
            string item_no = tmp.Tag.ToString();

            iniParser parser = new iniParser(Application.StartupPath + "\\settings.ini");
 
            parse_url_item(parser, item_no, false);
        }

        private void toolstripOpenChildWebRTC_Clicked(object sender, EventArgs e)
        {
            add_tab("test WebRTC", "", "https://test.webrtc.org/", "", "", "");
        }

        private void toolstripOpenChildTestAgent_Clicked(object sender, EventArgs e)
        {
            add_tab("test Agent", "", "https://www.whatismybrowser.com/detect/what-is-my-user-agent", "", "", "");
        }

        private void toolstripOpenChildTestFingerPrint_Clicked(object sender, EventArgs e)
        {
            add_tab("test FingerPrint", "", "https://browserleaks.com/canvas", "", "", "");
        }

        internal string clipboard_url = null;
        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                string g = General.GetFromClipboard();

                if (g.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    clipboard_url = g;
                    toolStripOpenInContainer.Visible = toolStripOpenInGlobal.Visible = toolStripSeparator3.Visible = true;
                }
                else
                {
                    toolStripOpenInContainer.Visible = toolStripOpenInGlobal.Visible = toolStripSeparator3.Visible = false;
                    clipboard_url = null;
                }

                contextTab.Show(this.tabControl1, e.Location);
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count > 1 && tabControl1.SelectedIndex > -1)
            {
                //tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                tabControl1.TabPages[tabControl1.SelectedIndex].Dispose();
            }

            new MemoryManagement().FlushMemory();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            CefControl1 user_ctl = (CefControl1)tabControl1.SelectedTab.Controls[0];
            user_ctl.refresh_page();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            CefControl1 user_ctl = (CefControl1)tabControl1.SelectedTab.Controls[0];
            user_ctl.dev_tools();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            int selectedtab = tabControl1.SelectedIndex;

            int count = tabControl1.TabPages.Count - selectedtab - 1;

            for (int i = 0; i < count; i++)
            {
                tabControl1.TabPages[selectedtab + 1].Dispose();
            }

            new MemoryManagement().FlushMemory();
        }

        private void toolStripCopyURL_Click(object sender, EventArgs e)
        {
            CefControl1 user_ctl = (CefControl1)tabControl1.SelectedTab.Controls[0];
            General.Copy2Clipboard(user_ctl.get_url());
        }

        private void toolStripCookies_Click(object sender, EventArgs e)
        {
            CefControl1 user_ctl = (CefControl1)tabControl1.SelectedTab.Controls[0];

            List<Tuple<string, string>> del = null;

            frmCookies f = new frmCookies(user_ctl.get_cookies());
            f.ShowDialog(out del);

            if (del != null)
            {
                foreach (Tuple<string, string> item in del)
                {
                    user_ctl.del_cookie("https://" + item.Item1, item.Item2);
                }
            }

            new MemoryManagement().FlushMemory();
        }

        private void toolStripOpenInContainer_Click(object sender, EventArgs e)
        {
            CefControl1 user_ctl = (CefControl1)tabControl1.SelectedTab.Controls[0];
            string cookies_jar = user_ctl.get_cache_name();

            add_tab("child", cookies_jar, clipboard_url, "", "", "");
        }

        private void toolStripOpenInGlobal_Click(object sender, EventArgs e)
        {
            add_tab("child", "", clipboard_url, "", "", "");
        }

        #endregion

    }

    public class MemoryManagement
    {
        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr proc, int min, int max);

        public void FlushMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
    }
}
