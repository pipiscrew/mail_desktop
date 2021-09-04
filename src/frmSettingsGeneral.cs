using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mailbox_desktop
{
    public partial class frmSettingsGeneral : Form
    {
                internal bool isNew; 
        public frmSettingsGeneral(        bool isNew )
        {
            InitializeComponent();

            this.isNew = isNew;

            if (File.Exists(General.configPath))                
                Fill();
        }

        internal void Fill()
        {
            //read the config file - dont use the var
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (  General.cfg==null)
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

            //if (this.isNew)
            //{
            //    MessageBox.Show("Application now will restart, please right click on tray icon and set the websites you want to have", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    Application.Restart();
            //}
            //else
                this.Parent.Parent.Parent.Dispose();
        }


    }
}
