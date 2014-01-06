namespace System_Info
{
    partial class Options_Screen
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
            this.scrollbarHSizeCPU = new System.Windows.Forms.HScrollBar();
            this.txtboxVSizeCPU = new System.Windows.Forms.TextBox();
            this.CPUGroupBox = new System.Windows.Forms.GroupBox();
            this.txtboxHSizeCPU = new System.Windows.Forms.TextBox();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.scrollbarVSizeCPU = new System.Windows.Forms.VScrollBar();
            this.progbarCPU = new System_Info.ProgressBar();
            this.RamGroupBox = new System.Windows.Forms.GroupBox();
            this.txtboxHSizeRAM = new System.Windows.Forms.TextBox();
            this.scrollbarVSizeRAM = new System.Windows.Forms.VScrollBar();
            this.progbarRAM = new System_Info.ProgressBar();
            this.txtboxVSizeRAM = new System.Windows.Forms.TextBox();
            this.scrollbarHSizeRAM = new System.Windows.Forms.HScrollBar();
            this.HddGroupBox = new System.Windows.Forms.GroupBox();
            this.txtboxHSizeHDD = new System.Windows.Forms.TextBox();
            this.scrollbarVSizeHDD = new System.Windows.Forms.VScrollBar();
            this.progbarHDD = new System_Info.ProgressBar();
            this.txtboxVSizeHDD = new System.Windows.Forms.TextBox();
            this.scrollbarHSizeHDD = new System.Windows.Forms.HScrollBar();
            this.btnResetDefaults = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.CPUGroupBox.SuspendLayout();
            this.RamGroupBox.SuspendLayout();
            this.HddGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrollbarHSizeCPU
            // 
            this.scrollbarHSizeCPU.LargeChange = 20;
            this.scrollbarHSizeCPU.Location = new System.Drawing.Point(29, 92);
            this.scrollbarHSizeCPU.Maximum = 419;
            this.scrollbarHSizeCPU.Minimum = 40;
            this.scrollbarHSizeCPU.Name = "scrollbarHSizeCPU";
            this.scrollbarHSizeCPU.Size = new System.Drawing.Size(360, 20);
            this.scrollbarHSizeCPU.SmallChange = 20;
            this.scrollbarHSizeCPU.TabIndex = 1;
            this.scrollbarHSizeCPU.Value = 40;
            this.scrollbarHSizeCPU.ValueChanged += new System.EventHandler(this.scrollbarHSize_ValueChanged);
            // 
            // txtboxVSizeCPU
            // 
            this.txtboxVSizeCPU.Location = new System.Drawing.Point(6, 19);
            this.txtboxVSizeCPU.Name = "txtboxVSizeCPU";
            this.txtboxVSizeCPU.Size = new System.Drawing.Size(20, 20);
            this.txtboxVSizeCPU.TabIndex = 2;
            this.txtboxVSizeCPU.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtboxVSize_KeyPress);
            // 
            // CPUGroupBox
            // 
            this.CPUGroupBox.Controls.Add(this.txtboxHSizeCPU);
            this.CPUGroupBox.Controls.Add(this.vScrollBar1);
            this.CPUGroupBox.Controls.Add(this.scrollbarVSizeCPU);
            this.CPUGroupBox.Controls.Add(this.progbarCPU);
            this.CPUGroupBox.Controls.Add(this.txtboxVSizeCPU);
            this.CPUGroupBox.Controls.Add(this.scrollbarHSizeCPU);
            this.CPUGroupBox.Location = new System.Drawing.Point(12, 12);
            this.CPUGroupBox.Name = "CPUGroupBox";
            this.CPUGroupBox.Size = new System.Drawing.Size(440, 120);
            this.CPUGroupBox.TabIndex = 3;
            this.CPUGroupBox.TabStop = false;
            this.CPUGroupBox.Text = "CPU Bar Size";
            // 
            // txtboxHSizeCPU
            // 
            this.txtboxHSizeCPU.Location = new System.Drawing.Point(392, 92);
            this.txtboxHSizeCPU.Name = "txtboxHSizeCPU";
            this.txtboxHSizeCPU.Size = new System.Drawing.Size(38, 20);
            this.txtboxHSizeCPU.TabIndex = 5;
            this.txtboxHSizeCPU.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtboxHSize_KeyPress);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.LargeChange = 1;
            this.vScrollBar1.Location = new System.Drawing.Point(6, 299);
            this.vScrollBar1.Maximum = 20;
            this.vScrollBar1.Minimum = 1;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 70);
            this.vScrollBar1.TabIndex = 3;
            this.vScrollBar1.Value = 1;
            this.vScrollBar1.ValueChanged += new System.EventHandler(this.scrollbarVSize_ValueChanged);
            // 
            // scrollbarVSizeCPU
            // 
            this.scrollbarVSizeCPU.LargeChange = 1;
            this.scrollbarVSizeCPU.Location = new System.Drawing.Point(6, 42);
            this.scrollbarVSizeCPU.Maximum = 20;
            this.scrollbarVSizeCPU.Minimum = 1;
            this.scrollbarVSizeCPU.Name = "scrollbarVSizeCPU";
            this.scrollbarVSizeCPU.Size = new System.Drawing.Size(17, 70);
            this.scrollbarVSizeCPU.TabIndex = 4;
            this.scrollbarVSizeCPU.Value = 1;
            this.scrollbarVSizeCPU.ValueChanged += new System.EventHandler(this.scrollbarVSize_ValueChanged);
            // 
            // progbarCPU
            // 
            this.progbarCPU.Location = new System.Drawing.Point(29, 50);
            this.progbarCPU.Name = "progbarCPU";
            this.progbarCPU.ShowPercentage = false;
            this.progbarCPU.Size = new System.Drawing.Size(400, 20);
            this.progbarCPU.TabIndex = 0;
            this.progbarCPU.Value = 0F;
            // 
            // RamGroupBox
            // 
            this.RamGroupBox.Controls.Add(this.txtboxHSizeRAM);
            this.RamGroupBox.Controls.Add(this.scrollbarVSizeRAM);
            this.RamGroupBox.Controls.Add(this.progbarRAM);
            this.RamGroupBox.Controls.Add(this.txtboxVSizeRAM);
            this.RamGroupBox.Controls.Add(this.scrollbarHSizeRAM);
            this.RamGroupBox.Location = new System.Drawing.Point(12, 138);
            this.RamGroupBox.Name = "RamGroupBox";
            this.RamGroupBox.Size = new System.Drawing.Size(440, 120);
            this.RamGroupBox.TabIndex = 4;
            this.RamGroupBox.TabStop = false;
            this.RamGroupBox.Text = "Ram Bar Size";
            // 
            // txtboxHSizeRAM
            // 
            this.txtboxHSizeRAM.Location = new System.Drawing.Point(392, 92);
            this.txtboxHSizeRAM.Name = "txtboxHSizeRAM";
            this.txtboxHSizeRAM.Size = new System.Drawing.Size(38, 20);
            this.txtboxHSizeRAM.TabIndex = 8;
            this.txtboxHSizeRAM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtboxHSize_KeyPress);
            // 
            // scrollbarVSizeRAM
            // 
            this.scrollbarVSizeRAM.LargeChange = 1;
            this.scrollbarVSizeRAM.Location = new System.Drawing.Point(6, 42);
            this.scrollbarVSizeRAM.Maximum = 20;
            this.scrollbarVSizeRAM.Minimum = 1;
            this.scrollbarVSizeRAM.Name = "scrollbarVSizeRAM";
            this.scrollbarVSizeRAM.Size = new System.Drawing.Size(17, 70);
            this.scrollbarVSizeRAM.TabIndex = 7;
            this.scrollbarVSizeRAM.Value = 1;
            this.scrollbarVSizeRAM.ValueChanged += new System.EventHandler(this.scrollbarVSize_ValueChanged);
            // 
            // progbarRAM
            // 
            this.progbarRAM.Location = new System.Drawing.Point(29, 50);
            this.progbarRAM.Name = "progbarRAM";
            this.progbarRAM.ShowPercentage = false;
            this.progbarRAM.Size = new System.Drawing.Size(400, 20);
            this.progbarRAM.TabIndex = 0;
            this.progbarRAM.Value = 0F;
            // 
            // txtboxVSizeRAM
            // 
            this.txtboxVSizeRAM.Location = new System.Drawing.Point(6, 19);
            this.txtboxVSizeRAM.Name = "txtboxVSizeRAM";
            this.txtboxVSizeRAM.Size = new System.Drawing.Size(20, 20);
            this.txtboxVSizeRAM.TabIndex = 2;
            this.txtboxVSizeRAM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtboxVSize_KeyPress);
            // 
            // scrollbarHSizeRAM
            // 
            this.scrollbarHSizeRAM.LargeChange = 20;
            this.scrollbarHSizeRAM.Location = new System.Drawing.Point(29, 92);
            this.scrollbarHSizeRAM.Maximum = 419;
            this.scrollbarHSizeRAM.Minimum = 40;
            this.scrollbarHSizeRAM.Name = "scrollbarHSizeRAM";
            this.scrollbarHSizeRAM.Size = new System.Drawing.Size(360, 20);
            this.scrollbarHSizeRAM.SmallChange = 20;
            this.scrollbarHSizeRAM.TabIndex = 1;
            this.scrollbarHSizeRAM.Value = 40;
            this.scrollbarHSizeRAM.ValueChanged += new System.EventHandler(this.scrollbarHSize_ValueChanged);
            // 
            // HddGroupBox
            // 
            this.HddGroupBox.Controls.Add(this.txtboxHSizeHDD);
            this.HddGroupBox.Controls.Add(this.scrollbarVSizeHDD);
            this.HddGroupBox.Controls.Add(this.progbarHDD);
            this.HddGroupBox.Controls.Add(this.txtboxVSizeHDD);
            this.HddGroupBox.Controls.Add(this.scrollbarHSizeHDD);
            this.HddGroupBox.Location = new System.Drawing.Point(12, 264);
            this.HddGroupBox.Name = "HddGroupBox";
            this.HddGroupBox.Size = new System.Drawing.Size(440, 120);
            this.HddGroupBox.TabIndex = 5;
            this.HddGroupBox.TabStop = false;
            this.HddGroupBox.Text = "HDD Bar Size";
            // 
            // txtboxHSizeHDD
            // 
            this.txtboxHSizeHDD.Location = new System.Drawing.Point(392, 92);
            this.txtboxHSizeHDD.Name = "txtboxHSizeHDD";
            this.txtboxHSizeHDD.Size = new System.Drawing.Size(38, 20);
            this.txtboxHSizeHDD.TabIndex = 4;
            this.txtboxHSizeHDD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtboxHSize_KeyPress);
            // 
            // scrollbarVSizeHDD
            // 
            this.scrollbarVSizeHDD.LargeChange = 1;
            this.scrollbarVSizeHDD.Location = new System.Drawing.Point(6, 42);
            this.scrollbarVSizeHDD.Maximum = 20;
            this.scrollbarVSizeHDD.Minimum = 1;
            this.scrollbarVSizeHDD.Name = "scrollbarVSizeHDD";
            this.scrollbarVSizeHDD.Size = new System.Drawing.Size(17, 70);
            this.scrollbarVSizeHDD.TabIndex = 3;
            this.scrollbarVSizeHDD.Value = 1;
            this.scrollbarVSizeHDD.ValueChanged += new System.EventHandler(this.scrollbarVSize_ValueChanged);
            // 
            // progbarHDD
            // 
            this.progbarHDD.Location = new System.Drawing.Point(29, 50);
            this.progbarHDD.Name = "progbarHDD";
            this.progbarHDD.ShowPercentage = false;
            this.progbarHDD.Size = new System.Drawing.Size(400, 20);
            this.progbarHDD.TabIndex = 0;
            this.progbarHDD.Value = 0F;
            // 
            // txtboxVSizeHDD
            // 
            this.txtboxVSizeHDD.Location = new System.Drawing.Point(6, 19);
            this.txtboxVSizeHDD.Name = "txtboxVSizeHDD";
            this.txtboxVSizeHDD.Size = new System.Drawing.Size(20, 20);
            this.txtboxVSizeHDD.TabIndex = 2;
            this.txtboxVSizeHDD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtboxVSize_KeyPress);
            // 
            // scrollbarHSizeHDD
            // 
            this.scrollbarHSizeHDD.LargeChange = 20;
            this.scrollbarHSizeHDD.Location = new System.Drawing.Point(29, 92);
            this.scrollbarHSizeHDD.Maximum = 419;
            this.scrollbarHSizeHDD.Minimum = 40;
            this.scrollbarHSizeHDD.Name = "scrollbarHSizeHDD";
            this.scrollbarHSizeHDD.Size = new System.Drawing.Size(360, 20);
            this.scrollbarHSizeHDD.SmallChange = 20;
            this.scrollbarHSizeHDD.TabIndex = 1;
            this.scrollbarHSizeHDD.Value = 40;
            this.scrollbarHSizeHDD.ValueChanged += new System.EventHandler(this.scrollbarHSize_ValueChanged);
            // 
            // btnResetDefaults
            // 
            this.btnResetDefaults.Location = new System.Drawing.Point(18, 549);
            this.btnResetDefaults.Name = "btnResetDefaults";
            this.btnResetDefaults.Size = new System.Drawing.Size(100, 40);
            this.btnResetDefaults.TabIndex = 6;
            this.btnResetDefaults.Text = "Reset Defaults";
            this.btnResetDefaults.UseVisualStyleBackColor = true;
            this.btnResetDefaults.Click += new System.EventHandler(this.btnResetDefaults_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(661, 549);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(100, 40);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(767, 549);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // Options_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 601);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnResetDefaults);
            this.Controls.Add(this.HddGroupBox);
            this.Controls.Add(this.RamGroupBox);
            this.Controls.Add(this.CPUGroupBox);
            this.Name = "Options_Screen";
            this.Text = "Options Screen";
            this.CPUGroupBox.ResumeLayout(false);
            this.CPUGroupBox.PerformLayout();
            this.RamGroupBox.ResumeLayout(false);
            this.RamGroupBox.PerformLayout();
            this.HddGroupBox.ResumeLayout(false);
            this.HddGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ProgressBar progbarCPU;
        private System.Windows.Forms.HScrollBar scrollbarHSizeCPU;
        private System.Windows.Forms.TextBox txtboxVSizeCPU;
        private System.Windows.Forms.GroupBox CPUGroupBox;
        private System.Windows.Forms.GroupBox RamGroupBox;
        private ProgressBar progbarRAM;
        private System.Windows.Forms.TextBox txtboxVSizeRAM;
        private System.Windows.Forms.HScrollBar scrollbarHSizeRAM;
        private System.Windows.Forms.GroupBox HddGroupBox;
        private ProgressBar progbarHDD;
        private System.Windows.Forms.TextBox txtboxVSizeHDD;
        private System.Windows.Forms.HScrollBar scrollbarHSizeHDD;
        private System.Windows.Forms.Button btnResetDefaults;
        private System.Windows.Forms.VScrollBar scrollbarVSizeCPU;
        private System.Windows.Forms.VScrollBar scrollbarVSizeRAM;
        private System.Windows.Forms.VScrollBar scrollbarVSizeHDD;
        private System.Windows.Forms.TextBox txtboxHSizeCPU;
        private System.Windows.Forms.TextBox txtboxHSizeRAM;
        private System.Windows.Forms.TextBox txtboxHSizeHDD;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
    }
}