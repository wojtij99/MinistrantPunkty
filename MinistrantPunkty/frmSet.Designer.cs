namespace MinistrantPunkty
{
    partial class frmSet
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
            this.btnExecute = new System.Windows.Forms.Button();
            this.labUser = new System.Windows.Forms.Label();
            this.coxUser = new System.Windows.Forms.ComboBox();
            this.coxDay = new System.Windows.Forms.ComboBox();
            this.labDay = new System.Windows.Forms.Label();
            this.labHour = new System.Windows.Forms.Label();
            this.coxHour = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Day = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(313, 53);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 7;
            this.btnExecute.Text = "WYKONAJ!";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // labUser
            // 
            this.labUser.AutoSize = true;
            this.labUser.Location = new System.Drawing.Point(13, 7);
            this.labUser.Name = "labUser";
            this.labUser.Size = new System.Drawing.Size(55, 13);
            this.labUser.TabIndex = 6;
            this.labUser.Text = "Ministrant:";
            // 
            // coxUser
            // 
            this.coxUser.FormattingEnabled = true;
            this.coxUser.Location = new System.Drawing.Point(12, 26);
            this.coxUser.Name = "coxUser";
            this.coxUser.Size = new System.Drawing.Size(121, 21);
            this.coxUser.TabIndex = 5;
            this.coxUser.SelectedIndexChanged += new System.EventHandler(this.coxUser_SelectedIndexChanged);
            // 
            // coxDay
            // 
            this.coxDay.AutoCompleteCustomSource.AddRange(new string[] {
            "Poniedziałek",
            "Wtorek",
            "Środa",
            "Czwartek",
            "Piątek",
            "Sobota",
            "Niedziela"});
            this.coxDay.FormattingEnabled = true;
            this.coxDay.Items.AddRange(new object[] {
            "Poniedziałek",
            "Wtorek",
            "Środa",
            "Czwartek",
            "Piątek",
            "Sobota",
            "Niedziela"});
            this.coxDay.Location = new System.Drawing.Point(140, 26);
            this.coxDay.Name = "coxDay";
            this.coxDay.Size = new System.Drawing.Size(121, 21);
            this.coxDay.TabIndex = 8;
            this.coxDay.SelectedIndexChanged += new System.EventHandler(this.coxDay_SelectedIndexChanged);
            // 
            // labDay
            // 
            this.labDay.AutoSize = true;
            this.labDay.Location = new System.Drawing.Point(140, 6);
            this.labDay.Name = "labDay";
            this.labDay.Size = new System.Drawing.Size(37, 13);
            this.labDay.TabIndex = 9;
            this.labDay.Text = "Dzień:";
            // 
            // labHour
            // 
            this.labHour.AutoSize = true;
            this.labHour.Location = new System.Drawing.Point(267, 6);
            this.labHour.Name = "labHour";
            this.labHour.Size = new System.Drawing.Size(49, 13);
            this.labHour.TabIndex = 11;
            this.labHour.Text = "Godzina:";
            // 
            // coxHour
            // 
            this.coxHour.FormattingEnabled = true;
            this.coxHour.Location = new System.Drawing.Point(267, 26);
            this.coxHour.Name = "coxHour";
            this.coxHour.Size = new System.Drawing.Size(121, 21);
            this.coxHour.TabIndex = 10;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Day,
            this.Hour});
            this.dataGridView1.Location = new System.Drawing.Point(12, 82);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(244, 150);
            this.dataGridView1.TabIndex = 12;
            // 
            // Day
            // 
            this.Day.HeaderText = "Dzień";
            this.Day.Name = "Day";
            this.Day.ReadOnly = true;
            // 
            // Hour
            // 
            this.Hour.HeaderText = "Godzina";
            this.Hour.Name = "Hour";
            this.Hour.ReadOnly = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(232, 53);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "Wyczyść";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 251);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.labHour);
            this.Controls.Add(this.coxHour);
            this.Controls.Add(this.labDay);
            this.Controls.Add(this.coxDay);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.labUser);
            this.Controls.Add(this.coxUser);
            this.Name = "frmSet";
            this.Text = "Ustaw obowiązkowe";
            this.Load += new System.EventHandler(this.frmSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Label labUser;
        private System.Windows.Forms.ComboBox coxUser;
        private System.Windows.Forms.ComboBox coxDay;
        private System.Windows.Forms.Label labDay;
        private System.Windows.Forms.Label labHour;
        private System.Windows.Forms.ComboBox coxHour;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Day;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hour;
        private System.Windows.Forms.Button btnClear;
    }
}