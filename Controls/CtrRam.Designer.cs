namespace System_Info
{
    partial class CtrRam
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
            PbFreeRam.Dispose();
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
            this.LblFreeRam = new System.Windows.Forms.Label();
            this.LblRamUsed = new System.Windows.Forms.Label();
            this.PbFreeRam = new System_Info.ProgressBar();
            this.SuspendLayout();
            // 
            // LblFreeRam
            // 
            this.LblFreeRam.AutoSize = true;
            this.LblFreeRam.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblFreeRam.Location = new System.Drawing.Point(0, 0);
            this.LblFreeRam.Name = "LblFreeRam";
            this.LblFreeRam.Size = new System.Drawing.Size(62, 13);
            this.LblFreeRam.TabIndex = 1;
            this.LblFreeRam.Text = "RAM: 0 MB";
            // 
            // LblRamUsed
            // 
            this.LblRamUsed.AutoSize = true;
            this.LblRamUsed.BackColor = System.Drawing.Color.Transparent;
            this.LblRamUsed.Dock = System.Windows.Forms.DockStyle.Right;
            this.LblRamUsed.Location = new System.Drawing.Point(73, 0);
            this.LblRamUsed.Name = "LblRamUsed";
            this.LblRamUsed.Size = new System.Drawing.Size(27, 13);
            this.LblRamUsed.TabIndex = 2;
            this.LblRamUsed.Text = "00%";
            this.LblRamUsed.SizeChanged += new System.EventHandler(this.LblRamUsed_SizeChanged);
            // 
            // PbFreeRam
            // 
            this.PbFreeRam.BackColor = System.Drawing.Color.Transparent;
            this.PbFreeRam.ForeColor = System.Drawing.SystemColors.Highlight;
            this.PbFreeRam.Location = new System.Drawing.Point(0, 15);
            this.PbFreeRam.Name = "PbFreeRam";
            this.PbFreeRam.ShowPercentage = false;
            this.PbFreeRam.Size = new System.Drawing.Size(100, 7);
            this.PbFreeRam.TabIndex = 0;
            this.PbFreeRam.Value = 0F;
            // 
            // CtrRam
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.LblRamUsed);
            this.Controls.Add(this.LblFreeRam);
            this.Controls.Add(this.PbFreeRam);
            this.Name = "CtrRam";
            this.Size = new System.Drawing.Size(100, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProgressBar PbFreeRam;
        private System.Windows.Forms.Label LblFreeRam;
        private System.Windows.Forms.Label LblRamUsed;
    }
}
