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
    public partial class FrmScanout : Form
    {
        #region Global Variables
        // Workstation Configuration
        private string toolName = "NFL Warehouse";
        private string stationName = "Scan Out Station";
        private string instruction1 = "1 Scan Tote Number";
        private string instruction2 = "";
        private string hostname = Dns.GetHostName();
        private Color titleColor = Color.OrangeRed;
        private System.Windows.Forms.Form commingFrom;
        // Database Connection
        private NFLWarehouseDB nflwarehouseDB;
        // Messages
        //string message1 = "Tool shutting down.";
        string message2 = "Connecting to NFL databse, please wait...";
        string message3 = "Tote name {0} has invalid nomeclature, please double check and try again.";
        #endregion

        public FrmScanout()
        {
            InitializeComponent();
        }
        public FrmScanout(System.Windows.Forms.Form commingFrom)
        {
            InitializeComponent();
            this.commingFrom = commingFrom;
        }
        


        #region Logic

        #endregion

        #region Actions
        public void ActionScanout()
        {
            if (Tools.IsGoodToteName(GetTote()))
            {
                nflwarehouseDB.ReleaseTote(GetTote());
                ClearForm();
            }else
            {
                MsgTypes.printme(MsgTypes.msg_failure, String.Format(message3, GetTote()), this);
                textBoxTote.Select();
                textBoxTote.SelectAll();
            }
        }
        public void ActionSwitch()
        {
            this.Hide();
            if (commingFrom is null)
            {
                if (this.Name.Contains("ScanIn"))
                {
                    FrmScanout frm = new FrmScanout(this);
                    this.commingFrom = frm;
                    frm.Show();
                }
                else
                {
                    FrmScanIn frm = new FrmScanIn(this);
                    this.commingFrom = frm;
                    frm.Show();
                }
            }
            else
            {
                commingFrom.Show();
            }

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
        private async void FrmScanout_Load(object sender, EventArgs e)
        {
            initForm();
            MsgTypes.printme(MsgTypes.msg_success, message2, this);
            nflwarehouseDB = new NFLWarehouseDB(this);
            blockForm();
            await nflwarehouseDB.OpenAsync();
        }
        public void initForm()
        {
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
        private void pictureWindowClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
        private void textBoxTote_Enter(object sender, EventArgs e)
        {
            SetColorTextBoxSelected((TextBox)sender);
        }
        private void textBoxTote_Leave(object sender, EventArgs e)
        {
            SetColorTextBoxReleased((TextBox)sender);
        }
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            ActionScanout();
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        private void pictureSwitch_Click(object sender, EventArgs e)
        {
            ActionSwitch();
        }
        // Customized
        private string GetTote()
        {
            return textBoxTote.Text;
        }
        public void ClearForm()
        {
            textBoxTote.Clear();
            textBoxLocation.Clear();
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
        private void SetColorTextBoxSelected(TextBox t)
        {
            t.BackColor = Color.Lime;
        }
        private void SetColorTextBoxReleased(TextBox t)
        {
            t.BackColor = SystemColors.Window;
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
