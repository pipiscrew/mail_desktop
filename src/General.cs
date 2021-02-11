using System;
using System.Windows.Forms;

namespace mailbox_desktop
{
    public static class General
    {
        public static void Copy2Clipboard(string val)
        {
            try
            {
                Clipboard.Clear();
                Clipboard.SetDataObject(val, true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName);
            }
        }


        public static string GetFromClipboard()
        {
            try
            {
                return Clipboard.GetText().Trim();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, Application.ProductName);
                return "";
            }
        }
    }
}
