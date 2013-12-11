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
            this.CPUScrollBar = new System.Windows.Forms.HScrollBar();
            this.CPUTextBox = new System.Windows.Forms.TextBox();
            this.CPUGroupBox = new System.Windows.Forms.GroupBox();
            this.CPUProgressBar = new System_Info.ProgressBar();
            this.RamGroupBox = new System.Windows.Forms.GroupBox();
            this.RamProgressBar = new System_Info.ProgressBar();
            this.RamTextBox = new System.Windows.Forms.TextBox();
            this.RamScrollBar = new System.Windows.Forms.HScrollBar();
            this.HddGroupBox = new System.Windows.Forms.GroupBox();
            this.HddProgressBar = new System_Info.ProgressBar();
            this.HddTextBox = new System.Windows.Forms.TextBox();
            this.HddScrollBar = new System.Windows.Forms.HScrollBar();
            this.CPUGroupBox.SuspendLayout();
            this.RamGroupBox.SuspendLayout();
            this.HddGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CPUScrollBar
            // 
            this.CPUScrollBar.LargeChange = 1;
            this.CPUScrollBar.Location = new System.Drawing.Point(6, 20);
            this.CPUScrollBar.Maximum = 20;
            this.CPUScrollBar.Minimum = 1;
            this.CPUScrollBar.Name = "CPUScrollBar";
            this.CPUScrollBar.Size = new System.Drawing.Size(103, 20);
            this.CPUScrollBar.TabIndex = 1;
            this.CPUScrollBar.Value = 1;
            this.CPUScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.CPUScrollBar_Scroll);
            this.CPUScrollBar.ValueChanged += new System.EventHandler(this.CPUScrollBar_ValueChanged);
            // 
            // CPUTextBox
            // 
            this.CPUTextBox.Location = new System.Drawing.Point(112, 20);
            this.CPUTextBox.Name = "CPUTextBox";
            this.CPUTextBox.Size = new System.Drawing.Size(38, 20);
            this.CPUTextBox.TabIndex = 2;
            this.CPUTextBox.TextChanged += new System.EventHandler(this.CPUTextBox_TextChanged);
            this.CPUTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CPUTextBox_KeyPress);
            // 
            // CPUGroupBox
            // 
            this.CPUGroupBox.Controls.Add(this.CPUProgressBar);
            this.CPUGroupBox.Controls.Add(this.CPUTextBox);
            this.CPUGroupBox.Controls.Add(this.CPUScrollBar);
            this.CPUGroupBox.Location = new System.Drawing.Point(12, 12);
            this.CPUGroupBox.Name = "CPUGroupBox";
            this.CPUGroupBox.Size = new System.Drawing.Size(153, 72);
            this.CPUGroupBox.TabIndex = 3;
            this.CPUGroupBox.TabStop = false;
            this.CPUGroupBox.Text = "CPU Bar Size";
            // 
            // CPUProgressBar
            // 
            this.CPUProgressBar.Location = new System.Drawing.Point(6, 43);
            this.CPUProgressBar.Name = "CPUProgressBar";
            this.CPUProgressBar.ShowPercentage = false;
            this.CPUProgressBar.Size = new System.Drawing.Size(100, 20);
            this.CPUProgressBar.TabIndex = 0;
            this.CPUProgressBar.Value = 0F;
            // 
            // RamGroupBox
            // 
            this.RamGroupBox.Controls.Add(this.RamProgressBar);
            this.RamGroupBox.Controls.Add(this.RamTextBox);
            this.RamGroupBox.Controls.Add(this.RamScrollBar);
            this.RamGroupBox.Location = new System.Drawing.Point(12, 90);
            this.RamGroupBox.Name = "RamGroupBox";
            this.RamGroupBox.Size = new System.Drawing.Size(153, 72);
            this.RamGroupBox.TabIndex = 4;
            this.RamGroupBox.TabStop = false;
            this.RamGroupBox.Text = "Ram Bar Size";
            // 
            // RamProgressBar
            // 
            this.RamProgressBar.Location = new System.Drawing.Point(6, 43);
            this.RamProgressBar.Name = "RamProgressBar";
            this.RamProgressBar.ShowPercentage = false;
            this.RamProgressBar.Size = new System.Drawing.Size(100, 20);
            this.RamProgressBar.TabIndex = 0;
            this.RamProgressBar.Value = 0F;
            // 
            // RamTextBox
            // 
            this.RamTextBox.Location = new System.Drawing.Point(112, 20);
            this.RamTextBox.Name = "RamTextBox";
            this.RamTextBox.Size = new System.Drawing.Size(38, 20);
            this.RamTextBox.TabIndex = 2;
            this.RamTextBox.TextChanged += new System.EventHandler(this.RamTextBox_TextChanged);
            this.RamTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RamTextBox_KeyPress);
            // 
            // RamScrollBar
            // 
            this.RamScrollBar.LargeChange = 1;
            this.RamScrollBar.Location = new System.Drawing.Point(6, 20);
            this.RamScrollBar.Maximum = 20;
            this.RamScrollBar.Minimum = 1;
            this.RamScrollBar.Name = "RamScrollBar";
            this.RamScrollBar.Size = new System.Drawing.Size(103, 20);
            this.RamScrollBar.TabIndex = 1;
            this.RamScrollBar.Value = 1;
            this.RamScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.RamScrollBar_Scroll);
            this.RamScrollBar.ValueChanged += new System.EventHandler(this.RamScrollBar_ValueChanged);
            // 
            // HddGroupBox
            // 
            this.HddGroupBox.Controls.Add(this.HddProgressBar);
            this.HddGroupBox.Controls.Add(this.HddTextBox);
            this.HddGroupBox.Controls.Add(this.HddScrollBar);
            this.HddGroupBox.Location = new System.Drawing.Point(12, 168);
            this.HddGroupBox.Name = "HddGroupBox";
            this.HddGroupBox.Size = new System.Drawing.Size(153, 72);
            this.HddGroupBox.TabIndex = 5;
            this.HddGroupBox.TabStop = false;
            this.HddGroupBox.Text = "HDD Bar Size";
            // 
            // HddProgressBar
            // 
            this.HddProgressBar.Location = new System.Drawing.Point(6, 43);
            this.HddProgressBar.Name = "HddProgressBar";
            this.HddProgressBar.ShowPercentage = false;
            this.HddProgressBar.Size = new System.Drawing.Size(100, 20);
            this.HddProgressBar.TabIndex = 0;
            this.HddProgressBar.Value = 0F;
            // 
            // HddTextBox
            // 
            this.HddTextBox.Location = new System.Drawing.Point(112, 20);
            this.HddTextBox.Name = "HddTextBox";
            this.HddTextBox.Size = new System.Drawing.Size(38, 20);
            this.HddTextBox.TabIndex = 2;
            this.HddTextBox.TextChanged += new System.EventHandler(this.HddTextBox_TextChanged);
            this.HddTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HddTextBox_KeyPress);
            // 
            // HddScrollBar
            // 
            this.HddScrollBar.LargeChange = 1;
            this.HddScrollBar.Location = new System.Drawing.Point(6, 20);
            this.HddScrollBar.Maximum = 20;
            this.HddScrollBar.Minimum = 1;
            this.HddScrollBar.Name = "HddScrollBar";
            this.HddScrollBar.Size = new System.Drawing.Size(103, 20);
            this.HddScrollBar.TabIndex = 1;
            this.HddScrollBar.Value = 1;
            this.HddScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.HddScrollBar_Scroll);
            this.HddScrollBar.ValueChanged += new System.EventHandler(this.HddScrollBar_ValueChanged);
            // 
            // Options_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 292);
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

        private ProgressBar CPUProgressBar;
        private System.Windows.Forms.HScrollBar CPUScrollBar;
        private System.Windows.Forms.TextBox CPUTextBox;
        private System.Windows.Forms.GroupBox CPUGroupBox;
        private System.Windows.Forms.GroupBox RamGroupBox;
        private ProgressBar RamProgressBar;
        private System.Windows.Forms.TextBox RamTextBox;
        private System.Windows.Forms.HScrollBar RamScrollBar;
        private System.Windows.Forms.GroupBox HddGroupBox;
        private ProgressBar HddProgressBar;
        private System.Windows.Forms.TextBox HddTextBox;
        private System.Windows.Forms.HScrollBar HddScrollBar;
    }
}