
namespace NFLWarehouse.Forms
{
    partial class FrmScanIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmScanIn));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.labelHostName = new System.Windows.Forms.Label();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.labelInstruction2 = new System.Windows.Forms.Label();
            this.textBoxTote = new System.Windows.Forms.TextBox();
            this.labelInstruction1 = new System.Windows.Forms.Label();
            this.checkedListBoxValidations = new System.Windows.Forms.CheckedListBox();
            this.labelProcessName = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureWindowMinimize = new System.Windows.Forms.PictureBox();
            this.pictureWindowNormal = new System.Windows.Forms.PictureBox();
            this.pictureWindowClose = new System.Windows.Forms.PictureBox();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.labelToolName = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labelVersion = new System.Windows.Forms.Label();
            this.prompt = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWindowMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWindowNormal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWindowClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.4502F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5498F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1081, 590);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1069, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(9, 44);
            this.panel3.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBoxLocation);
            this.panel2.Controls.Add(this.buttonClear);
            this.panel2.Controls.Add(this.labelHostName);
            this.panel2.Controls.Add(this.buttonConfirm);
            this.panel2.Controls.Add(this.labelInstruction2);
            this.panel2.Controls.Add(this.textBoxTote);
            this.panel2.Controls.Add(this.labelInstruction1);
            this.panel2.Controls.Add(this.checkedListBoxValidations);
            this.panel2.Controls.Add(this.labelProcessName);
            this.panel2.Location = new System.Drawing.Point(3, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1060, 466);
            this.panel2.TabIndex = 2;
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLocation.Location = new System.Drawing.Point(29, 270);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(433, 53);
            this.textBoxLocation.TabIndex = 2;
            this.textBoxLocation.Enter += new System.EventHandler(this.textBoxLocation_Enter);
            this.textBoxLocation.Leave += new System.EventHandler(this.textBoxLocation_Leave);
            // 
            // buttonClear
            // 
            this.buttonClear.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonClear.FlatAppearance.BorderSize = 0;
            this.buttonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClear.Location = new System.Drawing.Point(30, 339);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(260, 98);
            this.buttonClear.TabIndex = 4;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = false;
            // 
            // labelHostName
            // 
            this.labelHostName.AutoSize = true;
            this.labelHostName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelHostName.Location = new System.Drawing.Point(-1, 446);
            this.labelHostName.Name = "labelHostName";
            this.labelHostName.Size = new System.Drawing.Size(72, 17);
            this.labelHostName.TabIndex = 6;
            this.labelHostName.Text = "Hostname";
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.BackColor = System.Drawing.Color.Chartreuse;
            this.buttonConfirm.FlatAppearance.BorderSize = 0;
            this.buttonConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConfirm.Location = new System.Drawing.Point(786, 339);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(260, 98);
            this.buttonConfirm.TabIndex = 3;
            this.buttonConfirm.Text = "Confirm";
            this.buttonConfirm.UseVisualStyleBackColor = false;
            // 
            // labelInstruction2
            // 
            this.labelInstruction2.AutoSize = true;
            this.labelInstruction2.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInstruction2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelInstruction2.Location = new System.Drawing.Point(23, 215);
            this.labelInstruction2.MaximumSize = new System.Drawing.Size(1000, 0);
            this.labelInstruction2.Name = "labelInstruction2";
            this.labelInstruction2.Size = new System.Drawing.Size(194, 38);
            this.labelInstruction2.TabIndex = 4;
            this.labelInstruction2.Text = "Instruction 2";
            // 
            // textBoxTote
            // 
            this.textBoxTote.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTote.Location = new System.Drawing.Point(30, 134);
            this.textBoxTote.Name = "textBoxTote";
            this.textBoxTote.Size = new System.Drawing.Size(432, 53);
            this.textBoxTote.TabIndex = 1;
            this.textBoxTote.Enter += new System.EventHandler(this.textBoxTote_Enter);
            this.textBoxTote.Leave += new System.EventHandler(this.textBoxTote_Leave);
            // 
            // labelInstruction1
            // 
            this.labelInstruction1.AutoSize = true;
            this.labelInstruction1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInstruction1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelInstruction1.Location = new System.Drawing.Point(23, 89);
            this.labelInstruction1.Name = "labelInstruction1";
            this.labelInstruction1.Size = new System.Drawing.Size(194, 38);
            this.labelInstruction1.TabIndex = 2;
            this.labelInstruction1.Text = "Instruction 1";
            // 
            // checkedListBoxValidations
            // 
            this.checkedListBoxValidations.BackColor = System.Drawing.Color.Black;
            this.checkedListBoxValidations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBoxValidations.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.checkedListBoxValidations.FormattingEnabled = true;
            this.checkedListBoxValidations.Items.AddRange(new object[] {
            "Validation 1",
            "Validation 2",
            "Validation 3"});
            this.checkedListBoxValidations.Location = new System.Drawing.Point(915, 15);
            this.checkedListBoxValidations.Name = "checkedListBoxValidations";
            this.checkedListBoxValidations.Size = new System.Drawing.Size(103, 68);
            this.checkedListBoxValidations.TabIndex = 1;
            this.checkedListBoxValidations.TabStop = false;
            this.checkedListBoxValidations.Visible = false;
            // 
            // labelProcessName
            // 
            this.labelProcessName.AutoSize = true;
            this.labelProcessName.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProcessName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelProcessName.Location = new System.Drawing.Point(13, -8);
            this.labelProcessName.Name = "labelProcessName";
            this.labelProcessName.Size = new System.Drawing.Size(561, 91);
            this.labelProcessName.TabIndex = 0;
            this.labelProcessName.Text = "Process Name";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.panel4.Controls.Add(this.pictureWindowMinimize);
            this.panel4.Controls.Add(this.pictureWindowNormal);
            this.panel4.Controls.Add(this.pictureWindowClose);
            this.panel4.Controls.Add(this.pictureBoxLogo);
            this.panel4.Controls.Add(this.labelToolName);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1060, 44);
            this.panel4.TabIndex = 4;
            this.panel4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseDown);
            this.panel4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseMove);
            this.panel4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseUp);
            // 
            // pictureWindowMinimize
            // 
            this.pictureWindowMinimize.Image = global::NFLWarehouse.Properties.Resources.minimize_1;
            this.pictureWindowMinimize.Location = new System.Drawing.Point(930, 4);
            this.pictureWindowMinimize.Name = "pictureWindowMinimize";
            this.pictureWindowMinimize.Size = new System.Drawing.Size(40, 37);
            this.pictureWindowMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureWindowMinimize.TabIndex = 4;
            this.pictureWindowMinimize.TabStop = false;
            this.pictureWindowMinimize.Click += new System.EventHandler(this.pictureWindowMinimize_Click);
            // 
            // pictureWindowNormal
            // 
            this.pictureWindowNormal.Image = global::NFLWarehouse.Properties.Resources.maximize;
            this.pictureWindowNormal.Location = new System.Drawing.Point(974, 4);
            this.pictureWindowNormal.Name = "pictureWindowNormal";
            this.pictureWindowNormal.Size = new System.Drawing.Size(40, 37);
            this.pictureWindowNormal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureWindowNormal.TabIndex = 3;
            this.pictureWindowNormal.TabStop = false;
            this.pictureWindowNormal.Click += new System.EventHandler(this.pictureWindowNormal_Click);
            // 
            // pictureWindowClose
            // 
            this.pictureWindowClose.Image = global::NFLWarehouse.Properties.Resources.close_1;
            this.pictureWindowClose.Location = new System.Drawing.Point(1018, 4);
            this.pictureWindowClose.Name = "pictureWindowClose";
            this.pictureWindowClose.Size = new System.Drawing.Size(40, 37);
            this.pictureWindowClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureWindowClose.TabIndex = 2;
            this.pictureWindowClose.TabStop = false;
            this.pictureWindowClose.Click += new System.EventHandler(this.pictureWindowClose_Click);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::NFLWarehouse.Properties.Resources.unbrela;
            this.pictureBoxLogo.Location = new System.Drawing.Point(3, 2);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(40, 37);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 1;
            this.pictureBoxLogo.TabStop = false;
            this.pictureBoxLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxLogo_MouseDown);
            this.pictureBoxLogo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxLogo_MouseMove);
            this.pictureBoxLogo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxLogo_MouseUp);
            // 
            // labelToolName
            // 
            this.labelToolName.AutoSize = true;
            this.labelToolName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelToolName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelToolName.Location = new System.Drawing.Point(46, 9);
            this.labelToolName.Name = "labelToolName";
            this.labelToolName.Size = new System.Drawing.Size(108, 25);
            this.labelToolName.TabIndex = 0;
            this.labelToolName.Text = "Tool Name";
            this.labelToolName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelToolName_MouseDown);
            this.labelToolName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelToolName_MouseMove);
            this.labelToolName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelToolName_MouseUp);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.labelVersion);
            this.panel5.Controls.Add(this.prompt);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 525);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1060, 62);
            this.panel5.TabIndex = 5;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.BackColor = System.Drawing.Color.Black;
            this.labelVersion.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelVersion.Location = new System.Drawing.Point(941, -1);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(106, 17);
            this.labelVersion.TabIndex = 2;
            this.labelVersion.Text = "Version: Debug";
            // 
            // prompt
            // 
            this.prompt.BackColor = System.Drawing.Color.Black;
            this.prompt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.prompt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prompt.ForeColor = System.Drawing.Color.Lime;
            this.prompt.Location = new System.Drawing.Point(0, 0);
            this.prompt.Name = "prompt";
            this.prompt.Size = new System.Drawing.Size(1060, 62);
            this.prompt.TabIndex = 1;
            this.prompt.TabStop = false;
            this.prompt.Text = "";
            // 
            // FrmScanIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1081, 590);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmScanIn";
            this.Text = "FrmScanIn";
            this.Load += new System.EventHandler(this.FrmScanIn_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWindowMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWindowNormal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWindowClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label labelHostName;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Label labelInstruction2;
        private System.Windows.Forms.TextBox textBoxTote;
        private System.Windows.Forms.Label labelInstruction1;
        private System.Windows.Forms.CheckedListBox checkedListBoxValidations;
        private System.Windows.Forms.Label labelProcessName;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label labelToolName;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.RichTextBox prompt;
        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.PictureBox pictureWindowMinimize;
        private System.Windows.Forms.PictureBox pictureWindowNormal;
        private System.Windows.Forms.PictureBox pictureWindowClose;
    }
}