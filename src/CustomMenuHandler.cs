using System;
using CefSharp;
using CefSharp.WinForms;
using System.Windows.Forms;

namespace mailbox_desktop
{
    class CustomMenuHandler : IContextMenuHandler
    {

        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            // Remove any existent option using the Clear method of the model
            //
            //model.Clear();


            // Add a new item to the list using the AddItem method of the model

            if (!string.IsNullOrEmpty(parameters.LinkUrl) && parameters.LinkUrl.StartsWith("http",StringComparison.OrdinalIgnoreCase))
            {
                model.AddItem((CefMenuCommand)26504, "Copy link address");

                if (model.Count > 0)
                {
                    model.AddSeparator();
                }

                model.AddItem((CefMenuCommand)26502, "Open in new tab w/ cookies");
                model.AddItem((CefMenuCommand)26503, "Open in new tab");
                model.AddItem((CefMenuCommand)26505, "Open in default browser");
                //model.SetCommandIdAt(0, (CefMenuCommand)26502); not working?

                model.AddSeparator();
            }

            model.AddItem((CefMenuCommand)26501, "Export as PDF");

            // Add a separator
            //model.AddSeparator();
        }

        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            // React to the first ID (show dev tools method)
            // working but not useful as some pages disable right click
            //if (commandId == (CefMenuCommand)26501)
            //{
            //    browser.GetHost().ShowDevTools();
            //    return true;
            //}
            if (commandId == (CefMenuCommand)26501)
            {
                string filepath = AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                //ChromiumWebBrowser current_browser = (ChromiumWebBrowser)browserControl;
                //current_browser.PrintToPdfAsync(filepath, null);
                printToPdfCallback callback = new printToPdfCallback();
                browser.GetHost().PrintToPdf(filepath, null, callback);
                return true;
            }

            if (commandId == (CefMenuCommand)26504)
            {
                string url = clean_url(parameters.LinkUrl);

                General.Copy2Clipboard(url);
                return true;
            }

            // React to the second ID (show dev tools method)
            if (commandId == (CefMenuCommand)26502 || commandId == (CefMenuCommand)26503 || commandId == (CefMenuCommand)26505)
            {
                if (!string.IsNullOrEmpty(parameters.LinkUrl) && parameters.LinkUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    if (commandId == (CefMenuCommand)26505)
                    {
                        //System.Diagnostics.Process.Start(clean_url(parameters.LinkUrl));
                        ChromiumWebBrowser current_browser = (ChromiumWebBrowser)browserControl;
                        CefControl1 current_CEF_comtrol = (CefControl1)current_browser.Parent;
                        current_CEF_comtrol.open_to_browser(clean_url(parameters.LinkUrl));
                    }
                    else
                    {
                        ChromiumWebBrowser current_browser = (ChromiumWebBrowser)browserControl;
                        CefControl1 current_CEF_comtrol = (CefControl1)current_browser.Parent;
                        current_CEF_comtrol.open_child_tab(parameters.LinkUrl, commandId == (CefMenuCommand)26502);
                    }
                }
                //browser.GetHost().CloseDevTools();
                return true;
            }
            return false;
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {

        }

        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            return false;
        }

        class printToPdfCallback : IPrintToPdfCallback
        { //src - https://www.bountysource.com/issues/37990074-io-exception-when-trying-to-access-generated-pdf-file-immediately-after-task-completes
            public void OnPdfPrintFinished(string path, bool ok)
            {
                if (ok)
                {
                    if (MessageBox.Show("File exported to : \r\n\r\n" + path + "\r\n\r\nView the file ?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", path));
                    }
                }
                else
                    MessageBox.Show("Error occurred!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            public bool IsDisposed
            {
                get { return true; }
            }

            public void Dispose()
            {
            }
        }

        //try get the real URL from messenger shortcut
        internal string clean_url(string url)
        {

            string url_decoded = System.Net.WebUtility.UrlDecode(url);

            if (url_decoded.Contains("messenger.com/"))
            {
                int start = url_decoded.IndexOf("=");
                int end = url_decoded.LastIndexOf("&h=");

                if (start > 0 && end > 0)
                {
                    start += 1;
                    url = url_decoded.Substring(start, end - start);
                }
            }

            return url;
        }
    }
}
