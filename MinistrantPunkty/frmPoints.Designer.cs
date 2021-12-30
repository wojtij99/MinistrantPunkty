namespace MinistrantPunkty
{
    partial class frmPoints
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
            this.coxUser = new System.Windows.Forms.ComboBox();
            this.labUser = new System.Windows.Forms.Label();
            this.labPoints = new System.Windows.Forms.Label();
            this.txtPoints = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // coxUser
            // 
            this.coxUser.FormattingEnabled = true;
            this.coxUser.Location = new System.Drawing.Point(12, 32);
            this.coxUser.Name = "coxUser";
            this.coxUser.Size = new System.Drawing.Size(121, 21);
            this.coxUser.TabIndex = 0;
            // 
            // labUser
            // 
            this.labUser.AutoSize = true;
            this.labUser.Location = new System.Drawing.Point(13, 13);
            this.labUser.Name = "labUser";
            this.labUser.Size = new System.Drawing.Size(55, 13);
            this.labUser.TabIndex = 1;
            this.labUser.Text = "Ministrant:";
            // 
            // labPoints
            // 
            this.labPoints.AutoSize = true;
            this.labPoints.Location = new System.Drawing.Point(148, 12);
            this.labPoints.Name = "labPoints";
            this.labPoints.Size = new System.Drawing.Size(43, 13);
            this.labPoints.TabIndex = 2;
            this.labPoints.Text = "Punkty:";
            // 
            // txtPoints
            // 
            this.txtPoints.Location = new System.Drawing.Point(151, 32);
            this.txtPoints.Name = "txtPoints";
            this.txtPoints.Size = new System.Drawing.Size(100, 20);
            this.txtPoints.TabIndex = 3;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(12, 60);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 4;
            this.btnExecute.Text = "WYKONAJ!";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // frmPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 90);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.txtPoints);
            this.Controls.Add(this.labPoints);
            this.Controls.Add(this.labUser);
            this.Controls.Add(this.coxUser);
            this.Name = "frmPoints";
            this.Text = "Punkty";
            this.Load += new System.EventHandler(this.frmPoints_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox coxUser;
        private System.Windows.Forms.Label labUser;
        private System.Windows.Forms.Label labPoints;
        private System.Windows.Forms.TextBox txtPoints;
        private System.Windows.Forms.Button btnExecute;
    }
}