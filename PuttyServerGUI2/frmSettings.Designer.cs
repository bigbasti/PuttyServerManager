namespace PuttyServerGUI2 {
    partial class frmSettings {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.tabGeneral = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPuttyAgentParameters = new System.Windows.Forms.TextBox();
            this.chkUsePuttyAgent = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnOpenStoredSessionsFolder = new System.Windows.Forms.Button();
            this.txtStoredSessionsPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblStoredSessions = new System.Windows.Forms.Label();
            this.btnRunConfigurationWizard = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.chkSingleWindow = new System.Windows.Forms.CheckBox();
            this.chkStartWithWindows = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.btnOpenTeamSessionFolder = new System.Windows.Forms.Button();
            this.btnOpenTeamSessionList = new System.Windows.Forms.Button();
            this.txtTeamSessionFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTeamSessionList = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveClose = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.tabGeneral.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.tabPage1);
            this.tabGeneral.Controls.Add(this.tabPage2);
            this.tabGeneral.Location = new System.Drawing.Point(12, 12);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.SelectedIndex = 0;
            this.tabGeneral.Size = new System.Drawing.Size(350, 341);
            this.tabGeneral.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.pictureBox6);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.btnRunConfigurationWizard);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(342, 315);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(32, 249);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(236, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "These settings require a restart of the application";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::PuttyServerGUI2.Properties.Resources.error;
            this.pictureBox6.Location = new System.Drawing.Point(13, 246);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(16, 16);
            this.pictureBox6.TabIndex = 8;
            this.pictureBox6.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.pictureBox4);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.txtPuttyAgentParameters);
            this.groupBox5.Controls.Add(this.chkUsePuttyAgent);
            this.groupBox5.Location = new System.Drawing.Point(6, 157);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(330, 89);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Putty agent";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::PuttyServerGUI2.Properties.Resources.information;
            this.pictureBox4.Location = new System.Drawing.Point(6, 65);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(16, 16);
            this.pictureBox4.TabIndex = 6;
            this.pictureBox4.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(246, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "This setting is optional if you don\'t use Putty Agent.";
            // 
            // txtPuttyAgentParameters
            // 
            this.txtPuttyAgentParameters.Location = new System.Drawing.Point(9, 42);
            this.txtPuttyAgentParameters.Name = "txtPuttyAgentParameters";
            this.txtPuttyAgentParameters.Size = new System.Drawing.Size(292, 20);
            this.txtPuttyAgentParameters.TabIndex = 4;
            // 
            // chkUsePuttyAgent
            // 
            this.chkUsePuttyAgent.AutoSize = true;
            this.chkUsePuttyAgent.Location = new System.Drawing.Point(9, 19);
            this.chkUsePuttyAgent.Name = "chkUsePuttyAgent";
            this.chkUsePuttyAgent.Size = new System.Drawing.Size(228, 17);
            this.chkUsePuttyAgent.TabIndex = 0;
            this.chkUsePuttyAgent.Text = "Start pagent.exe with following parameters:";
            this.chkUsePuttyAgent.UseVisualStyleBackColor = true;
            this.chkUsePuttyAgent.CheckedChanged += new System.EventHandler(this.chkUsePuttyAgent_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnOpenStoredSessionsFolder);
            this.groupBox4.Controls.Add(this.txtStoredSessionsPath);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.pictureBox3);
            this.groupBox4.Controls.Add(this.lblStoredSessions);
            this.groupBox4.Location = new System.Drawing.Point(6, 73);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(330, 78);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Stored Sessions";
            // 
            // btnOpenStoredSessionsFolder
            // 
            this.btnOpenStoredSessionsFolder.Image = global::PuttyServerGUI2.Properties.Resources.folder_explore;
            this.btnOpenStoredSessionsFolder.Location = new System.Drawing.Point(303, 48);
            this.btnOpenStoredSessionsFolder.Name = "btnOpenStoredSessionsFolder";
            this.btnOpenStoredSessionsFolder.Size = new System.Drawing.Size(24, 23);
            this.btnOpenStoredSessionsFolder.TabIndex = 9;
            this.btnOpenStoredSessionsFolder.UseVisualStyleBackColor = true;
            this.btnOpenStoredSessionsFolder.Click += new System.EventHandler(this.btnOpenStoredSessionsFolder_Click);
            // 
            // txtStoredSessionsPath
            // 
            this.txtStoredSessionsPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtStoredSessionsPath.Location = new System.Drawing.Point(9, 51);
            this.txtStoredSessionsPath.Name = "txtStoredSessionsPath";
            this.txtStoredSessionsPath.ReadOnly = true;
            this.txtStoredSessionsPath.Size = new System.Drawing.Size(292, 20);
            this.txtStoredSessionsPath.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Sessions are stored here:";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::PuttyServerGUI2.Properties.Resources.server;
            this.pictureBox3.Location = new System.Drawing.Point(6, 16);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(16, 16);
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // lblStoredSessions
            // 
            this.lblStoredSessions.AutoSize = true;
            this.lblStoredSessions.Location = new System.Drawing.Point(26, 16);
            this.lblStoredSessions.Name = "lblStoredSessions";
            this.lblStoredSessions.Size = new System.Drawing.Size(301, 13);
            this.lblStoredSessions.TabIndex = 2;
            this.lblStoredSessions.Text = "You currently have {0} Sessions stored in your Sessions folder.";
            // 
            // btnRunConfigurationWizard
            // 
            this.btnRunConfigurationWizard.Image = global::PuttyServerGUI2.Properties.Resources.wand;
            this.btnRunConfigurationWizard.Location = new System.Drawing.Point(12, 282);
            this.btnRunConfigurationWizard.Name = "btnRunConfigurationWizard";
            this.btnRunConfigurationWizard.Size = new System.Drawing.Size(194, 27);
            this.btnRunConfigurationWizard.TabIndex = 3;
            this.btnRunConfigurationWizard.Text = "Run the Configuration Wizard";
            this.btnRunConfigurationWizard.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRunConfigurationWizard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRunConfigurationWizard.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox5);
            this.groupBox1.Controls.Add(this.chkSingleWindow);
            this.groupBox1.Controls.Add(this.chkStartWithWindows);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 61);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Settings";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::PuttyServerGUI2.Properties.Resources.error;
            this.pictureBox5.Location = new System.Drawing.Point(311, 38);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(16, 16);
            this.pictureBox5.TabIndex = 2;
            this.pictureBox5.TabStop = false;
            // 
            // chkSingleWindow
            // 
            this.chkSingleWindow.AutoSize = true;
            this.chkSingleWindow.Location = new System.Drawing.Point(6, 38);
            this.chkSingleWindow.Name = "chkSingleWindow";
            this.chkSingleWindow.Size = new System.Drawing.Size(186, 17);
            this.chkSingleWindow.TabIndex = 1;
            this.chkSingleWindow.Text = "Run PSG in Single-Window-Mode";
            this.chkSingleWindow.UseVisualStyleBackColor = true;
            // 
            // chkStartWithWindows
            // 
            this.chkStartWithWindows.AutoSize = true;
            this.chkStartWithWindows.Location = new System.Drawing.Point(6, 19);
            this.chkStartWithWindows.Name = "chkStartWithWindows";
            this.chkStartWithWindows.Size = new System.Drawing.Size(206, 17);
            this.chkStartWithWindows.TabIndex = 0;
            this.chkStartWithWindows.Text = "Start PSG with Windows automatically";
            this.chkStartWithWindows.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.pictureBox9);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(342, 315);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Team";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 244);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(236, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "These settings require a restart of the application";
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = global::PuttyServerGUI2.Properties.Resources.error;
            this.pictureBox9.Location = new System.Drawing.Point(6, 241);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(16, 16);
            this.pictureBox9.TabIndex = 6;
            this.pictureBox9.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtUsername);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.pictureBox2);
            this.groupBox3.Location = new System.Drawing.Point(3, 129);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(327, 106);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "User Settings";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(272, 39);
            this.label6.TabIndex = 12;
            this.label6.Text = "You can leave this setting blank if your team doesn\'t use\r\nplaceholders in the se" +
    "ssion files. You will need to enter\r\nyour username manually then.";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(6, 78);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(315, 20);
            this.txtUsername.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "My team username";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::PuttyServerGUI2.Properties.Resources.information;
            this.pictureBox2.Location = new System.Drawing.Point(6, 19);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox8);
            this.groupBox2.Controls.Add(this.pictureBox7);
            this.groupBox2.Controls.Add(this.btnOpenTeamSessionFolder);
            this.groupBox2.Controls.Add(this.btnOpenTeamSessionList);
            this.groupBox2.Controls.Add(this.txtTeamSessionFolder);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtTeamSessionList);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(3, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(330, 117);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Paths - Team Settings";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::PuttyServerGUI2.Properties.Resources.error;
            this.pictureBox8.Location = new System.Drawing.Point(305, 71);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(16, 16);
            this.pictureBox8.TabIndex = 14;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::PuttyServerGUI2.Properties.Resources.error;
            this.pictureBox7.Location = new System.Drawing.Point(305, 32);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(16, 16);
            this.pictureBox7.TabIndex = 13;
            this.pictureBox7.TabStop = false;
            // 
            // btnOpenTeamSessionFolder
            // 
            this.btnOpenTeamSessionFolder.Image = global::PuttyServerGUI2.Properties.Resources.folder_explore;
            this.btnOpenTeamSessionFolder.Location = new System.Drawing.Point(300, 88);
            this.btnOpenTeamSessionFolder.Name = "btnOpenTeamSessionFolder";
            this.btnOpenTeamSessionFolder.Size = new System.Drawing.Size(24, 23);
            this.btnOpenTeamSessionFolder.TabIndex = 7;
            this.btnOpenTeamSessionFolder.UseVisualStyleBackColor = true;
            this.btnOpenTeamSessionFolder.Click += new System.EventHandler(this.btnOpenTeamSessionFolder_Click);
            // 
            // btnOpenTeamSessionList
            // 
            this.btnOpenTeamSessionList.Image = global::PuttyServerGUI2.Properties.Resources.folder_explore;
            this.btnOpenTeamSessionList.Location = new System.Drawing.Point(300, 48);
            this.btnOpenTeamSessionList.Name = "btnOpenTeamSessionList";
            this.btnOpenTeamSessionList.Size = new System.Drawing.Size(24, 23);
            this.btnOpenTeamSessionList.TabIndex = 6;
            this.btnOpenTeamSessionList.UseVisualStyleBackColor = true;
            this.btnOpenTeamSessionList.Click += new System.EventHandler(this.btnOpenTeamSessionList_Click);
            // 
            // txtTeamSessionFolder
            // 
            this.txtTeamSessionFolder.Location = new System.Drawing.Point(6, 90);
            this.txtTeamSessionFolder.Name = "txtTeamSessionFolder";
            this.txtTeamSessionFolder.Size = new System.Drawing.Size(292, 20);
            this.txtTeamSessionFolder.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Path to Team Session folder";
            // 
            // txtTeamSessionList
            // 
            this.txtTeamSessionList.Location = new System.Drawing.Point(6, 51);
            this.txtTeamSessionList.Name = "txtTeamSessionList";
            this.txtTeamSessionList.Size = new System.Drawing.Size(292, 20);
            this.txtTeamSessionList.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Path to Team Session list file";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PuttyServerGUI2.Properties.Resources.information;
            this.pictureBox1.Location = new System.Drawing.Point(6, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(298, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "This configuration is only needed when you use a team setup!";
            // 
            // btnSaveClose
            // 
            this.btnSaveClose.Location = new System.Drawing.Point(242, 359);
            this.btnSaveClose.Name = "btnSaveClose";
            this.btnSaveClose.Size = new System.Drawing.Size(116, 23);
            this.btnSaveClose.TabIndex = 1;
            this.btnSaveClose.Text = "Save && Close";
            this.btnSaveClose.UseVisualStyleBackColor = true;
            this.btnSaveClose.Click += new System.EventHandler(this.btnSaveClose_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(13, 364);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(104, 13);
            this.lblVersion.TabIndex = 6;
            this.lblVersion.Text = "Software Version {0}";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 389);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnSaveClose);
            this.Controls.Add(this.tabGeneral);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.tabGeneral.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabGeneral;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnSaveClose;
        private System.Windows.Forms.CheckBox chkStartWithWindows;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRunConfigurationWizard;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOpenTeamSessionFolder;
        private System.Windows.Forms.Button btnOpenTeamSessionList;
        private System.Windows.Forms.TextBox txtTeamSessionFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTeamSessionList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblStoredSessions;
        private System.Windows.Forms.Button btnOpenStoredSessionsFolder;
        private System.Windows.Forms.TextBox txtStoredSessionsPath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtPuttyAgentParameters;
        private System.Windows.Forms.CheckBox chkUsePuttyAgent;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.CheckBox chkSingleWindow;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox9;
    }
}