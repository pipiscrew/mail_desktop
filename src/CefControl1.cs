using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CefSharp.WinForms;
using System.IO;
using CefSharp;
using CefSharp.Handlers;
using System.Threading;

//https://github.com/cefsharp/CefSharp.MinimalExample
namespace mailbox_desktop
{
    public partial class CefControl1 : UserControl
    {
        private readonly ChromiumWebBrowser chrome;

        //event - status changed
        public delegate void status_changed(string value, string icon_filepath, string notification_show_window);
        public event status_changed StatusChanged;

        //event - tab text
        public delegate void tab_text(int tabindex, string value);
        public event tab_text TabText;

        //event - open child
        public delegate void open_child(string url, string cache_dir, bool use_the_same_cookies);
        public event open_child OpenChild;

        private string notification_keyword = null;
        private string icon_filepath = null;
        private int tabindex = -1;
        private string notification_show_window = "0";

        private string cache_dir;
        private string externalbrowser;

        public CefControl1()
        {
            InitializeComponent();
        }

        public CefControl1(int tabindex, string cache_dir, string url, GeneralSettings gSettings, string notification_keyword, string notification_icon, string notification_show_window)
        {
            InitializeComponent();

            this.icon_filepath = notification_icon;
            this.tabindex = tabindex;
            this.cache_dir = cache_dir;
            this.notification_show_window = notification_show_window;
            this.externalbrowser = gSettings.externalbrowser;

            cache_dir = Application.StartupPath + "\\cache\\" + cache_dir;
            this.notification_keyword = notification_keyword.ToLower();

            Directory.CreateDirectory(cache_dir);

            if (!Cef.IsInitialized)
            {
                CefSettings settings = new CefSettings();
                settings.UserAgent = gSettings.agent;
                settings.RootCachePath = Application.StartupPath + "\\cache\\";
                settings.CachePath = cache_dir;
                settings.PersistSessionCookies = true;

                //fake GPU details - https://stackoverflow.com/questions/55955203/how-to-give-fake-gpu-info-to-site
                //test with https://browserleaks.com/canvas
                if (gSettings.noproxyserver == "1")
                    settings.CefCommandLineArgs.Add("no-proxy-server");

                if (gSettings.disablecanvas == "1")
                    settings.CefCommandLineArgs.Add("disable-reading-from-canvas");

                if (gSettings.disablegpu == "1")
                    settings.CefCommandLineArgs.Add("disable-gpu");

                if (gSettings.disablewebgl == "1")
                    settings.CefCommandLineArgs.Add("disable-webgl");


                if (gSettings.enableWebRTC == "1")
                    settings.CefCommandLineArgs.Add("enable-media-stream", "1");

                //test @
                //https://test.webrtc.org/
                //https://www.vidyard.com/mic-test/


                //Initialize
                Cef.Initialize(settings);

                chrome = new ChromiumWebBrowser(url);
                chrome.DownloadHandler = new DownloadHandler();
                chrome.MenuHandler = new CustomMenuHandler();
            }
            else
            {
                //  https://github.com/cefsharp/CefSharp/wiki/General-Usage#request-context-browser-isolation

                var requestContextSettings = new RequestContextSettings();
                requestContextSettings.CachePath = cache_dir;
                requestContextSettings.PersistSessionCookies = true;

                chrome = new ChromiumWebBrowser(url);
                chrome.DownloadHandler = new DownloadHandler();
                chrome.MenuHandler = new CustomMenuHandler();
                chrome.RequestContext = new RequestContext(requestContextSettings);
            }

            this.Controls.Add(chrome);

            //https://cefsharp.github.io/api/57.0.0/html/E_CefSharp_OffScreen_ChromiumWebBrowser_TitleChanged.htm
            chrome.TitleChanged += new EventHandler<TitleChangedEventArgs>(this.CefBrowserTitleChanged);


            ////https://stackoverflow.com/a/31156730/1320686

            //chrome.FrameLoadEnd += (sender, args) =>
            //{
            //    // Console.WriteLine("##" + args.Frame.Name);
            //    //Wait for the MainFrame to finish loading
            //    if (args.Frame.IsMain)
            //    {

            //        Console.WriteLine("***" + args.Frame.Name);
            //        //   args.Frame.ExecuteJavaScriptAsync(this.GetScriptsBundle(), "about:blank", 1);
            //    }
            //};
        }


