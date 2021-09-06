using System.Windows.Forms;

namespace mailbox_desktop
{
    public partial class frmSettings : Form
    {
        internal static bool isDirty = false;

        public frmSettings(bool isNew)
        {
            InitializeComponent();

            if (!isNew)
            {
                frmSettingsWebsites x = new frmSettingsWebsites();
                x.IsDirty += x_IsDirty;
                addform2tabpage(x, tab.TabPages[0]);

                frmSettingsGeneral y = new frmSettingsGeneral(isNew);
                y.IsDirty += x_IsDirty;
                addform2tabpage(y, tab.TabPages[1]);
            }
            else
            {
                tab.TabPages.Remove(tab.TabPages[0]);
                addform2tabpage(new frmSettingsGeneral(isNew), tab.TabPages[0]);
                return;
            }

        }

        internal void x_IsDirty(object sender, bool e)
        {
            isDirty = e;
        }

        internal void addform2tabpage(Form frm, TabPage tab)
        {
            frm.TopLevel = false;
            frm.Visible = true;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            tab.Controls.Add(frm);
        }

        private void frmSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isDirty)
            {
                MessageBox.Show("Application restart required, so changes take place. Shutting down, please start manually the application.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }
    }
}
