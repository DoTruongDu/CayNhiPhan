namespace SimuBTree
{
  partial class CayBTree
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.BTreeView = new System.Windows.Forms.TreeView();
            this.btAjouter = new System.Windows.Forms.Button();
            this.btSupprimer = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNb = new System.Windows.Forms.TextBox();
            this.tbMax = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BTreeView
            // 
            this.BTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTreeView.Location = new System.Drawing.Point(0, 0);
            this.BTreeView.Name = "BTreeView";
            this.BTreeView.Size = new System.Drawing.Size(734, 597);
            this.BTreeView.TabIndex = 0;
            // 
            // btAjouter
            // 
            this.btAjouter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btAjouter.Location = new System.Drawing.Point(755, 106);
            this.btAjouter.Name = "btAjouter";
            this.btAjouter.Size = new System.Drawing.Size(75, 23);
            this.btAjouter.TabIndex = 9;
            this.btAjouter.Text = "Tạo mới ";
            this.btAjouter.UseVisualStyleBackColor = true;
            this.btAjouter.Click += new System.EventHandler(this.btAjouter_Click);
            // 
            // btSupprimer
            // 
            this.btSupprimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSupprimer.Location = new System.Drawing.Point(755, 135);
            this.btSupprimer.Name = "btSupprimer";
            this.btSupprimer.Size = new System.Drawing.Size(75, 23);
            this.btSupprimer.TabIndex = 10;
            this.btSupprimer.Text = "Xoá ";
            this.btSupprimer.UseVisualStyleBackColor = true;
            this.btSupprimer.Click += new System.EventHandler(this.btSupprimer_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(755, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Min ";
            // 
            // tbNb
            // 
            this.tbNb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNb.Location = new System.Drawing.Point(792, 51);
            this.tbNb.Name = "tbNb";
            this.tbNb.Size = new System.Drawing.Size(38, 23);
            this.tbNb.TabIndex = 6;
            this.tbNb.Text = "30";
            // 
            // tbMax
            // 
            this.tbMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMax.Location = new System.Drawing.Point(792, 77);
            this.tbMax.Name = "tbMax";
            this.tbMax.Size = new System.Drawing.Size(38, 23);
            this.tbMax.TabIndex = 8;
            this.tbMax.Text = "s";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(755, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Max";
            // 
            // CayBTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 609);
            this.Controls.Add(this.tbMax);
            this.Controls.Add(this.tbNb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btSupprimer);
            this.Controls.Add(this.btAjouter);
            this.Controls.Add(this.BTreeView);
            this.Name = "CayBTree";
            this.Text = "Cay BTree";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TreeView BTreeView;
    private System.Windows.Forms.Button btAjouter;
    private System.Windows.Forms.Button btSupprimer;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbNb;
    private System.Windows.Forms.TextBox tbMax;
    private System.Windows.Forms.Label label4;
    }
}

