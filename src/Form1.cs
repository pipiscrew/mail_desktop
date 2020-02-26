using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace mailbox_desktop
{
    public partial class Form1 : Form
    {

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

                parser.AddSetting("url0", "url", "https://mail.google.com/mail/u/0/#inbox");
                parser.AddSetting("url0", "title", "gmail");
                parser.AddSetting("url0", "cookies_jar", "gmail342342D");
                parser.AddSetting("url0", "notification_keyword", "(");
                parser.AddSetting("url0", "notification_icon", "");
                parser.AddSetting("url0", "no_at_startup", "0");

                parser.AddSetting("cookies", "exclude", "google;messenger;linkedin");
                parser.SaveSettings();

                MessageBox.Show(Application.StartupPath + "\\settings.ini\r\n created, adjust the settings and reload the application.");
                return;
            }

            ////toolstrip @ tab
            //ToolStripMenuItem ts;

            //GENERAL
            string enableWebRTC = parser.GetSetting("general", "enableWebRTC");
            string agent = parser.GetSetting("general", "agent");

            //tray options
            ToolStripMenuItem ts = null;
            ts = new ToolStripMenuItem();
            ts.Tag = ((enableWebRTC == "1") ? "0" : "1");
            ts.Text = ((enableWebRTC == "1") ? "disabled WebRTC" : "enabled WebRTC");
            toolStripRestart.DropDownItems.Add(ts);
            ts.Click += new System.EventHandler(toolstripRestartWebRTC_Clicked);

            ts = new ToolStripMenuItem();
            ts.Tag = (agent.ToLower().Contains(") s") ? "c" : "s");
            ts.Text = (agent.ToLower().Contains(") s") ? "agent chrome" : "agent safari");
            toolStripRestart.DropDownItems.Add(ts);
            ts.Click += new System.EventHandler(toolstripRestartAgent_Clicked);


            //enumerate ini - max acceptable 10 entries
            for (int i = 0; i < 10; i++)
            {
                if (parser.EnumSection("url" + i).Length == 6)
                {
                    parse_url_item(parser, i.ToString(), agent, enableWebRTC, true);
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

        }



        private void parse_url_item(iniParser parser, string url_no, string agent, string enableWebRTC, bool add2toolstrip)
        {
            string url = parser.GetSetting("url" + url_no, "url").ToLower();
            string cookies_jar = parser.GetSetting("url" + url_no, "cookies_jar");
            string notification_keyword = parser.GetSetting("url" + url_no, "notification_keyword");
            string notification_icon = parser.GetSetting("url" + url_no, "notification_icon");
            string title = parser.GetSetting("url" + url_no, "title");
            string no_at_startup = parser.GetSetting("url" + url_no, "no_at_startup");

            //create instance of CefSharp + add new tab [start]
            if (no_at_startup == "0" || !add2toolstrip)
            {
                add_tab(title, cookies_jar, url, agent, enableWebRTC, notification_keyword, notification_icon);
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

        private void add_tab(string title, string cookies_jar, string url, string agent, string enableWebRTC, string notification_keyword, string notification_icon)
        {
            TabPage tp = new TabPage(title);
            tabControl1.TabPages.Add(tp);

            CefControl1 x = new CefControl1(tabControl1.TabPages.Count - 1, cookies_jar, url, agent, enableWebRTC, notification_keyword, notification_icon);
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

        private void StatusUpdated(string value, string icon_filepath)
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
                    if (File.Exists(Application.StartupPath + "\\" + icon_filepath))
                        trayIcon.Icon = Icon.ExtractAssociatedIcon(Application.StartupPath + "\\" + icon_filepath);
                    else
                        trayIcon.Icon = mailbox_desktop.Properties.Resources.gmail_red;

                PlaySound("MailBeep", new System.IntPtr(), PlaySoundFlags.SND_SYSTEM | PlaySoundFlags.SND_NODEFAULT | PlaySoundFlags.SND_ALIAS);

                trayIcon.Text = value;
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

            add_tab("child", use_the_same_cookies ? cache_dir : "", url, "", "", "", "");

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
            trayIcon.Icon = mailbox_desktop.Properties.Resources.gmail_blue;
        }

        #endregion


        #region " TAB CONTEXT "

        private void toolstripOpenChild_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem tmp = (sender as ToolStripMenuItem);
            string item_no = tmp.Tag.ToString();
            iniParser parser = new iniParser(Application.StartupPath + "\\settings.ini");
            string enableWebRTC = parser.GetSetting("general", "enableWebRTC");
            string agent = parser.GetSetting("general", "agent");

            parse_url_item(parser, item_no, agent, enableWebRTC, false);
        }

        private void toolstripOpenChildWebRTC_Clicked(object sender, EventArgs e)
        {
            add_tab("test WebRTC", "", "https://test.webrtc.org/", "", "", "", "");
        }

        private void toolstripOpenChildTestAgent_Clicked(object sender, EventArgs e)
        {
            add_tab("test Agent", "", "https://www.whatismybrowser.com/detect/what-is-my-user-agent", "", "", "", "");
        }

        internal string clipboard_url = null;
        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                string g = General.GetFromClipboard().ToLower();

                if (g.StartsWith("http"))
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

            add_tab("child", cookies_jar, clipboard_url, "", "", "", "");
        }

        private void toolStripOpenInGlobal_Click(object sender, EventArgs e)
        {
            add_tab("child", "", clipboard_url, "", "", "", "");
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
