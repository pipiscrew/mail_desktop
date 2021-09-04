using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;

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

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        public const int SWP_SHOWWINDOW = 0x40;
        public const int SWP_NOSIZE = 0x1;
        #endregion

        //Firefox
        internal string agent_alternative = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:68.0) Gecko/20100101 Firefox/68.0";

        public Form1()
        {
            InitializeComponent();

            trayIcon.Icon = mailbox_desktop.Properties.Resources.gmail_blue;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (General.cfg == null)
            { // use close settings form, without save XML
                Application.Exit();
                return;
            }

            this.Text = Application.CompanyName + "." + Application.ProductName + " v" + Application.ProductVersion;

            //Construct TRAY ICON Toolstrips
            ToolStripMenuItem ts = null;
            ts = new ToolStripMenuItem();
            ts.Name = "webrtc";
            ts.Tag = (General.cfg.g.enableWebRTC ? false : true);
            ts.Text = (General.cfg.g.enableWebRTC ? "disabled WebRTC" : "enabled WebRTC");
            toolStripRestart.DropDownItems.Add(ts);
            ts.Click += new System.EventHandler(toolstripRestartWebRTC_Clicked);

            ts = new ToolStripMenuItem();
            ts.Name = "agent";
            ts.Tag = (General.cfg.g.agent.Equals(agent_alternative) ? "c" : "s");
            ts.Text = (General.cfg.g.agent.Equals(agent_alternative) ? "agent chrome" : "agent alternative");
            toolStripRestart.DropDownItems.Add(ts);
            ts.Click += new System.EventHandler(toolstripRestartAgent_Clicked);


            //iterate websites
            foreach (WebsiteDetail w in General.cfg.wList)
            {
                parse_url_item(w, true);

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



        private void parse_url_item(WebsiteDetail item, bool add2toolstrip)
        {
            //create instance of CefSharp + add new tab [start]
            if (!item.noAtStartup || !add2toolstrip)
            {
                add_tab(item);
            }
            //create instance of CefSharp + add new tab [end]

            //add to toolstrip open [start]
            if (add2toolstrip)
            {
                ToolStripMenuItem ts = new ToolStripMenuItem();
                ts.Text = item.title;
                toolStripOpen.DropDownItems.Add(ts);
                ts.Click += new System.EventHandler(toolstripOpenChild_Clicked);
            }
            //add to toolstrip open [end]

        }

        private void add_tab(WebsiteDetail item)
        {
            TabPage tp = new TabPage(item.title);
            tabControl1.TabPages.Add(tp);

            CefControl1 x = new CefControl1(tabControl1.TabPages.Count - 1, General.cfg.g, item);
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

        private long sec_ticks = 0;
        private void StatusUpdated(string value, string icon_filepath, bool notification_show_window)
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
                if (notification_show_window)
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

            WebsiteDetail x = new WebsiteDetail()
            {
                cookiesJar = use_the_same_cookies ? cache_dir : "",
                noAtStartup = false,
                notificationIcon = "",
                notificationKeyword = "",
                notificationShowWindow = false,
                title = "child",
                url = url
            };

            add_tab(x);
        }

        #endregion


        #region " TRAY "

        private void toolstripRestartWebRTC_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem tmp = (sender as ToolStripMenuItem);
            bool webRTC_new_value = tmp.Tag.ToBool();

            General.cfg.g.enableWebRTC = webRTC_new_value;
            XmlHelper.ToXmlFile(General.cfg, General.configPath);

            Application.Exit();
        }

        private void toolstripRestartAgent_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem tmp = (sender as ToolStripMenuItem);
            string agent_new_value = tmp.Tag.ToString();

            if (agent_new_value == "c")
                agent_new_value = "";
            else
                agent_new_value = agent_alternative;

            General.cfg.g.agent = agent_new_value;
            XmlHelper.ToXmlFile(General.cfg, General.configPath);

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
                for (int i = 0; i < tabControl1.TabPages.Count - 1; i++)
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

            var f = General.cfg.wList.Where(x => x.title.Equals(tmp.Text, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (f == null)
            {
                MessageBox.Show("Cant find the entry!");
                return;
            }

            parse_url_item(f, false);
        }

        private void toolstripOpenChildWebRTC_Clicked(object sender, EventArgs e)
        {

            WebsiteDetail x = new WebsiteDetail()
            {
                cookiesJar = "",
                noAtStartup = false,
                notificationIcon = "",
                notificationKeyword = "",
                notificationShowWindow = false,
                title = "test WebRTC",
                url = "https://test.webrtc.org/"
            };

            add_tab(x);

        }

        private void toolstripOpenChildTestAgent_Clicked(object sender, EventArgs e)
        {
            WebsiteDetail x = new WebsiteDetail()
            {
                cookiesJar = "",
                noAtStartup = false,
                notificationIcon = "",
                notificationKeyword = "",
                notificationShowWindow = false,
                title = "test Agent",
                url = "https://www.whatismybrowser.com/detect/what-is-my-user-agent"
            };

            add_tab(x);
        }

        private void toolstripOpenChildTestFingerPrint_Clicked(object sender, EventArgs e)
        {
            WebsiteDetail x = new WebsiteDetail()
            {
                cookiesJar = "",
                noAtStartup = false,
                notificationIcon = "",
                notificationKeyword = "",
                notificationShowWindow = false,
                title = "test FingerPrint",
                url = "https://browserleaks.com/canvas"
            };

            add_tab(x);
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

            WebsiteDetail x = new WebsiteDetail()
            {
                cookiesJar = cookies_jar,
                noAtStartup = false,
                notificationIcon = "",
                notificationKeyword = "",
                notificationShowWindow = false,
                title = "child",
                url = clipboard_url
            };

            add_tab(x);
        }

        private void toolStripOpenInGlobal_Click(object sender, EventArgs e)
        {
            WebsiteDetail x = new WebsiteDetail()
            {
                cookiesJar = "",
                noAtStartup = false,
                notificationIcon = "",
                notificationKeyword = "",
                notificationShowWindow = false,
                title = "child",
                url = clipboard_url
            };

            add_tab(x);
        }

        #endregion

        private void toolStripSettings_Click(object sender, EventArgs e)
        {
            frmSettings x = new frmSettings(false);
            x.ShowDialog();

            //reflect the modifications
            toolStripRestart.DropDownItems["webrtc"].Tag = (General.cfg.g.enableWebRTC ? false : true);
            toolStripRestart.DropDownItems["webrtc"].Text = (General.cfg.g.enableWebRTC ? "disabled WebRTC" : "enabled WebRTC");

            toolStripRestart.DropDownItems["agent"].Tag = (General.cfg.g.agent.Equals(agent_alternative) ? "c" : "s");
            toolStripRestart.DropDownItems["agent"].Text = (General.cfg.g.agent.Equals(agent_alternative) ? "agent chrome" : "agent alternative");
        }

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

    public class GeneralSettings
    {
        public bool enableWebRTC { get; set; }
        public string agent { get; set; }
        public bool noproxyserver { get; set; }
        public bool disablecanvas { get; set; }
        public bool disablegpu { get; set; }
        public bool disablewebgl { get; set; }
        public string externalbrowser { get; set; }
        public string cookies { get; set; }

        public GeneralSettings()
        {
        }
    }

    public class WebsiteDetail
    {
        public bool noAtStartup { get; set; }
        public string notificationKeyword { get; set; }
        public string url { get; set; }
        public string notificationIcon { get; set; }
        public string cookiesJar { get; set; }
        public string title { get; set; }
        public bool notificationShowWindow { get; set; }

        public WebsiteDetail()
        {
        }


    }

    public class ConfigApp
    {

        public GeneralSettings g { get; set; }
        public List<WebsiteDetail> wList { get; set; }


        public ConfigApp()
        {
            g = new GeneralSettings();
            wList = new List<WebsiteDetail>();
        }
    }
}
