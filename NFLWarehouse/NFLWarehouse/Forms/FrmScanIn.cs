﻿using System;
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

    public partial class FrmScanIn : Form
    {

        #region Global Variables
        private string toolName = "NFL Warehouse";
        private string stationName = "Scan In Station";
        private string processName = "Scan In";
        private string instruction1 = "1 Scan Tote Number";
        private string instruction2 = "2 Scan the location barcode where the item is being stored";
        private string hostname = Dns.GetHostName();
        private Color titleColor = Color.Yellow;
        private NFLWarehouseDB nflwarehouseDB;
        private System.Windows.Forms.Form commingFrom;
        public bool flag = false;

        // Messages
        string message1 = "Tool shutting down.";
        string message2 = "Connecting to NFL databse, please wait...";

        #endregion


        public FrmScanIn()
        {
            InitializeComponent();
        }
        public FrmScanIn(System.Windows.Forms.Form commingFrom)
        {
            InitializeComponent();
           
        }



        #region Logic
        public void ValidationPassed(int index)
        {
            checkedListBoxValidations.SetItemChecked(index, true);
        }
        #endregion

        #region Actions
        public void ActionAllocate()
        {
            nflwarehouseDB.AllocateTote(GetTote(), GetLocation());
            ClearForm();
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
        private async void FrmScanIn_Load(object sender, EventArgs e)
        {
            initForm();
            MsgTypes.printme(MsgTypes.msg_success, message2, this);

            nflwarehouseDB = new NFLWarehouseDB(this);
            blockForm();
            await nflwarehouseDB.OpenAsync();
           
        }
        private string GetTote()
        {
            return textBoxTote.Text;
        }
        public string GetLocation()
        {
            return textBoxLocation.Text;
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
            SetColorTextBoxSelected((TextBox) sender);
        }
        private void textBoxTote_Leave(object sender, EventArgs e)
        {
            SetColorTextBoxReleased((TextBox)sender);
        }
        private void textBoxLocation_Enter(object sender, EventArgs e)
        {
            SetColorTextBoxSelected((TextBox) sender);
        }
        private void textBoxLocation_Leave(object sender, EventArgs e)
        {
            SetColorTextBoxReleased((TextBox)sender);
            
        }
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            ActionAllocate();
        }
        private void FrmScanIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            MsgTypes.printme(MsgTypes.msg_success, message1, this);
        }
        // Custom
        private void SetColorTextBoxSelected(TextBox t)
        {
            t.BackColor = Color.Lime;
        }
        private void SetColorTextBoxReleased(TextBox t)
        {
            t.BackColor = SystemColors.Window;
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
