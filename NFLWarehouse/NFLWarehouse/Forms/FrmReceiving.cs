using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NFLWarehouse.Classes;
using System.Net;

namespace NFLWarehouse.Forms
{
    public partial class FrmReceiving : Form
    {
        private string toolName = "NFL Warehouse";
        private string stationName = "NFL Receiving";
        private string processName = "Receiving";
        private string instruction1 = "Scan Tote Number";
        private string instruction2 = "To confirm receipt, double scan the Tote QR code or press Confirm.";
        private string hostname = Dns.GetHostName();

        public FrmReceiving()
        {
            InitializeComponent();
        }

        private void FrmReceiving_Load(object sender, EventArgs e)
        {
            initForm();
            MsgTypes.printme(MsgTypes.msg_success, "Ready",this);
            

        }

        public void initForm()
        {
            
            this.Text = toolName + " " + stationName;
            labelToolName.Text = this.toolName;
            labelProcessName.Text = this.processName;
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

        #region Logic
        public void ValidationPassed(int index)
        {
            checkedListBoxValidations.SetItemChecked(index, true);
        }
        #endregion


        #region Actions
        public void clearFormAction()
        {
            textBoxTote.Clear();

            checkedListBoxValidations.SetItemChecked(0, false);
            checkedListBoxValidations.SetItemChecked(1, false);
            checkedListBoxValidations.SetItemChecked(2, false);
        }
        #endregion


        #region Controls
        private void buttonClear_Click(object sender, EventArgs e)
        {
            clearFormAction();
        }
        #endregion






    }
}
