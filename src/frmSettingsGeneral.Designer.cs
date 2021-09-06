namespace mailbox_desktop
{
    partial class frmSettingsGeneral
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
            this.chkProxy = new System.Windows.Forms.CheckBox();
            this.chkGPU = new System.Windows.Forms.CheckBox();
            this.txtExternalBrowser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkCanvas = new System.Windows.Forms.CheckBox();
            this.chkWebRTC = new System.Windows.Forms.CheckBox();
            this.chkWebGL = new System.Windows.Forms.CheckBox();
            this.txtAgent = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCookies = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkProxy
            // 
            this.chkProxy.AutoSize = true;
            this.chkProxy.Location = new System.Drawing.Point(13, 13);
            this.chkProxy.Name = "chkProxy";
            this.chkProxy.Size = new System.Drawing.Size(208, 19);
            this.chkProxy.TabIndex = 0;
            this.chkProxy.Text = "don\'t use proxy by default";
            this.toolTip2.SetToolTip(this.chkProxy, "on tests disabling proxy, speed up loading time");
            this.chkProxy.UseVisualStyleBackColor = true;
            // 
            // chkGPU
            // 
            this.chkGPU.AutoSize = true;
            this.chkGPU.Location = new System.Drawing.Point(12, 38);
            this.chkGPU.Name = "chkGPU";
            this.chkGPU.Size = new System.Drawing.Size(103, 19);
            this.chkGPU.TabIndex = 1;
            this.chkGPU.Text = "disable GPU";
            this.toolTip2.SetToolTip(this.chkGPU, "video decoding and 2D rendering");
            this.chkGPU.UseVisualStyleBackColor = true;
            // 
            // txtExternalBrowser
            // 
            this.txtExternalBrowser.Location = new System.Drawing.Point(15, 178);
            this.txtExternalBrowser.Name = "txtExternalBrowser";
            this.txtExternalBrowser.Size = new System.Drawing.Size(395, 23);
            this.txtExternalBrowser.TabIndex = 3;
            this.toolTip2.SetToolTip(this.txtExternalBrowser, "when use the option \'open in default browser\' will not open it to default browser" +
        " but to external browser");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "external browser :";
            // 
            // chkCanvas
            // 
            this.chkCanvas.AutoSize = true;
            this.chkCanvas.Location = new System.Drawing.Point(12, 63);
            this.chkCanvas.Name = "chkCanvas";
            this.chkCanvas.Size = new System.Drawing.Size(124, 19);
            this.chkCanvas.TabIndex = 5;
            this.chkCanvas.Text = "disable canvas";
            this.toolTip2.SetToolTip(this.chkCanvas, "allows for dynamic, scriptable rendering of 2D shapes and bitmap images");
            this.chkCanvas.UseVisualStyleBackColor = true;
            // 
            // chkWebRTC
            // 
            this.chkWebRTC.AutoSize = true;
            this.chkWebRTC.Location = new System.Drawing.Point(12, 113);
            this.chkWebRTC.Name = "chkWebRTC";
            this.chkWebRTC.Size = new System.Drawing.Size(117, 19);
            this.chkWebRTC.TabIndex = 6;
            this.chkWebRTC.Text = "enable WebRTC";
            this.toolTip2.SetToolTip(this.chkWebRTC, "enable realtime communication of audio, video and data");
            this.chkWebRTC.UseVisualStyleBackColor = true;
            // 
            // chkWebGL
            // 
            this.chkWebGL.AutoSize = true;
            this.chkWebGL.Location = new System.Drawing.Point(12, 88);
            this.chkWebGL.Name = "chkWebGL";
            this.chkWebGL.Size = new System.Drawing.Size(117, 19);
            this.chkWebGL.TabIndex = 7;
            this.chkWebGL.Text = "disable WebGL";
            this.toolTip2.SetToolTip(this.chkWebGL, "JavaScript API for rendering interactive 2D and 3D graphics");
            this.chkWebGL.UseVisualStyleBackColor = true;
            // 
            // txtAgent
            // 
            this.txtAgent.Location = new System.Drawing.Point(15, 241);
            this.txtAgent.Name = "txtAgent";
            this.txtAgent.Size = new System.Drawing.Size(395, 23);
            this.txtAgent.TabIndex = 8;
            this.toolTip2.SetToolTip(this.txtAgent, "browser agent declaring at websites");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "browser agent :";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(334, 356);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolTip2
            // 
            this.toolTip2.AutomaticDelay = 10000;
            this.toolTip2.AutoPopDelay = 15000;
            this.toolTip2.InitialDelay = 200;
            this.toolTip2.ReshowDelay = 200;
            this.toolTip2.ShowAlways = true;
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(192, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 18);
            this.label3.TabIndex = 12;
            this.label3.Text = "fingerprint, better disable";
            this.toolTip2.SetToolTip(this.label3, "is a type of online tracking that\'s more invasive than ordinary cookie-based trac" +
        "king");
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(192, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 18);
            this.label4.TabIndex = 13;
            this.label4.Text = "fingerprint, better disable";
            this.toolTip2.SetToolTip(this.label4, "is a type of online tracking that\'s more invasive than ordinary cookie-based trac" +
        "king");
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(192, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(197, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "fingerprint, better disable";
            this.toolTip2.SetToolTip(this.label5, "is a type of online tracking that\'s more invasive than ordinary cookie-based trac" +
        "king");
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(192, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(197, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "fingerprint, better disable";
            this.toolTip2.SetToolTip(this.label6, "is a type of online tracking that\'s more invasive than ordinary cookie-based trac" +
        "king");
            // 
            // txtCookies
            // 
            this.txtCookies.Location = new System.Drawing.Point(15, 305);
            this.txtCookies.Name = "txtCookies";
            this.txtCookies.Size = new System.Drawing.Size(395, 23);
            this.txtCookies.TabIndex = 16;
            this.toolTip2.SetToolTip(this.txtCookies, "cookies domain exclusion on cookies manager when delete batch from context");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 285);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(308, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "cookies exclude (separate with semicolon) :";
            // 
            // frmSettingsGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 387);
            this.Controls.Add(this.txtCookies);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtAgent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkWebGL);
            this.Controls.Add(this.chkWebRTC);
            this.Controls.Add(this.chkCanvas);
            this.Controls.Add(this.txtExternalBrowser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkGPU);
            this.Controls.Add(this.chkProxy);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Name = "frmSettingsGeneral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSettingsGeneral";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkProxy;
        private System.Windows.Forms.CheckBox chkGPU;
        private System.Windows.Forms.TextBox txtExternalBrowser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkCanvas;
        private System.Windows.Forms.CheckBox chkWebRTC;
        private System.Windows.Forms.CheckBox chkWebGL;
        private System.Windows.Forms.TextBox txtAgent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCookies;
        private System.Windows.Forms.Label label7;
    }
}