namespace MinistrantKatedra3
{
    partial class frmLoged
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoged));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cPunkty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOpis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labPoints = new System.Windows.Forms.Label();
            this.labWelcome = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cPunkty,
            this.cOpis,
            this.cData});
            this.dataGridView1.Location = new System.Drawing.Point(13, 74);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.Size = new System.Drawing.Size(516, 438);
            this.dataGridView1.TabIndex = 8;
            // 
            // cPunkty
            // 
            this.cPunkty.HeaderText = "Punkty";
            this.cPunkty.MinimumWidth = 8;
            this.cPunkty.Name = "cPunkty";
            this.cPunkty.ReadOnly = true;
            this.cPunkty.Width = 50;
            // 
            // cOpis
            // 
            this.cOpis.HeaderText = "Opis";
            this.cOpis.MinimumWidth = 8;
            this.cOpis.Name = "cOpis";
            this.cOpis.ReadOnly = true;
            this.cOpis.Width = 150;
            // 
            // cData
            // 
            this.cData.HeaderText = "Data";
            this.cData.MinimumWidth = 8;
            this.cData.Name = "cData";
            this.cData.ReadOnly = true;
            this.cData.Width = 150;
            // 
            // labPoints
            // 
            this.labPoints.AutoSize = true;
            this.labPoints.Location = new System.Drawing.Point(9, 48);
            this.labPoints.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labPoints.Name = "labPoints";
            this.labPoints.Size = new System.Drawing.Size(13, 20);
            this.labPoints.TabIndex = 7;
            this.labPoints.Text = ".";
            // 
            // labWelcome
            // 
            this.labWelcome.AutoSize = true;
            this.labWelcome.Location = new System.Drawing.Point(9, 11);
            this.labWelcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labWelcome.Name = "labWelcome";
            this.labWelcome.Size = new System.Drawing.Size(13, 20);
            this.labWelcome.TabIndex = 6;
            this.labWelcome.Text = ".";
            // 
            // frmLoged
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 521);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.labPoints);
            this.Controls.Add(this.labWelcome);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLoged";
            this.Text = "Zalogowano";
            this.Load += new System.EventHandler(this.frmLoged_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPunkty;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOpis;
        private System.Windows.Forms.DataGridViewTextBoxColumn cData;
        private System.Windows.Forms.Label labPoints;
        private System.Windows.Forms.Label labWelcome;
    }
}