        //3. How do you expose a .NET class to JavaScript?
        //https://github.com/cefsharp/CefSharp/wiki/General-Usage
        //https://github.com/cefsharp/CefSharp/blob/master/CefSharp.Example/Resources/BindingTest.html


        private void CefBrowserTitleChanged(object sender, TitleChangedEventArgs e)
        {
            string newtitle = e.Title;

            if (newtitle.Length > 64)
                newtitle = e.Title.Substring(0, 63);
            else
                newtitle = e.Title;

            if (!string.IsNullOrEmpty(notification_keyword) && newtitle.ToLower().Contains(notification_keyword)) //(Regex.IsMatch(e.Title, pattern)) //(e.Title.Contains("("))
            {
                if (StatusChanged != null)
                    StatusChanged(newtitle, icon_filepath, notification_show_window);
            }

            if (TabText != null)
                TabText(tabindex, newtitle);
        }

        public void refresh_page()
        {
            chrome.Reload();
        }

        public void dev_tools()
        {
            chrome.ShowDevTools();
        }

        public string get_url()
        {
            return chrome.Address;
        }

        public bool del_cookie(string url, string name_value)
        {
            //base
            if (chrome.RequestContext == null)
            {
                return Cef.GetGlobalCookieManager().DeleteCookies(url, name_value);
            }
            else
            { //RequestContext

                ICookieManager cookie_del = chrome.RequestContext.GetCookieManager(null);
                return cookie_del.DeleteCookies(url, name_value);
            }

        }

        // src - https://github.com/cefsharp/CefSharp/issues/826#issuecomment-75226806
        public IEnumerable<Tuple<string, string, string, string, string, string>> get_cookies()
        {
            //https://github.com/cefsharp/CefSharp/issues/1770
            //https://github.com/cefsharp/CefSharp/blob/cefsharp/51/CefSharp.Core/RequestContext.h#L121


            CookieMonster visitor = new CookieMonster();


            //base
            if (chrome.RequestContext == null)
            {
                if (Cef.GetGlobalCookieManager().VisitAllCookies(visitor))
                    visitor.WaitForAllCookies();
            }
            else
            { //RequestContext

                ICookieManager cookie_manage = chrome.RequestContext.GetCookieManager(null);

                if (cookie_manage.VisitAllCookies(visitor))
                    visitor.WaitForAllCookies();
            }

            return visitor.NamesValues;
        }

        public string get_cache_name()
        {
            return this.cache_dir;
        }

        // called by CustomMenuHandler
        public void open_child_tab(string url, bool use_the_same_cookies)
        {
            if (OpenChild != null)
                OpenChild(url, this.cache_dir, use_the_same_cookies);

        }

        // called by CustomMenuHandler
        public void open_to_browser(string url)
        {
            if (string.IsNullOrEmpty(this.externalbrowser))
                System.Diagnostics.Process.Start(url);
            else
                System.Diagnostics.Process.Start(this.externalbrowser, url);
        }

        class CookieMonster : ICookieVisitor
        {
            readonly List<Tuple<string, string, string, string, string, string>> cookies = new List<Tuple<string, string, string, string, string, string>>();
            readonly ManualResetEvent gotAllCookies = new ManualResetEvent(false);

            public bool Visit(Cookie cookie, int count, int total, ref bool deleteCookie)
            {
                cookies.Add(new Tuple<string, string, string, string, string, string>(cookie.Domain, cookie.Name, cookie.Value, cookie.Creation.ToString("yyyyMMddHHmmss"), (cookie.Expires != null ? cookie.Expires.Value.ToString("yyyyMMddHHmmss") : ""), cookie.Path));

                if (count == total - 1)
                    gotAllCookies.Set();

                return true;
            }

            public void WaitForAllCookies()
            {
                gotAllCookies.WaitOne(2000); //otherwise stack here
            }

            public IEnumerable<Tuple<string, string, string, string, string, string>> NamesValues
            {
                get { return cookies; }
            }

            public void Dispose()
            {

            }
        }

    }
}
