using System;
using System.IO;
using System.Windows.Forms;

namespace mailbox_desktop
{
    public partial class frmSettingsGeneral : Form
    {
        public event EventHandler<bool> IsDirty;

        internal bool isNew;
        public frmSettingsGeneral(bool isNew)
        {
            InitializeComponent();

            this.isNew = isNew;

            if (isNew)
            {
                chkProxy.Checked = chkGPU.Checked = chkCanvas.Checked = chkWebGL.Checked = true;
                txtCookies.Text = "google;messenger;linkedin";
            }

            if (File.Exists(General.configPath))
                Fill();
        }

        internal void Fill()
        {
            //read the config file
            General.cfg = XmlHelper.FromXmlFile<ConfigApp>(General.configPath);

            chkProxy.Checked = General.cfg.g.noproxyserver.ToBool();
            chkGPU.Checked = General.cfg.g.disablegpu.ToBool();
            chkCanvas.Checked = General.cfg.g.disablecanvas.ToBool();
            chkWebGL.Checked = General.cfg.g.disablewebgl.ToBool();
            chkWebRTC.Checked = General.cfg.g.enableWebRTC.ToBool();
            txtExternalBrowser.Text = General.cfg.g.externalbrowser;
            txtAgent.Text = General.cfg.g.agent;
            txtCookies.Text = General.cfg.g.cookies;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (General.cfg == null)
                General.cfg = new ConfigApp();

            General.cfg.g = new GeneralSettings()
            {
                noproxyserver = chkProxy.Checked,
                disablegpu = chkGPU.Checked,
                disablecanvas = chkCanvas.Checked,
                disablewebgl = chkWebGL.Checked,
                enableWebRTC = chkWebRTC.Checked,
                externalbrowser = txtExternalBrowser.Text,
                agent = txtAgent.Text,
                cookies = txtCookies.Text
            };

            //save
            XmlHelper.ToXmlFile(General.cfg, General.configPath);

            if (!this.isNew)
            {
                IsDirty.Invoke(this, true);
            }
            else
            {
                MessageBox.Show("Please proceed at tray icon icon, right click it, select 'settings' and set the needed websites. Keep in mind on any setting change application restart required.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Parent.Parent.Parent.Dispose(); //close parent form
            }
        }


    }
}
