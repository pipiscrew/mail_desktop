namespace mailbox_desktop
{
    partial class frmSettingsWebsites
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lst = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkDontShowStartup = new System.Windows.Forms.CheckBox();
            this.chkNotifShowWindow = new System.Windows.Forms.CheckBox();
            this.txtNotifKeyword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNotifIconFilename = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCookieJarName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTabTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWebsite = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAddnew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lst
            // 
            this.lst.FormattingEnabled = true;
            this.lst.ItemHeight = 15;
            this.lst.Location = new System.Drawing.Point(13, 13);
            this.lst.Name = "lst";
            this.lst.Size = new System.Drawing.Size(194, 379);
            this.lst.TabIndex = 3;
            this.lst.SelectedIndexChanged += new System.EventHandler(this.lst_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDontShowStartup);
            this.groupBox1.Controls.Add(this.chkNotifShowWindow);
            this.groupBox1.Controls.Add(this.txtNotifKeyword);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNotifIconFilename);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCookieJarName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTabTitle);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtWebsite);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(229, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 381);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " details :";
            // 
            // chkDontShowStartup
            // 
            this.chkDontShowStartup.Location = new System.Drawing.Point(28, 350);
            this.chkDontShowStartup.Name = "chkDontShowStartup";
            this.chkDontShowStartup.Size = new System.Drawing.Size(352, 25);
            this.chkDontShowStartup.TabIndex = 6;
            this.chkDontShowStartup.Text = "don\'t open the website when application starts";
            this.toolTip.SetToolTip(this.chkDontShowStartup, "restrict the specific website to open when application starts, the user then is a" +
        "ble to navigate from tab context menu by \'open\' option");
            this.chkDontShowStartup.UseVisualStyleBackColor = true;
            // 
            // chkNotifShowWindow
            // 
            this.chkNotifShowWindow.Location = new System.Drawing.Point(52, 252);
            this.chkNotifShowWindow.Name = "chkNotifShowWindow";
            this.chkNotifShowWindow.Size = new System.Drawing.Size(328, 25);
            this.chkNotifShowWindow.TabIndex = 4;
            this.chkNotifShowWindow.Text = "when notification occurring, show window";
            this.toolTip.SetToolTip(this.chkNotifShowWindow, "when the notfication keyword occurred, show the window even is minimized");
            this.chkNotifShowWindow.UseVisualStyleBackColor = true;
            // 
            // txtNotifKeyword
            // 
            this.txtNotifKeyword.Location = new System.Drawing.Point(27, 223);
            this.txtNotifKeyword.Name = "txtNotifKeyword";
            this.txtNotifKeyword.Size = new System.Drawing.Size(330, 23);
            this.txtNotifKeyword.TabIndex = 3;
            this.toolTip.SetToolTip(this.txtNotifKeyword, "when a update made to website, change the title for example Inbox(1), for this si" +
        "tutation use  (");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(203, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "notification icon filename :";
            // 
            // txtNotifIconFilename
            // 
            this.txtNotifIconFilename.Location = new System.Drawing.Point(28, 304);
            this.txtNotifIconFilename.Name = "txtNotifIconFilename";
            this.txtNotifIconFilename.Size = new System.Drawing.Size(331, 23);
            this.txtNotifIconFilename.TabIndex = 5;
            this.toolTip.SetToolTip(this.txtNotifIconFilename, "is a .ico file exist near application executable, used for notification tray");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "notification keyword :";
            // 
            // txtCookieJarName
            // 
            this.txtCookieJarName.Location = new System.Drawing.Point(27, 166);
            this.txtCookieJarName.Name = "txtCookieJarName";
            this.txtCookieJarName.Size = new System.Drawing.Size(331, 23);
            this.txtCookieJarName.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtCookieJarName, "the folder name inside cache folder is near application exe");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "cookie jar name :";
            // 
            // txtTabTitle
            // 
            this.txtTabTitle.Location = new System.Drawing.Point(27, 51);
            this.txtTabTitle.Name = "txtTabTitle";
            this.txtTabTitle.Size = new System.Drawing.Size(330, 23);
            this.txtTabTitle.TabIndex = 0;
            this.toolTip.SetToolTip(this.txtTabTitle, "the caption will appear to tab caption");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "tab title :";
            // 
            // txtWebsite
            // 
            this.txtWebsite.Location = new System.Drawing.Point(27, 108);
            this.txtWebsite.Name = "txtWebsite";
            this.txtWebsite.Size = new System.Drawing.Size(330, 23);
            this.txtWebsite.TabIndex = 1;
            this.toolTip.SetToolTip(this.txtWebsite, "the actual URL will be shown");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "website :";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(634, 47);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 28);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddnew
            // 
            this.btnAddnew.Location = new System.Drawing.Point(634, 13);
            this.btnAddnew.Name = "btnAddnew";
            this.btnAddnew.Size = new System.Drawing.Size(91, 28);
            this.btnAddnew.TabIndex = 0;
            this.btnAddnew.Text = "add new";
            this.btnAddnew.UseVisualStyleBackColor = true;
            this.btnAddnew.Click += new System.EventHandler(this.btnAddnew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(634, 81);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(91, 28);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 10000;
            this.toolTip.AutoPopDelay = 15000;
            this.toolTip.InitialDelay = 200;
            this.toolTip.ReshowDelay = 200;
            this.toolTip.ShowAlways = true;
            // 
            // frmSettingsWebsites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 404);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddnew);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lst);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Name = "frmSettingsWebsites";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lst;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkDontShowStartup;
        private System.Windows.Forms.CheckBox chkNotifShowWindow;
        private System.Windows.Forms.TextBox txtNotifKeyword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNotifIconFilename;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCookieJarName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTabTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWebsite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAddnew;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ToolTip toolTip;

    }
}