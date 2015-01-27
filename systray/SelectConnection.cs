using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ViperClient
{
    public partial class SelectConnection : Form
    {
        public string PathToConfigFile;
        public string ConnectionName;

        public SelectConnection()
        {
            InitializeComponent();
            this.AllowDrop = true;
        }

        private void SelectConnection_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            this.PathToConfigFile = this.GetFirstConfigFound(files);

            panelAddTunnelConfig.Visible = true;
            // change image on display
            this.pictureBox1.Image = (Image)Properties.Resources.ok_green;
            this.pictureBox1.Refresh();

            label1.Text = "Give this connection a name, any name you like and type it in the box bellow.";

            // name the connection
            string name = Path.GetFileNameWithoutExtension(this.PathToConfigFile);
            tbConnectionName.Text = name;
            btnAddProvider.Focus();
        }

        private string GetFirstConfigFound(string []files)
        {
            string retval = string.Empty;
            foreach (string file in files)
            {
                //MessageBox.Show("Extension: " + Path.GetExtension(file));
                if (".ovpn" == Path.GetExtension(file))
                {
                    retval = file;
                }
            }
            return retval;
        }

        private void SelectConnection_DragEnter(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            // check if load includes valid config file
            string found = this.GetFirstConfigFound(files);
            if (string.Empty != found)
            {
                // cofirm that we can take the load
                e.Effect = DragDropEffects.Copy;
                // change image on display
                this.pictureBox1.Image = (Image)Properties.Resources.ovpn_green;
                this.pictureBox1.Refresh();
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void SelectConnection_DragLeave(object sender, EventArgs e)
        {
            // change image on display
            this.pictureBox1.Image = (Image)Properties.Resources.ovpn_grey;
            this.pictureBox1.Refresh();
        }

        private void btnAddProvider_Click(object sender, EventArgs e)
        {
            this.ConnectionName = tbConnectionName.Text;

            if (ViperClient.Tools.AddConnection(this.ConnectionName, PathToConfigFile))
            {
                // hide controls to add connections 
                panelAddTunnelConfig.Visible = false;

                // restore visual fedback
                this.pictureBox1.Image = (Image)Properties.Resources.ovpn_grey;
                this.pictureBox1.Refresh();
            }

            this.Hide();
        }

        private void SelectConnection_Load(object sender, EventArgs e)
        {
            this.Location = Properties.Settings.Default.AddFormPosition;
        }

        private void SelectConnection_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.AddFormPosition = this.Location;
            Properties.Settings.Default.Save();
        }
    }
}
