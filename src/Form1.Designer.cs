namespace mailbox_desktop
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripRestart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripOpenInContainer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripOpenInGlobal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCopyURL = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCookies = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.trayContext.SuspendLayout();
            this.contextTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(954, 541);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseClick);
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayContext;
            this.trayIcon.Visible = true;
            this.trayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseClick);
            // 
            // trayContext
            // 
            this.trayContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem3,
            this.toolStripRestart,
            this.toolStripSeparator1,
            this.toolStripSettings,
            this.toolStripMenuItem2});
            this.trayContext.Name = "trayContext";
            this.trayContext.Size = new System.Drawing.Size(153, 142);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItem1.Text = "show";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItem3.Text = "set all read";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripRestart
            // 
            this.toolStripRestart.Name = "toolStripRestart";
            this.toolStripRestart.Size = new System.Drawing.Size(133, 22);
            this.toolStripRestart.Text = "restart with";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItem2.Text = "exit";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // contextTab
            // 
            this.contextTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripOpenInContainer,
            this.toolStripOpenInGlobal,
            this.toolStripSeparator3,
            this.toolStripMenuItem6,
            this.toolStripCopyURL,
            this.toolStripMenuItem5,
            this.toolStripCookies,
            this.toolStripSeparator2,
            this.toolStripOpen,
            this.toolStripMenuItem7,
            this.toolStripMenuItem4});
            this.contextTab.Name = "contextTab";
            this.contextTab.Size = new System.Drawing.Size(162, 214);
            // 
            // toolStripOpenInContainer
            // 
            this.toolStripOpenInContainer.Image = global::mailbox_desktop.Properties.Resources.open_in_container;
            this.toolStripOpenInContainer.Name = "toolStripOpenInContainer";
            this.toolStripOpenInContainer.Size = new System.Drawing.Size(161, 22);
            this.toolStripOpenInContainer.Text = "open w/ cookies";
            this.toolStripOpenInContainer.Click += new System.EventHandler(this.toolStripOpenInContainer_Click);
            // 
            // toolStripOpenInGlobal
            // 
            this.toolStripOpenInGlobal.Image = global::mailbox_desktop.Properties.Resources.open_in_global;
            this.toolStripOpenInGlobal.Name = "toolStripOpenInGlobal";
            this.toolStripOpenInGlobal.Size = new System.Drawing.Size(161, 22);
            this.toolStripOpenInGlobal.Text = "open";
            this.toolStripOpenInGlobal.Click += new System.EventHandler(this.toolStripOpenInGlobal_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(158, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Image = global::mailbox_desktop.Properties.Resources.refresh;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItem6.Text = "refresh";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripCopyURL
            // 
            this.toolStripCopyURL.Image = global::mailbox_desktop.Properties.Resources.copy;
            this.toolStripCopyURL.Name = "toolStripCopyURL";
            this.toolStripCopyURL.Size = new System.Drawing.Size(161, 22);
            this.toolStripCopyURL.Text = "copy url";
            this.toolStripCopyURL.Click += new System.EventHandler(this.toolStripCopyURL_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Image = global::mailbox_desktop.Properties.Resources.devtools;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItem5.Text = "developer tools";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripCookies
            // 
            this.toolStripCookies.Image = global::mailbox_desktop.Properties.Resources.cookies;
            this.toolStripCookies.Name = "toolStripCookies";
            this.toolStripCookies.Size = new System.Drawing.Size(161, 22);
            this.toolStripCookies.Text = "cookies";
            this.toolStripCookies.Click += new System.EventHandler(this.toolStripCookies_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(158, 6);
            // 
            // toolStripOpen
            // 
            this.toolStripOpen.Image = global::mailbox_desktop.Properties.Resources.open;
            this.toolStripOpen.Name = "toolStripOpen";
            this.toolStripOpen.Size = new System.Drawing.Size(161, 22);
            this.toolStripOpen.Text = "open";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItem7.Text = "close right tabs";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Image = global::mailbox_desktop.Properties.Resources.close;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItem4.Text = "close tab";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripSettings
            // 
            this.toolStripSettings.Name = "toolStripSettings";
            this.toolStripSettings.Size = new System.Drawing.Size(152, 22);
            this.toolStripSettings.Text = "settings";
            this.toolStripSettings.Click += new System.EventHandler(this.toolStripSettings_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 541);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.trayContext.ResumeLayout(false);
            this.contextTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayContext;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextTab;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripRestart;
        private System.Windows.Forms.ToolStripMenuItem toolStripCopyURL;
        private System.Windows.Forms.ToolStripMenuItem toolStripCookies;
        private System.Windows.Forms.ToolStripMenuItem toolStripOpenInContainer;
        private System.Windows.Forms.ToolStripMenuItem toolStripOpenInGlobal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripSettings;
    }
}

