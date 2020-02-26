using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mailbox_desktop
{
    public partial class frmCookies : Form
    {
        public frmCookies(IEnumerable<Tuple<string, string, string, string, string, string>> cookiesList)
        {
            InitializeComponent();

            var result = cookiesList.OrderBy(x => x.Item1).ToList();

            var d = from x in result select new ListViewItem(new string[] { x.Item1, x.Item2, x.Item3, x.Item4, x.Item5, x.Item6 });

            lstv.Items.AddRange(d.ToArray());

            if (lstv.Items.Count == 0)
                lstv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            else
                lstv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            //cookie name
            lstv.Columns[2].Width = 200;

            this.Text = "Cookies : " + lstv.Items.Count;

            read_ini();
        }

        bool ctrl_pressed;
        private void frmCookies_FormClosing(object sender, FormClosingEventArgs e)
        {
            ctrl_pressed = (ModifierKeys == Keys.Control);
        }

        public DialogResult ShowDialog(out List<Tuple<string, string>> del)
        {
            DialogResult dialogResult = base.ShowDialog();

            //List<Tuple<string, string>> del = null;
            if (ctrl_pressed && lstv.SelectedItems.Count > 0)
            {

                del = new List<Tuple<string, string>>();

                foreach (ListViewItem item in lstv.SelectedItems)
                {
                    del.Add(new Tuple<string, string>(item.Text + item.SubItems[5].Text, item.SubItems[1].Text));
                }
            }
            else
                del = null;
            //result = textBox1.Text;
            return dialogResult;
        }

        #region " listview functions "

        private void lstv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\x1')
            {
                CheckAllItems(lstv, lstv.SelectedItems.Count == 0);

                e.Handled = true;
            }
        }

        //https://stackoverflow.com/a/19673145
        public void CheckAllItems(ListView lvw, bool check)
        {
            lvw.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Selected = check);
        }

        private void lstv_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            int c = e.Column;
            bool asc = (lstv.Tag == null) || (lstv.Tag.ToString() != c + "");
            var items = lstv.Items.Cast<ListViewItem>().ToList();
            var sorted = asc ? items.OrderByDescending(x => x.SubItems[c].Text).ToList() :
                                items.OrderBy(x => x.SubItems[c].Text).ToList();
            lstv.Items.Clear();
            lstv.Items.AddRange(sorted.ToArray());
            if (asc) lstv.Tag = c + ""; else lstv.Tag = null;
        }

        #endregion


        #region " exclusions context "

        internal void read_ini()
        {
            ToolStripMenuItem ts = null;

            iniParser parser = new iniParser(Application.StartupPath + "\\settings.ini");

            string exclusions = parser.GetSetting("cookies", "exclude");

            if (exclusions == null)
            {
                contextLSTV.Items.RemoveAt(0);
                return;
            }

            exclusions = exclusions.ToLower();

            string[] exclusion_arr = exclusions.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in exclusion_arr)
            {
                ts = new ToolStripMenuItem();
                ts.Text = item;
                toolStripExclude.DropDownItems.Add(ts);
                ts.Click += new System.EventHandler(toolstripExclusion_Clicked);
            }

        }

        private void toolstripExclusion_Clicked(object sender, EventArgs e)
        {
            ToolStripMenuItem tmp = (sender as ToolStripMenuItem);
            string exclusion = tmp.Text;
            lstv.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Selected = !item.Text.Contains(exclusion));
        }

        private void lstv_MouseClick(object sender, MouseEventArgs e)
        {
            if ((lstv.SelectedItems.Count != 0) && (e.Button == MouseButtons.Right))
            {
                contextLSTV.Show(Cursor.Position);
            }
        }

        #endregion


        #region " export "

        private void toolStripExport_Click(object sender, EventArgs e)
        {
            string filepath = Application.StartupPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            lstv2csv(lstv, filepath, true);

            if (MessageBox.Show("File exported to : \r\n\r\n" + filepath + "\r\n\r\nView the file ?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", filepath));
            }

        }

        //https://stackoverflow.com/a/1008994
        public static void lstv2csv(ListView listView, string filepath, bool includeHidden)
        {
            //make header string
            StringBuilder result = new StringBuilder();
            WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listView.Columns[i].Text);

            //export data rows
            foreach (ListViewItem listItem in listView.Items)
                WriteCSVRow(result, listView.Columns.Count, i => includeHidden || listView.Columns[i].Width > 0, i => listItem.SubItems[i].Text);



            File.WriteAllText(filepath, result.ToString(), new UTF8Encoding(false)); // w/o BOM ;)
        }

        private static void WriteCSVRow(StringBuilder result, int itemsCount, Func<int, bool> isColumnNeeded, Func<int, string> columnValue)
        {
            bool isFirstTime = true;
            for (int i = 0; i < itemsCount; i++)
            {
                if (!isColumnNeeded(i))
                    continue;

                if (!isFirstTime)
                    result.Append("|");

                isFirstTime = false;

                result.Append(String.Format("\"{0}\"", columnValue(i)));
            }
            result.AppendLine();
        }

#endregion

    }
}
