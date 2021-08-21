namespace Cay_Nhi_Phan
{
	partial class AVLTree_Form
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
            this.Main_PictureBox = new System.Windows.Forms.PictureBox();
            this.Input_TextBox = new System.Windows.Forms.TextBox();
            this.Del_Tree_Button = new System.Windows.Forms.Button();
            this.Speed_ComboBox = new System.Windows.Forms.ComboBox();
            this.Basic_GroupBox = new System.Windows.Forms.GroupBox();
            this.Speed_Label = new System.Windows.Forms.Label();
            this.N_Random_Label = new System.Windows.Forms.Label();
            this.Random_Button = new System.Windows.Forms.Button();
            this.Random_NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.Delete_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Delete_ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.height = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Main_PictureBox)).BeginInit();
            this.Basic_GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random_NumericUpDown)).BeginInit();
            this.Delete_ContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Main_PictureBox
            // 
            this.Main_PictureBox.BackColor = System.Drawing.Color.White;
            this.Main_PictureBox.Location = new System.Drawing.Point(6, 6);
            this.Main_PictureBox.Name = "Main_PictureBox";
            this.Main_PictureBox.Size = new System.Drawing.Size(1092, 515);
            this.Main_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Main_PictureBox.TabIndex = 1;
            this.Main_PictureBox.TabStop = false;
            // 
            // Input_TextBox
            // 
            this.Input_TextBox.AcceptsReturn = true;
            this.Input_TextBox.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Input_TextBox.Location = new System.Drawing.Point(6, 45);
            this.Input_TextBox.Name = "Input_TextBox";
            this.Input_TextBox.Size = new System.Drawing.Size(118, 20);
            this.Input_TextBox.TabIndex = 1;
            this.Input_TextBox.Text = "Input + Enter to Insert";
            this.Input_TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Input_TextBox_KeyDown);
            // 
            // Del_Tree_Button
            // 
            this.Del_Tree_Button.Location = new System.Drawing.Point(229, 92);
            this.Del_Tree_Button.Name = "Del_Tree_Button";
            this.Del_Tree_Button.Size = new System.Drawing.Size(75, 23);
            this.Del_Tree_Button.TabIndex = 3;
            this.Del_Tree_Button.Text = "Delete Tree";
            this.Del_Tree_Button.UseVisualStyleBackColor = true;
            this.Del_Tree_Button.Click += new System.EventHandler(this.Del_Tree_Button_Click);
            // 
            // Speed_ComboBox
            // 
            this.Speed_ComboBox.FormattingEnabled = true;
            this.Speed_ComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.Speed_ComboBox.Location = new System.Drawing.Point(37, 80);
            this.Speed_ComboBox.Name = "Speed_ComboBox";
            this.Speed_ComboBox.Size = new System.Drawing.Size(66, 21);
            this.Speed_ComboBox.TabIndex = 4;
            // 
            // Basic_GroupBox
            // 
            this.Basic_GroupBox.Controls.Add(this.Speed_Label);
            this.Basic_GroupBox.Controls.Add(this.Speed_ComboBox);
            this.Basic_GroupBox.Controls.Add(this.N_Random_Label);
            this.Basic_GroupBox.Controls.Add(this.Random_Button);
            this.Basic_GroupBox.Controls.Add(this.Del_Tree_Button);
            this.Basic_GroupBox.Controls.Add(this.Random_NumericUpDown);
            this.Basic_GroupBox.Controls.Add(this.Input_TextBox);
            this.Basic_GroupBox.Location = new System.Drawing.Point(12, 532);
            this.Basic_GroupBox.Name = "Basic_GroupBox";
            this.Basic_GroupBox.Size = new System.Drawing.Size(319, 118);
            this.Basic_GroupBox.TabIndex = 5;
            this.Basic_GroupBox.TabStop = false;
            this.Basic_GroupBox.Text = "Basic";
            // 
            // Speed_Label
            // 
            this.Speed_Label.AutoSize = true;
            this.Speed_Label.Location = new System.Drawing.Point(-3, 83);
            this.Speed_Label.Name = "Speed_Label";
            this.Speed_Label.Size = new System.Drawing.Size(38, 13);
            this.Speed_Label.TabIndex = 9;
            this.Speed_Label.Text = "Speed";
            // 
            // N_Random_Label
            // 
            this.N_Random_Label.AutoSize = true;
            this.N_Random_Label.Location = new System.Drawing.Point(3, 18);
            this.N_Random_Label.Name = "N_Random_Label";
            this.N_Random_Label.Size = new System.Drawing.Size(15, 13);
            this.N_Random_Label.TabIndex = 8;
            this.N_Random_Label.Text = "N";
            // 
            // Random_Button
            // 
            this.Random_Button.Location = new System.Drawing.Point(229, 11);
            this.Random_Button.Name = "Random_Button";
            this.Random_Button.Size = new System.Drawing.Size(75, 23);
            this.Random_Button.TabIndex = 6;
            this.Random_Button.Text = "Random";
            this.Random_Button.UseVisualStyleBackColor = true;
            this.Random_Button.Click += new System.EventHandler(this.Random_Button_Click);
            // 
            // Random_NumericUpDown
            // 
            this.Random_NumericUpDown.Location = new System.Drawing.Point(24, 16);
            this.Random_NumericUpDown.Name = "Random_NumericUpDown";
            this.Random_NumericUpDown.Size = new System.Drawing.Size(79, 20);
            this.Random_NumericUpDown.TabIndex = 4;
            this.Random_NumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Random_NumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Delete_ToolStripMenuItem
            // 
            this.Delete_ToolStripMenuItem.Image = global::Cay_Nhi_Phan.Properties.Resources.delete;
            this.Delete_ToolStripMenuItem.Name = "Delete_ToolStripMenuItem";
            this.Delete_ToolStripMenuItem.Size = new System.Drawing.Size(67, 22);
            // 
            // Delete_ContextMenuStrip
            // 
            this.Delete_ContextMenuStrip.AutoSize = false;
            this.Delete_ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Delete_ToolStripMenuItem});
            this.Delete_ContextMenuStrip.Name = "Delete_ContextMenuStrip";
            this.Delete_ContextMenuStrip.Size = new System.Drawing.Size(110, 30);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(353, 543);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Tree Height:";
            // 
            // height
            // 
            this.height.AutoSize = true;
            this.height.Location = new System.Drawing.Point(425, 543);
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(13, 13);
            this.height.TabIndex = 7;
            this.height.Text = "0";
            // 
            // AVLTree_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 659);
            this.Controls.Add(this.height);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Basic_GroupBox);
            this.Controls.Add(this.Main_PictureBox);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(1120, 698);
            this.MinimumSize = new System.Drawing.Size(1120, 698);
            this.Name = "AVLTree_Form";
            this.Text = "Cay Nhi Phan";
            ((System.ComponentModel.ISupportInitialize)(this.Main_PictureBox)).EndInit();
            this.Basic_GroupBox.ResumeLayout(false);
            this.Basic_GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Random_NumericUpDown)).EndInit();
            this.Delete_ContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.PictureBox Main_PictureBox;
		private System.Windows.Forms.TextBox Input_TextBox;
		private System.Windows.Forms.Button Del_Tree_Button;
		private System.Windows.Forms.ComboBox Speed_ComboBox;
		private System.Windows.Forms.GroupBox Basic_GroupBox;
		private System.Windows.Forms.NumericUpDown Random_NumericUpDown;
		private System.Windows.Forms.Button Random_Button;
		private System.Windows.Forms.Label N_Random_Label;
		private System.Windows.Forms.Label Speed_Label;
		private System.Windows.Forms.ToolStripMenuItem Delete_ToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip Delete_ContextMenuStrip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label height;
    }
}

