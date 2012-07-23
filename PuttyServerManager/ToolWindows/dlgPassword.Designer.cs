namespace PuttyServerManager.ToolWindows {
    partial class dlgPassword {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.imgKey = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgKey)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(87, 42);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '#';
            this.txtPassword.Size = new System.Drawing.Size(256, 20);
            this.txtPassword.TabIndex = 11;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(187, 68);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 10;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(268, 68);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 9;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(84, 21);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(197, 13);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "Please enter your Password here:";
            // 
            // imgKey
            // 
            this.imgKey.Image = global::PuttyServerManager.Properties.Resources.Key1;
            this.imgKey.Location = new System.Drawing.Point(12, 12);
            this.imgKey.Name = "imgKey";
            this.imgKey.Size = new System.Drawing.Size(66, 75);
            this.imgKey.TabIndex = 7;
            this.imgKey.TabStop = false;
            // 
            // dlgPassword
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(356, 102);
            this.ControlBox = false;
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.imgKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "dlgPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter your password";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.dlgPassword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgKey)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.Button cmdCancel;
        internal System.Windows.Forms.Button cmdOK;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.PictureBox imgKey;
    }
}