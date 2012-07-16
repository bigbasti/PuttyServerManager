namespace PuttyServerManager.ToolWindows {
    partial class twiPutty {
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
            this.components = new System.ComponentModel.Container();
            this.conMenuSessionTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conMenuSessionTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // conMenuSessionTab
            // 
            this.conMenuSessionTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeSessionToolStripMenuItem,
            this.cloneSessionToolStripMenuItem,
            this.toolStripMenuItem1,
            this.renameTabToolStripMenuItem});
            this.conMenuSessionTab.Name = "conMenuSessionTab";
            this.conMenuSessionTab.Size = new System.Drawing.Size(153, 98);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // closeSessionToolStripMenuItem
            // 
            this.closeSessionToolStripMenuItem.Image = global::PuttyServerManager.Properties.Resources.tab_delete;
            this.closeSessionToolStripMenuItem.Name = "closeSessionToolStripMenuItem";
            this.closeSessionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeSessionToolStripMenuItem.Text = "Close Session";
            this.closeSessionToolStripMenuItem.Click += new System.EventHandler(this.closeSessionToolStripMenuItem_Click);
            // 
            // cloneSessionToolStripMenuItem
            // 
            this.cloneSessionToolStripMenuItem.Image = global::PuttyServerManager.Properties.Resources.application_double;
            this.cloneSessionToolStripMenuItem.Name = "cloneSessionToolStripMenuItem";
            this.cloneSessionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cloneSessionToolStripMenuItem.Text = "Clone Session";
            this.cloneSessionToolStripMenuItem.Click += new System.EventHandler(this.cloneSessionToolStripMenuItem_Click);
            // 
            // renameTabToolStripMenuItem
            // 
            this.renameTabToolStripMenuItem.Image = global::PuttyServerManager.Properties.Resources.tab_edit;
            this.renameTabToolStripMenuItem.Name = "renameTabToolStripMenuItem";
            this.renameTabToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.renameTabToolStripMenuItem.Text = "Rename Tab";
            this.renameTabToolStripMenuItem.Click += new System.EventHandler(this.renameTabToolStripMenuItem_Click);
            // 
            // twiPutty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 419);
            this.Name = "twiPutty";
            this.Text = "Putty";
            this.Load += new System.EventHandler(this.twiPutty_Load);
            this.conMenuSessionTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip conMenuSessionTab;
        private System.Windows.Forms.ToolStripMenuItem closeSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloneSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem renameTabToolStripMenuItem;
    }
}