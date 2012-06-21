namespace PuttyServerGUI2.ToolWindows {
    partial class twiSessions {
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
            this.tabSessionAreas = new System.Windows.Forms.TabControl();
            this.tabSessions = new System.Windows.Forms.TabPage();
            this.tabPuttySessions = new System.Windows.Forms.TabPage();
            this.trvSessions = new System.Windows.Forms.TreeView();
            this.tabSessionAreas.SuspendLayout();
            this.tabSessions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSessionAreas
            // 
            this.tabSessionAreas.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabSessionAreas.Controls.Add(this.tabSessions);
            this.tabSessionAreas.Controls.Add(this.tabPuttySessions);
            this.tabSessionAreas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSessionAreas.Location = new System.Drawing.Point(0, 0);
            this.tabSessionAreas.Name = "tabSessionAreas";
            this.tabSessionAreas.SelectedIndex = 0;
            this.tabSessionAreas.Size = new System.Drawing.Size(631, 474);
            this.tabSessionAreas.TabIndex = 0;
            // 
            // tabSessions
            // 
            this.tabSessions.Controls.Add(this.trvSessions);
            this.tabSessions.Location = new System.Drawing.Point(4, 4);
            this.tabSessions.Name = "tabSessions";
            this.tabSessions.Padding = new System.Windows.Forms.Padding(3);
            this.tabSessions.Size = new System.Drawing.Size(623, 448);
            this.tabSessions.TabIndex = 0;
            this.tabSessions.Text = "Sessions";
            this.tabSessions.UseVisualStyleBackColor = true;
            // 
            // tabPuttySessions
            // 
            this.tabPuttySessions.Location = new System.Drawing.Point(4, 4);
            this.tabPuttySessions.Name = "tabPuttySessions";
            this.tabPuttySessions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPuttySessions.Size = new System.Drawing.Size(623, 448);
            this.tabPuttySessions.TabIndex = 1;
            this.tabPuttySessions.Text = "Putty";
            this.tabPuttySessions.UseVisualStyleBackColor = true;
            // 
            // trvSessions
            // 
            this.trvSessions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvSessions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvSessions.Location = new System.Drawing.Point(3, 3);
            this.trvSessions.Name = "trvSessions";
            this.trvSessions.Size = new System.Drawing.Size(617, 442);
            this.trvSessions.TabIndex = 0;
            // 
            // twiSessions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 474);
            this.Controls.Add(this.tabSessionAreas);
            this.Name = "twiSessions";
            this.Text = "Sessions";
            this.Load += new System.EventHandler(this.twiSessions_Load);
            this.tabSessionAreas.ResumeLayout(false);
            this.tabSessions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabSessionAreas;
        private System.Windows.Forms.TabPage tabSessions;
        private System.Windows.Forms.TabPage tabPuttySessions;
        private System.Windows.Forms.TreeView trvSessions;

    }
}