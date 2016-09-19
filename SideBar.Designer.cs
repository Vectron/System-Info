namespace System_Info
{
    partial class SideBar
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
			this.components = new System.ComponentModel.Container();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.graphicalOverlay1 = new CodeProject.GraphicalOverlay(this.components);
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(0, 0);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// graphicalOverlay1
			// 
			this.graphicalOverlay1.Paint += new System.EventHandler<System.Windows.Forms.PaintEventArgs>(this.SideBar_Paint);
			// 
			// SideBar
			// 
			this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Black;
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(243, 521);
			this.ControlBox = false;
			this.Controls.Add(this.flowLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SideBar";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SideBar_FormClosing);
			this.Load += new System.EventHandler(this.SideBar_Load);
			this.SizeChanged += new System.EventHandler(this.SideBar_SizeChanged);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);
			this.ResumeLayout(false);
			this.PerformLayout();

        }







		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private CodeProject.GraphicalOverlay graphicalOverlay1;
	}
}