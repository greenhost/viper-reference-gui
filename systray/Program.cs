using System;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Policy;

namespace ViperClient
{

    public class SysTrayApp : Form
    {
        [STAThread]
        public static void Main()
        {
            Application.Run(new SysTrayApp());
        }

        private NotifyIcon icon;
        private Button button1;
        private Panel panel1;
        private Button btnConnect;
        private Label lblLastConnected;
        private Panel panel2;
        private WebBrowser webBox;
        private ListBox lbSelectProvider;
        private Button button2;
        private LinkLabel lnkChecksum;
        private ToolTip tipChecksum;
        private System.ComponentModel.IContainer components;
        private ContextMenu menu;

        public void OnConnect(object sender, EventArgs e)
        {
        }

        public void OnDisconnect(object sender, EventArgs e)
        {
        }

        public void OnConfigure(object sender, EventArgs e)
        {
        }

        public SysTrayApp()
        {
            InitializeComponent();

            menu = new ContextMenu();

            menu.MenuItems.Add("Exit", new EventHandler(this.OnExit) );
            //menu.MenuItems.Add("Connect to VPN...", new EventHandler(this.OnConnect));
            //menu.MenuItems.Add("Disconnect...", new EventHandler(this.OnDisconnect));
            //menu.MenuItems.Add("Configure...", new EventHandler(this.OnConfigure));

            icon = new NotifyIcon();
            icon.Text = "Not connected";
            icon.Icon = ViperClient.Properties.Resources.offline;
            icon.Click += new System.EventHandler(this.OnSystrayClick);

            //wndSelect = new SelectConnection();

            icon.ContextMenu = menu;
            icon.Visible = true;

            this.RefreshListOfConnections();

            webBox.Url = new Uri("http://localhost:8088/");
        }

        protected void RefreshListOfConnections() {
            lbSelectProvider.Items.Clear();
            string[] conns = ViperClient.Tools.GetConnectionNames();
            foreach(string c in conns) {
                lbSelectProvider.Items.Add(c);
            }
        }


        private void OnSystrayClick(object Sender, EventArgs e)
        {
            this.Show();
        }

        protected override void OnLoad(EventArgs e)
        {
            //Visible = false;
            ShowInTaskbar = false;

            //wndSelect.Show();
            base.OnLoad(e);
        }



