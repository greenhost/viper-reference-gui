namespace ViperClient
{
    partial class SelectConnection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectConnection));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelAddTunnelConfig = new System.Windows.Forms.Panel();
            this.btnAddProvider = new System.Windows.Forms.Button();
            this.tbConnectionName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelAddTunnelConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(306, 269);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(10, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "Drag a configuration file with OVPN extension into this box to import a tunnel co" +
    "nfiguration.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(87, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.SelectConnection_DragDrop);
            // 
            // panelAddTunnelConfig
            // 
            this.panelAddTunnelConfig.Controls.Add(this.btnAddProvider);
            this.panelAddTunnelConfig.Controls.Add(this.tbConnectionName);
            this.panelAddTunnelConfig.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelAddTunnelConfig.Location = new System.Drawing.Point(0, 228);
            this.panelAddTunnelConfig.Name = "panelAddTunnelConfig";
            this.panelAddTunnelConfig.Size = new System.Drawing.Size(306, 41);
            this.panelAddTunnelConfig.TabIndex = 4;
            this.panelAddTunnelConfig.Visible = false;
            // 
            // btnAddProvider
            // 
            this.btnAddProvider.Location = new System.Drawing.Point(259, 7);
            this.btnAddProvider.Name = "btnAddProvider";
            this.btnAddProvider.Size = new System.Drawing.Size(35, 23);
            this.btnAddProvider.TabIndex = 1;
            this.btnAddProvider.Text = "+";
            this.btnAddProvider.UseVisualStyleBackColor = true;
            this.btnAddProvider.Click += new System.EventHandler(this.btnAddProvider_Click);
            // 
            // tbConnectionName
            // 
            this.tbConnectionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbConnectionName.Location = new System.Drawing.Point(13, 9);
            this.tbConnectionName.Name = "tbConnectionName";
            this.tbConnectionName.Size = new System.Drawing.Size(240, 22);
            this.tbConnectionName.TabIndex = 0;
            // 
            // SelectConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 269);
            this.Controls.Add(this.panelAddTunnelConfig);
            this.Controls.Add(this.panel1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::ViperClient.Properties.Settings.Default, "AddFormPosition", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = global::ViperClient.Properties.Settings.Default.AddFormPosition;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectConnection";
            this.Text = "Add tunnel configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectConnection_FormClosing);
            this.Load += new System.EventHandler(this.SelectConnection_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.SelectConnection_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.SelectConnection_DragEnter);
            this.DragLeave += new System.EventHandler(this.SelectConnection_DragLeave);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelAddTunnelConfig.ResumeLayout(false);
            this.panelAddTunnelConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelAddTunnelConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddProvider;
        private System.Windows.Forms.TextBox tbConnectionName;

    }
}