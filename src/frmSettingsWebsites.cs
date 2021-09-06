using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace mailbox_desktop
{
    public partial class frmSettingsWebsites : Form
    {
        public event EventHandler<bool> IsDirty; 

        public frmSettingsWebsites()
        {
            InitializeComponent();

            if (File.Exists(General.configPath))
                FillList();
        }

        internal void FillList()
        {
            string prev = txtTabTitle.Text;

            ClearForm();

            //read the config file - dont use the var
            General.cfg = XmlHelper.FromXmlFile<ConfigApp>(General.configPath);

            lst.DataSource = null;
            lst.Items.Clear();

            lst.DisplayMember = "title";
            lst.DataSource = General.cfg.wList.OrderBy(x => x.title).ToList();

            //if is coming from save
            if (!string.IsNullOrEmpty(prev))
                lst.SelectedIndex = lst.FindStringExact(prev);

        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst.SelectedItem == null)
                return;

            Class2Form((WebsiteDetail)lst.SelectedItem);
        }

        internal void Class2Form(WebsiteDetail item)
        {
            txtTabTitle.Text = item.title;
            txtWebsite.Text = item.url;
            txtCookieJarName.Text = item.cookiesJar;
            txtNotifKeyword.Text = item.notificationKeyword;
            chkNotifShowWindow.Checked = item.notificationShowWindow;
            txtNotifIconFilename.Text = item.notificationIcon;
            chkDontShowStartup.Checked = item.noAtStartup;
        }

        internal void ClearForm()
        {
            txtTabTitle.Text = "";
            txtWebsite.Text = "";
            txtCookieJarName.Text = "";
            txtNotifKeyword.Text = "";
            chkNotifShowWindow.Checked = false;
            txtNotifIconFilename.Text = "";
            chkDontShowStartup.Checked = false;
        }

        private void btnAddnew_Click(object sender, EventArgs e)
        {
            if (btnAddnew.Text == "cancel")
            {
                lst.Enabled = true;

                btnAddnew.Text = "add new";

                //advise fillist dont autoselect
                txtTabTitle.Text = string.Empty;

                FillList();
            }
            else
            { // ADD NEW
                btnAddnew.Text = "cancel";

                lst.Enabled = false;

                lst.SelectedItem = null;

                ClearForm();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            txtTabTitle.Text = txtTabTitle.Text.Trim();
            txtCookieJarName.Text = General.MakeSafeFilename(txtCookieJarName.Text.Trim());
            txtNotifIconFilename.Text = General.MakeSafeFilename(txtNotifIconFilename.Text.Trim());
            txtWebsite.Text = txtWebsite.Text.Trim();

            if (string.IsNullOrEmpty(txtTabTitle.Text) || string.IsNullOrEmpty(txtCookieJarName.Text) || string.IsNullOrEmpty(txtWebsite.Text))
            {
                 MessageBox.Show("Title / CookieJAR / Website fields are mandatory, operation aborted!");
                 return;
            }
            
            if (lst.SelectedItem == null)
            { //add new
                if (General.cfg != null)
                {
                    int validation = General.cfg.wList.Where(x => x.title.Equals(txtTabTitle.Text, StringComparison.OrdinalIgnoreCase)).Count();

                    if (validation > 0)
                    {
                        MessageBox.Show("Title already exists, operation aborted!");
                        return;
                    }
                }
                else
                    General.cfg = new ConfigApp();

                General.cfg.wList.Add(new WebsiteDetail()
                {
                    cookiesJar = txtCookieJarName.Text,
                    noAtStartup = chkDontShowStartup.Checked,
                    notificationIcon = txtNotifIconFilename.Text,
                    notificationKeyword = txtNotifKeyword.Text.Trim(),
                    notificationShowWindow = chkNotifShowWindow.Checked,
                    title = txtTabTitle.Text,
                    url = txtWebsite.Text
                });
            }
            else
            {
                //WARN - duplication is not prevented

                WebsiteDetail x = General.cfg.wList.Where(y => y.title.Equals((lst.SelectedItem as WebsiteDetail).title)).FirstOrDefault();

                x.cookiesJar = txtCookieJarName.Text;
                x.noAtStartup = chkDontShowStartup.Checked;
                x.notificationIcon = txtNotifIconFilename.Text;
                x.notificationKeyword = txtNotifKeyword.Text.Trim();
                x.notificationShowWindow = chkNotifShowWindow.Checked;
                x.title = txtTabTitle.Text;
                x.url = txtWebsite.Text;

            }

            //save
            XmlHelper.ToXmlFile(General.cfg, General.configPath);

         
            //signalize parent form 
            SetIsDirty();

            lst.Enabled = true;
            btnAddnew.Text = "add new";

            FillList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lst.SelectedItem == null)
            {
                //MessageBox.Show("Please select an item, operation aborted!");
                return;
            }
            if (MessageBox.Show("Delete the selected item ?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) != System.Windows.Forms.DialogResult.Yes)
                return;

            General.cfg.wList.Remove(General.cfg.wList.Where(x => x.title.Equals((lst.SelectedValue as WebsiteDetail).title, StringComparison.OrdinalIgnoreCase)).First());

            //save
            XmlHelper.ToXmlFile(General.cfg, General.configPath);

            //advise fillist dont autoselect
            txtTabTitle.Text = string.Empty;

            FillList();
        }

        private void SetIsDirty()
        {
            if (IsDirty!=null) 
                IsDirty.Invoke(this, true);
        }

    }
}
