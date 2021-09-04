using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace mailbox_desktop
{
   

    public static class General
    {
        public static ConfigApp cfg;
        public static string configPath; 

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


        public static string MakeSafeFilename(string filename)
        {
            Regex pattern = new Regex("[" + string.Join(",", Path.GetInvalidFileNameChars()) + "]");

            return pattern.Replace(filename, "_");
        }

        public static bool ToBool(this object value)
        {
            bool result = false;
            if (value != null)
                bool.TryParse(value.ToString(), out result);

            return result;
        }
    }
}
