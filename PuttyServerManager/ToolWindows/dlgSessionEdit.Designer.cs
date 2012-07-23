namespace PuttyServerManager.ToolWindows {
    partial class dlgSessionEdit {
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
            this.lblCaption = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBackColor = new System.Windows.Forms.Button();
            this.btnTextColor = new System.Windows.Forms.Button();
            this.txtColorDemo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRemoteCommand = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHostname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveClose = new System.Windows.Forms.Button();
            this.btnEditInEditor = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.Location = new System.Drawing.Point(66, 12);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(124, 17);
            this.lblCaption.TabIndex = 1;
            this.lblCaption.Text = "Edit Session {0}";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(337, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "All changes you make here will be written into the session permanently";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBackColor);
            this.groupBox1.Controls.Add(this.btnTextColor);
            this.groupBox1.Controls.Add(this.txtColorDemo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtRemoteCommand);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtHostname);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(69, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 209);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // btnBackColor
            // 
            this.btnBackColor.Location = new System.Drawing.Point(257, 176);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(75, 23);
            this.btnBackColor.TabIndex = 22;
            this.btnBackColor.Text = "&Backcolor";
            this.btnBackColor.UseVisualStyleBackColor = true;
            this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            // 
            // btnTextColor
            // 
            this.btnTextColor.Location = new System.Drawing.Point(176, 176);
            this.btnTextColor.Name = "btnTextColor";
            this.btnTextColor.Size = new System.Drawing.Size(75, 23);
            this.btnTextColor.TabIndex = 21;
            this.btnTextColor.Text = "&Textcolor";
            this.btnTextColor.UseVisualStyleBackColor = true;
            this.btnTextColor.Click += new System.EventHandler(this.btnTextColor_Click);
            // 
            // txtColorDemo
            // 
            this.txtColorDemo.Location = new System.Drawing.Point(9, 178);
            this.txtColorDemo.Name = "txtColorDemo";
            this.txtColorDemo.Size = new System.Drawing.Size(161, 20);
            this.txtColorDemo.TabIndex = 20;
            this.txtColorDemo.Text = "This is a demotext";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Colors";
            // 
            // txtRemoteCommand
            // 
            this.txtRemoteCommand.Location = new System.Drawing.Point(9, 129);
            this.txtRemoteCommand.Name = "txtRemoteCommand";
            this.txtRemoteCommand.Size = new System.Drawing.Size(325, 20);
            this.txtRemoteCommand.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "RemoteCommand";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(9, 80);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(325, 20);
            this.txtUserName.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "UserName";
            // 
            // txtHostname
            // 
            this.txtHostname.Location = new System.Drawing.Point(9, 32);
            this.txtHostname.Name = "txtHostname";
            this.txtHostname.Size = new System.Drawing.Size(325, 20);
            this.txtHostname.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "HostName";
            // 
            // btnSaveClose
            // 
            this.btnSaveClose.Image = global::PuttyServerManager.Properties.Resources.disk;
            this.btnSaveClose.Location = new System.Drawing.Point(295, 277);
            this.btnSaveClose.Name = "btnSaveClose";
            this.btnSaveClose.Size = new System.Drawing.Size(115, 29);
            this.btnSaveClose.TabIndex = 23;
            this.btnSaveClose.Text = "&Save && Close";
            this.btnSaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveClose.UseVisualStyleBackColor = true;
            this.btnSaveClose.Click += new System.EventHandler(this.btnSaveClose_Click);
            // 
            // btnEditInEditor
            // 
            this.btnEditInEditor.Image = global::PuttyServerManager.Properties.Resources.page_edit;
            this.btnEditInEditor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditInEditor.Location = new System.Drawing.Point(69, 277);
            this.btnEditInEditor.Name = "btnEditInEditor";
            this.btnEditInEditor.Size = new System.Drawing.Size(170, 29);
            this.btnEditInEditor.TabIndex = 22;
            this.btnEditInEditor.Text = "&Open Session in Editor";
            this.btnEditInEditor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditInEditor.UseVisualStyleBackColor = true;
            this.btnEditInEditor.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PuttyServerManager.Properties.Resources.Edit;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // dlgSessionEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 319);
            this.Controls.Add(this.btnSaveClose);
            this.Controls.Add(this.btnEditInEditor);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgSessionEdit";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Session";
            this.Load += new System.EventHandler(this.dlgSessionEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBackColor;
        private System.Windows.Forms.Button btnTextColor;
        private System.Windows.Forms.TextBox txtColorDemo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRemoteCommand;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHostname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEditInEditor;
        private System.Windows.Forms.Button btnSaveClose;
    }
}