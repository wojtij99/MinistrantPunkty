namespace MinistrantPunkty
{
    partial class frmAdd
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.labName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtLastname = new System.Windows.Forms.TextBox();
            this.labLastname = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.labPass = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Namedgw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lastname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.points = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(372, 32);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Dodaj";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labName
            // 
            this.labName.AutoSize = true;
            this.labName.Location = new System.Drawing.Point(12, 9);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(29, 13);
            this.labName.TabIndex = 1;
            this.labName.Text = "Imie:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(91, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(122, 20);
            this.txtName.TabIndex = 2;
            // 
            // txtLastname
            // 
            this.txtLastname.Location = new System.Drawing.Point(311, 6);
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(136, 20);
            this.txtLastname.TabIndex = 4;
            // 
            // labLastname
            // 
            this.labLastname.AutoSize = true;
            this.labLastname.Location = new System.Drawing.Point(232, 9);
            this.labLastname.Name = "labLastname";
            this.labLastname.Size = new System.Drawing.Size(56, 13);
            this.labLastname.TabIndex = 3;
            this.labLastname.Text = "Nazwisko:";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(91, 31);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(122, 20);
            this.txtPass.TabIndex = 6;
            // 
            // labPass
            // 
            this.labPass.AutoSize = true;
            this.labPass.Location = new System.Drawing.Point(12, 34);
            this.labPass.Name = "labPass";
            this.labPass.Size = new System.Drawing.Size(39, 13);
            this.labPass.TabIndex = 5;
            this.labPass.Text = "Hasło:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Namedgw,
            this.Lastname,
            this.pass,
            this.points});
            this.dataGridView1.Location = new System.Drawing.Point(4, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(443, 256);
            this.dataGridView1.TabIndex = 7;
            // 
            // Namedgw
            // 
            this.Namedgw.HeaderText = "Imie";
            this.Namedgw.Name = "Namedgw";
            this.Namedgw.ReadOnly = true;
            // 
            // Lastname
            // 
            this.Lastname.HeaderText = "Nazwisko";
            this.Lastname.Name = "Lastname";
            this.Lastname.ReadOnly = true;
            // 
            // pass
            // 
            this.pass.HeaderText = "Hasło";
            this.pass.Name = "pass";
            this.pass.ReadOnly = true;
            // 
            // points
            // 
            this.points.HeaderText = "Punkty";
            this.points.Name = "points";
            this.points.ReadOnly = true;
            // 
            // frmAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 328);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.labPass);
            this.Controls.Add(this.txtLastname);
            this.Controls.Add(this.labLastname);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.btnAdd);
            this.Name = "frmAdd";
            this.Text = "Dodaj Ministranta";
            this.Load += new System.EventHandler(this.frmAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtLastname;
        private System.Windows.Forms.Label labLastname;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label labPass;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Namedgw;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lastname;
        private System.Windows.Forms.DataGridViewTextBoxColumn pass;
        private System.Windows.Forms.DataGridViewTextBoxColumn points;
    }
}