namespace System_Info
{
    partial class CtrNetworkTraffic
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.LblRx = new System.Windows.Forms.Label();
			this.LblTx = new System.Windows.Forms.Label();
			this.LblName = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// LblRx
			// 
			this.LblRx.AutoSize = true;
			this.LblRx.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F);
			this.LblRx.Location = new System.Drawing.Point(0, 24);
			this.LblRx.MaximumSize = new System.Drawing.Size(100, 12);
			this.LblRx.Name = "LblRx";
			this.LblRx.Size = new System.Drawing.Size(33, 12);
			this.LblRx.TabIndex = 0;
			this.LblRx.Text = "label1";
			this.LblRx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblTx
			// 
			this.LblTx.AutoSize = true;
			this.LblTx.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F);
			this.LblTx.Location = new System.Drawing.Point(0, 12);
			this.LblTx.MaximumSize = new System.Drawing.Size(100, 12);
			this.LblTx.Name = "LblTx";
			this.LblTx.Size = new System.Drawing.Size(35, 12);
			this.LblTx.TabIndex = 1;
			this.LblTx.Text = "label2";
			this.LblTx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblName
			// 
			this.LblName.AutoSize = true;
			this.LblName.Dock = System.Windows.Forms.DockStyle.Left;
			this.LblName.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F);
			this.LblName.Location = new System.Drawing.Point(0, 0);
			this.LblName.MaximumSize = new System.Drawing.Size(100, 12);
			this.LblName.Name = "LblName";
			this.LblName.Size = new System.Drawing.Size(33, 12);
			this.LblName.TabIndex = 2;
			this.LblName.Text = "label1";
			this.LblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LblName.SizeChanged += new System.EventHandler(this.LblName_SizeChanged);
			// 
			// CtrNetworkTraffic
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.LblName);
			this.Controls.Add(this.LblTx);
			this.Controls.Add(this.LblRx);
			this.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "CtrNetworkTraffic";
			this.Size = new System.Drawing.Size(38, 36);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblRx;
        private System.Windows.Forms.Label LblTx;
        private System.Windows.Forms.Label LblName;
    }
}
