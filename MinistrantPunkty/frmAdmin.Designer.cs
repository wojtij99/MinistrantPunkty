namespace MinistrantPunkty
{
    partial class frmAdmin
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
            this.btnSet = new System.Windows.Forms.Button();
            this.btnPoints = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnEvents = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnSetOFF = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(13, 13);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(114, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Dodaj ministranta";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(133, 13);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(129, 23);
            this.btnSet.TabIndex = 1;
            this.btnSet.Text = "Ustaw obowiązkowe";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnPoints
            // 
            this.btnPoints.Location = new System.Drawing.Point(13, 42);
            this.btnPoints.Name = "btnPoints";
            this.btnPoints.Size = new System.Drawing.Size(114, 23);
            this.btnPoints.TabIndex = 2;
            this.btnPoints.Text = "Punkty Admin";
            this.btnPoints.UseVisualStyleBackColor = true;
            this.btnPoints.Click += new System.EventHandler(this.btnPoints_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(268, 41);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(76, 23);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Wyloguj";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnEvents
            // 
            this.btnEvents.Location = new System.Drawing.Point(133, 72);
            this.btnEvents.Name = "btnEvents";
            this.btnEvents.Size = new System.Drawing.Size(129, 23);
            this.btnEvents.TabIndex = 4;
            this.btnEvents.Text = "Wydarzenia";
            this.btnEvents.UseVisualStyleBackColor = true;
            this.btnEvents.Click += new System.EventHandler(this.btnEvents_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(269, 13);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(13, 72);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(114, 23);
            this.btnHistory.TabIndex = 6;
            this.btnHistory.Text = "Historia";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnSetOFF
            // 
            this.btnSetOFF.Location = new System.Drawing.Point(133, 41);
            this.btnSetOFF.Name = "btnSetOFF";
            this.btnSetOFF.Size = new System.Drawing.Size(129, 23);
            this.btnSetOFF.TabIndex = 7;
            this.btnSetOFF.Text = "Wyłącz obowiązkowe";
            this.btnSetOFF.UseVisualStyleBackColor = true;
            this.btnSetOFF.Click += new System.EventHandler(this.btnSetOFF_Click);
            // 
            // frmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 101);
            this.Controls.Add(this.btnSetOFF);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnEvents);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnPoints);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnAdd);
            this.Name = "frmAdmin";
            this.Text = "Admin";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnPoints;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnEvents;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnSetOFF;
    }
}