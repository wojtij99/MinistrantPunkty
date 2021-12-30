namespace MinistrantPunkty
{
    partial class frmZalogowano
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
            this.labWelcome = new System.Windows.Forms.Label();
            this.labPoints = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cPunkty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOpis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // labWelcome
            // 
            this.labWelcome.AutoSize = true;
            this.labWelcome.Location = new System.Drawing.Point(12, 9);
            this.labWelcome.Name = "labWelcome";
            this.labWelcome.Size = new System.Drawing.Size(10, 13);
            this.labWelcome.TabIndex = 0;
            this.labWelcome.Text = ".";
            // 
            // labPoints
            // 
            this.labPoints.AutoSize = true;
            this.labPoints.Location = new System.Drawing.Point(12, 33);
            this.labPoints.Name = "labPoints";
            this.labPoints.Size = new System.Drawing.Size(10, 13);
            this.labPoints.TabIndex = 1;
            this.labPoints.Text = ".";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cPunkty,
            this.cOpis,
            this.cData});
            this.dataGridView1.Location = new System.Drawing.Point(15, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(344, 285);
            this.dataGridView1.TabIndex = 2;
            // 
            // cPunkty
            // 
            this.cPunkty.HeaderText = "Punkty";
            this.cPunkty.Name = "cPunkty";
            this.cPunkty.ReadOnly = true;
            this.cPunkty.Width = 50;
            // 
            // cOpis
            // 
            this.cOpis.HeaderText = "Opis";
            this.cOpis.Name = "cOpis";
            this.cOpis.ReadOnly = true;
            // 
            // cData
            // 
            this.cData.HeaderText = "Data";
            this.cData.Name = "cData";
            this.cData.ReadOnly = true;
            this.cData.Width = 150;
            // 
            // frmZalogowano
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 341);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.labPoints);
            this.Controls.Add(this.labWelcome);
            this.Name = "frmZalogowano";
            this.Text = "Zalogowano";
            this.Load += new System.EventHandler(this.frmZalogowano_Load);
            this.Shown += new System.EventHandler(this.frmZalogowano_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labWelcome;
        private System.Windows.Forms.Label labPoints;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPunkty;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOpis;
        private System.Windows.Forms.DataGridViewTextBoxColumn cData;
    }
}