namespace System_Info
{
	partial class CtrDisplay
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
			this.LblName = new System.Windows.Forms.Label();
			this.LblUse = new System.Windows.Forms.Label();
			this.flowpanelProgresbars = new System.Windows.Forms.FlowLayoutPanel();
			this.SuspendLayout();
			// 
			// LblName
			// 
			this.LblName.AutoSize = true;
			this.LblName.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LblName.Location = new System.Drawing.Point(0, 0);
			this.LblName.Margin = new System.Windows.Forms.Padding(0);
			this.LblName.Name = "LblName";
			this.LblName.Size = new System.Drawing.Size(27, 13);
			this.LblName.TabIndex = 10;
			this.LblName.Text = "CPU";
			this.LblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LblUse
			// 
			this.LblUse.AutoSize = true;
			this.LblUse.BackColor = System.Drawing.Color.Transparent;
			this.LblUse.Dock = System.Windows.Forms.DockStyle.Right;
			this.LblUse.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LblUse.Location = new System.Drawing.Point(0, 0);
			this.LblUse.Margin = new System.Windows.Forms.Padding(0);
			this.LblUse.Name = "LblUse";
			this.LblUse.Size = new System.Drawing.Size(28, 13);
			this.LblUse.TabIndex = 16;
			this.LblUse.Text = "00%";
			this.LblUse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// flowpanelProgresbars
			// 
			this.flowpanelProgresbars.AutoSize = true;
			this.flowpanelProgresbars.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowpanelProgresbars.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowpanelProgresbars.Location = new System.Drawing.Point(0, 13);
			this.flowpanelProgresbars.Margin = new System.Windows.Forms.Padding(0);
			this.flowpanelProgresbars.Name = "flowpanelProgresbars";
			this.flowpanelProgresbars.Size = new System.Drawing.Size(0, 0);
			this.flowpanelProgresbars.TabIndex = 11;
			// 
			// CtrDisplay
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.LblName);
			this.Controls.Add(this.LblUse);
			this.Controls.Add(this.flowpanelProgresbars);
			this.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "CtrDisplay";
			this.Size = new System.Drawing.Size(28, 13);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label LblUse;
		private System.Windows.Forms.FlowLayoutPanel flowpanelProgresbars;
		private System.Windows.Forms.Label LblName;
	}
}
