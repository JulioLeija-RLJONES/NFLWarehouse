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
        #endregion


        public FrmScanIn()
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
        #endregion

        #region Cosmetics
        public void CustomizeTitle()
        {
            this.labelProcessName.ForeColor = titleColor;
        }
        #endregion

        #region Controls
        private void FrmScanIn_Load(object sender, EventArgs e)
        {
            initForm();
            MsgTypes.printme(MsgTypes.msg_success, "Ready", this);
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
                labelVersion.Text = "verison: Debug Mode";
            }
        }
        #endregion
    }
}