        public void OnExit(object sender, EventArgs e)
        {
            // close all forms
            this.Close();
            System.Environment.Exit(0);

            if (System.Windows.Forms.Application.MessageLoop)
            {
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                System.Environment.Exit(1);
            }
        }



        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                icon.Dispose();
            }
            base.Dispose(isDisposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbSelectProvider = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panelSelectTunnel = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lnkChecksum = new System.Windows.Forms.LinkLabel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblLastConnected = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.webBox = new System.Windows.Forms.WebBrowser();
            this.tipChecksum = new System.Windows.Forms.ToolTip(this.components);
            this.panelSelectTunnel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbSelectProvider
            // 
            this.lbSelectProvider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSelectProvider.FormattingEnabled = true;
            this.lbSelectProvider.ItemHeight = 16;
            this.lbSelectProvider.Location = new System.Drawing.Point(3, 3);
            this.lbSelectProvider.Name = "lbSelectProvider";
            this.lbSelectProvider.Size = new System.Drawing.Size(176, 68);
            this.lbSelectProvider.TabIndex = 4;
            this.lbSelectProvider.SelectedIndexChanged += new System.EventHandler(this.lbSelectProvider_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(185, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(109, 20);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "&Add tunnel...";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panelSelectTunnel
            // 
            this.panelSelectTunnel.AutoSize = true;
            this.panelSelectTunnel.Controls.Add(this.button2);
            this.panelSelectTunnel.Controls.Add(this.button1);
            this.panelSelectTunnel.Controls.Add(this.lbSelectProvider);
            this.panelSelectTunnel.Controls.Add(this.btnAdd);
            this.panelSelectTunnel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSelectTunnel.Location = new System.Drawing.Point(0, 0);
            this.panelSelectTunnel.Name = "panelSelectTunnel";
            this.panelSelectTunnel.Size = new System.Drawing.Size(306, 74);
            this.panelSelectTunnel.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(186, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 20);
            this.button2.TabIndex = 6;
            this.button2.Text = "&Options";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(185, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 20);
            this.button1.TabIndex = 5;
            this.button1.Text = "&Remove";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lnkChecksum);
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Controls.Add(this.lblLastConnected);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 74);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(306, 57);
            this.panel1.TabIndex = 4;
            // 
            // lnkChecksum
            // 
            this.lnkChecksum.AutoSize = true;
            this.lnkChecksum.Location = new System.Drawing.Point(12, 10);
            this.lnkChecksum.Name = "lnkChecksum";
            this.lnkChecksum.Size = new System.Drawing.Size(56, 13);
            this.lnkChecksum.TabIndex = 3;
            this.lnkChecksum.TabStop = true;
            this.lnkChecksum.Text = "checksum";
            this.lnkChecksum.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lnkChecksum_MouseClick);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(185, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(109, 37);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "&Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblLastConnected
            // 
            this.lblLastConnected.AutoSize = true;
            this.lblLastConnected.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastConnected.Location = new System.Drawing.Point(13, 35);
            this.lblLastConnected.Name = "lblLastConnected";
            this.lblLastConnected.Size = new System.Drawing.Size(117, 12);
            this.lblLastConnected.TabIndex = 1;
            this.lblLastConnected.Text = "Last connected: 2 days ago";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.webBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 131);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(306, 296);
            this.panel2.TabIndex = 5;
            // 
            // webBox
            // 
            this.webBox.AllowNavigation = false;
            this.webBox.AllowWebBrowserDrop = false;
            this.webBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBox.IsWebBrowserContextMenuEnabled = false;
            this.webBox.Location = new System.Drawing.Point(0, 0);
            this.webBox.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBox.Name = "webBox";
            this.webBox.ScriptErrorsSuppressed = true;
            this.webBox.ScrollBarsEnabled = false;
            this.webBox.Size = new System.Drawing.Size(306, 296);
            this.webBox.TabIndex = 0;
            // 
            // SysTrayApp
            // 
            this.ClientSize = new System.Drawing.Size(306, 427);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelSelectTunnel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SysTrayApp";
            this.Text = "PolyTunnel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SysTrayApp_FormClosing);
            this.panelSelectTunnel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Button btnAdd;
        private Panel panelSelectTunnel;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SelectConnection frmAddConnection = new SelectConnection();
            frmAddConnection.ShowDialog();

            this.RefreshListOfConnections();
            // ... select the one we just added as current
            lbSelectProvider.SelectedItem = frmAddConnection.ConnectionName;

            frmAddConnection.Close();
        }

        private void SysTrayApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            // minimize on close
            e.Cancel = true;
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conn = lbSelectProvider.SelectedItem.ToString();
            if (DialogResult.Yes == MessageBox.Show("This will permanently delete the connection " + conn + ". Are you sure?", "Do you want to delete?", MessageBoxButtons.YesNoCancel))
            {
                ViperClient.Tools.RemoveConnection( conn );
                this.RefreshListOfConnections();
            }
        }

        private void lbSelectProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            string conn = lbSelectProvider.SelectedItem.ToString();

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string conn = lbSelectProvider.SelectedItem.ToString();
            string cfg = ViperClient.Tools.GetConfigFromConnectionName(conn);
            string log = ViperClient.Tools.GetLogFromConnectionName(conn);
            Api api = new ViperClient.Api();
            api.OpenTunnel(cfg, log);
        }

        private void lnkChecksum_MouseClick(object sender, MouseEventArgs e)
        {
            tipChecksum.ToolTipIcon = ToolTipIcon.Info;
            tipChecksum.IsBalloon = true;
            tipChecksum.ShowAlways = true;

            string hash = ViperClient.Tools.GetConnectionFingerprint(lbSelectProvider.SelectedItem.ToString());
            string caption = "SHA256: " + hash;

            tipChecksum.SetToolTip(lnkChecksum, caption);
        }

    } // class

} // ns



