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
    public partial class FrmShipping : Form
    {
        #region Global Variables
        // Workstation Configuration
        private string toolName = "NFL Warehouse";
        private string stationName = "Shipping Station";
        private string instruction1 = "1 Scan Tote Number";
        private string instruction2 = "";
        private string hostname = Dns.GetHostName();
        private Color titleColor = Color.White;
        private System.Windows.Forms.Form commingFrom;

        private FrmScanIn scanin;
        private FrmScanout scanout;
        private FrmShipping shipping;
        // Database Connection
        private NFLWarehouseDB nflwarehouseDB;
        // Messages
        //string message1 = "Tool shutting down.";
        string message2 = "Connecting to NFL databse, please wait...";
        string message3 = "Tote name {0} has invalid nomeclature, please double check and try again.";
        #endregion

        public FrmShipping()
        {
            InitializeComponent();
        }
        public FrmShipping(System.Windows.Forms.Form commingFrom)
        {
            InitializeComponent();
            this.commingFrom = commingFrom;
        }
        public FrmShipping(FrmScanIn scanin)
        {
            InitializeComponent();
            this.scanin = scanin;
        }
    
        public FrmShipping(FrmScanIn scanin, FrmScanout scanout, FrmShipping shipping)
        {
            InitializeComponent();
            this.scanin = scanin;
            this.scanout = scanout;
            this.shipping = shipping;
        }


        #region Logic
        #endregion


        #region Actions
        public void ActionShip()
        {
            if (Tools.IsGoodToteName(GetTote()))
            {
                nflwarehouseDB.ShipTote(GetTote()); 
                ClearForm();
            }
            else
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
                if (this.Name.Contains("Scanin"))
                {
                    FrmScanout frm = new FrmScanout(this);
                    this.commingFrom = frm;
                    frm.Show();
                }else if(this.Name.Contains("Scanout") || this.Name.Contains("Shipping"))
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
        public void Navigate(Object sender)
        {
            this.Hide();
            if (((Control)sender).Name.Contains("Scanin"))
            {
                if (this.scanin == null)
                {
                    FrmScanIn scanin = new FrmScanIn(this.scanin,this.scanout,this);
                    this.scanin = scanin;
                    scanin.Show();
                }
                else
                {
                    this.scanin.Show();
                }
            }
            else if (((Control)sender).Name.Contains("Shipping"))
            {
                if (this.shipping == null)
                {
                    FrmShipping shipping = new FrmShipping(this.scanin, this.scanout, this);
                    this.shipping = shipping;
                    shipping.Show();
                }
                else
                {
                    this.shipping.Show();
                }
            }
            else if (((Control)sender).Name.Contains("Scanout"))
            {
                if (this.scanout == null)
                {
                    FrmScanout scanout = new FrmScanout(this.scanin, this.scanout, this);
                    this.scanout = scanout;
                    scanout.Show();
                }
                else
                {
                    this.scanout.Show();
                }
            }
        }

        #endregion

        #region Controls
        private async void FrmShipping_Load(object sender, EventArgs e)
        {
            initForm();
            MsgTypes.printme(MsgTypes.msg_success, message2, this);
            nflwarehouseDB = new NFLWarehouseDB(this);
            blockForm();
            await nflwarehouseDB.OpenAsync();
        }
        public void initForm()
        {
            this.Location = new Point((int)(Screen.PrimaryScreen.Bounds.Width * .5-this.Width*.5),
                                      (int)(Screen.PrimaryScreen.Bounds.Height * .5-this.Height*.5));
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
        private void pictureSwitch_Click(object sender, EventArgs e)
        {
            ActionSwitch();
        }
        private void pictureWindowMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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
        private void pictureWindowClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            ActionShip();
        }
        private void pictureBoxScanin_Click(object sender, EventArgs e)
        {
            Navigate(sender);
        }
        private void pictureBoxScanout_Click(object sender, EventArgs e)
        {
            Navigate(sender);
        }
        private void pictureBoxInfoCenter_Click(object sender, EventArgs e)
        {
            FrmInfoCenter frm = new FrmInfoCenter();
            frm.Show();
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
        #endregion

        #region Cosmetics
        public void CustomizeTitle()
        {
            this.labelProcessName.ForeColor = titleColor;
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
