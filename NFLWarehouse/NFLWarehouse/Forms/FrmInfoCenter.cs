using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using NFLWarehouse.Classes;

namespace NFLWarehouse.Forms
{
    public partial class FrmInfoCenter : Form
    {
        // Workstation Configuration
        private string toolName = "NFL Warehouse";
        private string stationName = "InfoCenter";
        private string instruction1 = "Scan Tote Number";
        private string instruction2 = "Scan Location";
        private string hostname = Dns.GetHostName();
        private Color titleColor = Color.White;
        private System.Windows.Forms.Form commingFrom;
        // Database Connection
        private NFLWarehouseDB nflwarehouseDB;
        // Messages
        //string message1 = "Tool shutting down.";
        string message2 = "Connecting to NFL databse, please wait...";
        string message3 = "Tote name {0} has invalid nomeclature, please double check and try again.";
        string message4 = "data of {0} retrieved.";


        public FrmInfoCenter()
        {
            InitializeComponent();
        }


        #region Actions
        public void GetInfo(string Tote)
        {
            int toteid = nflwarehouseDB.GetToteId(Tote);
            labelTote.Text = Tote;
            labelLocation.Text = nflwarehouseDB.GetLocation(Tote);
            labelStatus.Text = nflwarehouseDB.GetStatusName(nflwarehouseDB.GetStatusId(toteid));
            labelCreationDate.Text = nflwarehouseDB.GetCreationDate(toteid);
            labelCreatedBy.Text = nflwarehouseDB.GetCreatedBy(toteid);
            MsgTypes.printme(MsgTypes.msg_success, String.Format(message4, Tote), this);
        }
        #endregion 


        #region Cosmetics
        public void CustomizeTitle()
        {
            this.labelProcessName.ForeColor = titleColor;
        }
        #endregion


        #region Controls
        // Standard
        private async void FrmInfoCenter_Load(object sender, EventArgs e)
        {
            initForm();
            MsgTypes.printme(MsgTypes.msg_success, message2, this);
            nflwarehouseDB = new NFLWarehouseDB(this);
            blockForm();
            await nflwarehouseDB.OpenAsync();
        }
        private void pictureWindowClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pictureWindowNormal_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
        private void pictureWindowMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void textBoxTote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetInfo(GetTote());
            }
        }
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            GetInfo(GetTote());
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        // Customize
        public void initForm()
        {
            this.Location = new Point((int)(Screen.PrimaryScreen.Bounds.Width * .5 - this.Width * .5),
                                      (int)(Screen.PrimaryScreen.Bounds.Height * .5 - this.Height * .5));
            CustomizeTitle();
            this.Text = toolName + " " + stationName;
            labelToolName.Text = this.toolName;
            labelProcessName.Text = this.stationName;
            labelInstruction1.Text = this.instruction1;
            labelInstruction2.Text = this.instruction2;
            labelHostName.Text = "Working from: " + this.hostname;
            if (!Tools.isDebugMode())
            {
                labelVersion.Text = "verison: " +
                System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            else
            {
                labelVersion.Text = "verison: Debug";
            }
        }
        private string GetTote()
        {
            return textBoxTote.Text;
        }
        public void ClearForm()
        {
            textBoxTote.Clear();
            textBoxLocation.Clear();

            labelTote.Text = "";
            labelLocation.Text = "";
            labelStatus.Text = "";
            labelCreationDate.Text ="";
            labelCreatedBy.Text ="";
        }
        public void blockForm()
        {
            textBoxTote.Enabled = false;
            textBoxLocation.Enabled = false;
            buttonConfirm.Enabled = false;
            buttonClear.Enabled = false;
            this.UseWaitCursor = true;
        }
        public void releaseForm()
        {
            textBoxTote.Enabled = true;
            textBoxLocation.Enabled = true;
            buttonConfirm.Enabled = true;
            buttonClear.Enabled = true;
            this.UseWaitCursor = false;
        }



        #endregion

      

        #region Window Movement
        Boolean Moveform;
        Point Moveform_MousePosition;
        private void pictureBoxLogo_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Moveform = false;
                this.Cursor = Cursors.Default;
            }
        }
        private void labelToolName_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Moveform = false;
                this.Cursor = Cursors.Default;
            }
        }
        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Moveform = false;
                this.Cursor = Cursors.Default;
            }
        }
        private void pictureBoxLogo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Moveform = true;
                this.Cursor = Cursors.NoMove2D;
                Moveform_MousePosition = e.Location;
            }
        }
        private void labelToolName_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Moveform = true;
                this.Cursor = Cursors.NoMove2D;
                Moveform_MousePosition = e.Location;
            }
        }
        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Moveform = true;
                this.Cursor = Cursors.NoMove2D;
                Moveform_MousePosition = e.Location;
            }
        }
        private void pictureBoxLogo_MouseMove(object sender, MouseEventArgs e)
        {
            if (Moveform)
            {
                this.Location = new Point(this.Location.X + (e.Location.X - Moveform_MousePosition.X),
                                          this.Location.Y + (e.Location.Y - Moveform_MousePosition.Y));
            }
        }
        private void labelToolName_MouseMove(object sender, MouseEventArgs e)
        {
            if (Moveform)
            {
                this.Location = new Point(this.Location.X + (e.Location.X - Moveform_MousePosition.X),
                                          this.Location.Y + (e.Location.Y - Moveform_MousePosition.Y));
            }
        }
        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (Moveform)
            {
                this.Location = new Point(this.Location.X + (e.Location.X - Moveform_MousePosition.X),
                                          this.Location.Y + (e.Location.Y - Moveform_MousePosition.Y));
            }
        }
        #endregion

    }
}
