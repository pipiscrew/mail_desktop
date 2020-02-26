namespace mailbox_desktop
{
    partial class frmCookies
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lstv = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextLSTV = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripExclude = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripExport = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.contextLSTV.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(805, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "any selected element will be removed when user holding CTRL on form close. Use CT" +
    "RL+A to select / deselect all.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(805, 18);
            this.panel1.TabIndex = 2;
            // 
            // lstv
            // 
            this.lstv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lstv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstv.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.lstv.FullRowSelect = true;
            this.lstv.HideSelection = false;
            this.lstv.Location = new System.Drawing.Point(0, 18);
            this.lstv.Name = "lstv";
            this.lstv.Size = new System.Drawing.Size(805, 445);
            this.lstv.TabIndex = 3;
            this.lstv.UseCompatibleStateImageBehavior = false;
            this.lstv.View = System.Windows.Forms.View.Details;
            this.lstv.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstv_ColumnClick);
            this.lstv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstv_KeyPress);
            this.lstv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstv_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Domain";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Value";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Created";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Expires";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Path";
            // 
            // contextLSTV
            // 
            this.contextLSTV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripExclude,
            this.toolStripExport});
            this.contextLSTV.Name = "contextLSTV";
            this.contextLSTV.Size = new System.Drawing.Size(115, 48);
            // 
            // toolStripExclude
            // 
            this.toolStripExclude.Image = global::mailbox_desktop.Properties.Resources.exclude;
            this.toolStripExclude.Name = "toolStripExclude";
            this.toolStripExclude.Size = new System.Drawing.Size(114, 22);
            this.toolStripExclude.Text = "exclude";
            // 
            // toolStripExport
            // 
            this.toolStripExport.Image = global::mailbox_desktop.Properties.Resources.export;
            this.toolStripExport.Name = "toolStripExport";
            this.toolStripExport.Size = new System.Drawing.Size(114, 22);
            this.toolStripExport.Text = "export";
            this.toolStripExport.Click += new System.EventHandler(this.toolStripExport_Click);
            // 
            // frmCookies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 463);
            this.Controls.Add(this.lstv);
            this.Controls.Add(this.panel1);
            this.Name = "frmCookies";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tab Cookies";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCookies_FormClosing);
            this.panel1.ResumeLayout(false);
            this.contextLSTV.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lstv;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ContextMenuStrip contextLSTV;
        private System.Windows.Forms.ToolStripMenuItem toolStripExclude;
        private System.Windows.Forms.ToolStripMenuItem toolStripExport;

    }
}