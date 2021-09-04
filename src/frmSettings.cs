using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mailbox_desktop
{
    public partial class frmSettings : Form
    {
        public frmSettings(bool isNew)
        {
            InitializeComponent();

            if (!isNew)
                addform2tabpage(new frmSettingsWebsites(), tab.TabPages[0]);
            else
            {
                tab.TabPages.Remove(tab.TabPages[0]);
                addform2tabpage(new frmSettingsGeneral(isNew), tab.TabPages[0]);
                return;
            }

            addform2tabpage(new frmSettingsGeneral(isNew), tab.TabPages[1]);
        }

        internal void addform2tabpage(Form frm, TabPage tab)
        {
            frm.TopLevel = false;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            tab.Controls.Add(frm);
        }
    }
}
