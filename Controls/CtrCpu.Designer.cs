namespace System_Info
{
    partial class CtrCpu
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
            foreach (ProgressBar Pb in PbCpuCore)
            {
                Pb.Dispose();
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LblCpuUse = new System.Windows.Forms.Label();
            this.LblCPU = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblCpuUse
            // 
            this.LblCpuUse.AutoSize = true;
            this.LblCpuUse.BackColor = System.Drawing.Color.Transparent;
            this.LblCpuUse.Dock = System.Windows.Forms.DockStyle.Right;
            this.LblCpuUse.Location = new System.Drawing.Point(73, 0);
            this.LblCpuUse.Name = "LblCpuUse";
            this.LblCpuUse.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblCpuUse.Size = new System.Drawing.Size(27, 13);
            this.LblCpuUse.TabIndex = 3;
            this.LblCpuUse.Text = "00%";
            this.LblCpuUse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LblCpuUse.SizeChanged += new System.EventHandler(this.LblCpuUse_SizeChanged);
            // 
            // LblCPU
            // 
            this.LblCPU.AutoSize = true;
            this.LblCPU.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblCPU.Location = new System.Drawing.Point(0, 0);
            this.LblCPU.Name = "LblCPU";
            this.LblCPU.Size = new System.Drawing.Size(29, 13);
            this.LblCPU.TabIndex = 4;
            this.LblCPU.Text = "CPU";
            this.LblCPU.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CtrCpu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.LblCPU);
            this.Controls.Add(this.LblCpuUse);
            this.Name = "CtrCpu";
            this.Size = new System.Drawing.Size(100, 16);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblCpuUse;
        private System.Windows.Forms.Label LblCPU;

    }
}
