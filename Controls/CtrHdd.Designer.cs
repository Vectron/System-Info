namespace System_Info
{
    partial class CtrHdd
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
            PbFreeSpace.Dispose();
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
            this.LblHddName = new System.Windows.Forms.Label();
            this.LblFreeSpace = new System.Windows.Forms.Label();
            this.PbFreeSpace = new System_Info.ProgressBar();
            this.SuspendLayout();
            // 
            // LblHddName
            // 
            this.LblHddName.AutoSize = true;
            this.LblHddName.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblHddName.Location = new System.Drawing.Point(0, 0);
            this.LblHddName.Name = "LblHddName";
            this.LblHddName.Size = new System.Drawing.Size(28, 13);
            this.LblHddName.TabIndex = 1;
            this.LblHddName.Text = "Text";
            this.LblHddName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblFreeSpace
            // 
            this.LblFreeSpace.AutoSize = true;
            this.LblFreeSpace.Dock = System.Windows.Forms.DockStyle.Right;
            this.LblFreeSpace.Location = new System.Drawing.Point(72, 0);
            this.LblFreeSpace.Name = "LblFreeSpace";
            this.LblFreeSpace.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LblFreeSpace.Size = new System.Drawing.Size(28, 13);
            this.LblFreeSpace.TabIndex = 2;
            this.LblFreeSpace.Text = "Text";
            this.LblFreeSpace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LblFreeSpace.SizeChanged += new System.EventHandler(this.LblFreeSpace_SizeChanged);
            // 
            // PbFreeSpace
            // 
            this.PbFreeSpace.Location = new System.Drawing.Point(0, 16);
            this.PbFreeSpace.Name = "PbFreeSpace";
            this.PbFreeSpace.ShowPercentage = false;
            this.PbFreeSpace.Size = new System.Drawing.Size(100, 7);
            this.PbFreeSpace.TabIndex = 0;
            this.PbFreeSpace.Value = 0F;
            // 
            // CtrHdd
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.LblFreeSpace);
            this.Controls.Add(this.LblHddName);
            this.Controls.Add(this.PbFreeSpace);
            this.Name = "CtrHdd";
            this.Size = new System.Drawing.Size(100, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProgressBar PbFreeSpace;
        private System.Windows.Forms.Label LblHddName;
        private System.Windows.Forms.Label LblFreeSpace;
    }
}
