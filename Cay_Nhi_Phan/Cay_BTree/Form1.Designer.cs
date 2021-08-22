
namespace Cay_BTree
{
    partial class frmBTree
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
            this.BTreeView = new System.Windows.Forms.TreeView();
            this.tbNb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbMax = new System.Windows.Forms.TextBox();
            this.btAjouter = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BTreeView
            // 
            this.BTreeView.Location = new System.Drawing.Point(12, 12);
            this.BTreeView.Name = "BTreeView";
            this.BTreeView.Size = new System.Drawing.Size(920, 658);
            this.BTreeView.TabIndex = 0;
            // 
            // tbNb
            // 
            this.tbNb.Location = new System.Drawing.Point(1008, 76);
            this.tbNb.Name = "tbNb";
            this.tbNb.Size = new System.Drawing.Size(67, 22);
            this.tbNb.TabIndex = 1;
            this.tbNb.Text = "30";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(956, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Min";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(956, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Max";
            // 
            // tbMax
            // 
            this.tbMax.Location = new System.Drawing.Point(1008, 123);
            this.tbMax.Name = "tbMax";
            this.tbMax.Size = new System.Drawing.Size(67, 22);
            this.tbMax.TabIndex = 3;
            this.tbMax.Text = "100";
            // 
            // btAjouter
            // 
            this.btAjouter.Location = new System.Drawing.Point(980, 169);
            this.btAjouter.Name = "btAjouter";
            this.btAjouter.Size = new System.Drawing.Size(75, 49);
            this.btAjouter.TabIndex = 5;
            this.btAjouter.Text = "Tạo mới";
            this.btAjouter.UseVisualStyleBackColor = true;
            this.btAjouter.Click += new System.EventHandler(this.btAjouter_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(980, 224);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 48);
            this.btnXoa.TabIndex = 6;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // frmBTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 682);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btAjouter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbMax);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNb);
            this.Controls.Add(this.BTreeView);
            this.Name = "frmBTree";
            this.Text = "Cây BTree";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView BTreeView;
        private System.Windows.Forms.TextBox tbNb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMax;
        private System.Windows.Forms.Button btAjouter;
        private System.Windows.Forms.Button btnXoa;
    }
}

