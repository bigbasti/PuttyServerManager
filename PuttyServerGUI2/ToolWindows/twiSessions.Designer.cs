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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Server", 6, 6);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Server", 6, 6);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Umgebung", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Server", 6, 6);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Noch nen Server", 6, 6);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Äin Süperserver", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Projekt", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Sessions", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode7});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(twiSessions));
            this.tabSessionAreas = new System.Windows.Forms.TabControl();
            this.tabSessions = new System.Windows.Forms.TabPage();
            this.tabPuttySessions = new System.Windows.Forms.TabPage();
            this.trvSessions = new System.Windows.Forms.TreeView();
            this.imgSessionImages = new System.Windows.Forms.ImageList(this.components);
            this.tabRecentSessions = new System.Windows.Forms.TabPage();
            this.conMenuSession = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.startSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startColoredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSessionAreas.SuspendLayout();
            this.tabSessions.SuspendLayout();
            this.conMenuSession.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSessionAreas
            // 
            this.tabSessionAreas.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabSessionAreas.Controls.Add(this.tabSessions);
            this.tabSessionAreas.Controls.Add(this.tabPuttySessions);
            this.tabSessionAreas.Controls.Add(this.tabRecentSessions);
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
            this.trvSessions.ImageIndex = 0;
            this.trvSessions.ImageList = this.imgSessionImages;
            this.trvSessions.Location = new System.Drawing.Point(3, 3);
            this.trvSessions.Name = "trvSessions";
            treeNode1.ImageIndex = 6;
            treeNode1.Name = "Knoten1";
            treeNode1.SelectedImageIndex = 6;
            treeNode1.Text = "Server";
            treeNode2.ImageIndex = 6;
            treeNode2.Name = "Knoten5";
            treeNode2.SelectedImageIndex = 6;
            treeNode2.Text = "Server";
            treeNode3.ImageIndex = 1;
            treeNode3.Name = "Knoten4";
            treeNode3.SelectedImageIndex = 1;
            treeNode3.Text = "Umgebung";
            treeNode4.ImageIndex = 6;
            treeNode4.Name = "Knoten6";
            treeNode4.SelectedImageIndex = 6;
            treeNode4.Text = "Server";
            treeNode5.ImageIndex = 6;
            treeNode5.Name = "Knoten8";
            treeNode5.SelectedImageIndex = 6;
            treeNode5.Text = "Noch nen Server";
            treeNode6.ImageIndex = 1;
            treeNode6.Name = "Knoten7";
            treeNode6.SelectedImageIndex = 1;
            treeNode6.Text = "Äin Süperserver";
            treeNode7.ImageIndex = 1;
            treeNode7.Name = "Knoten3";
            treeNode7.SelectedImageIndex = 1;
            treeNode7.Text = "Projekt";
            treeNode8.Name = "Knoten0";
            treeNode8.Text = "Sessions";
            this.trvSessions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8});
            this.trvSessions.SelectedImageIndex = 0;
            this.trvSessions.Size = new System.Drawing.Size(617, 442);
            this.trvSessions.TabIndex = 0;
            this.trvSessions.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trvSessions_MouseClick);
            // 
            // imgSessionImages
            // 
            this.imgSessionImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgSessionImages.ImageStream")));
            this.imgSessionImages.TransparentColor = System.Drawing.Color.Transparent;
            this.imgSessionImages.Images.SetKeyName(0, "computer.png");
            this.imgSessionImages.Images.SetKeyName(1, "folder.png");
            this.imgSessionImages.Images.SetKeyName(2, "folder_add.png");
            this.imgSessionImages.Images.SetKeyName(3, "folder_delete.png");
            this.imgSessionImages.Images.SetKeyName(4, "folder_error.png");
            this.imgSessionImages.Images.SetKeyName(5, "folder_explore.png");
            this.imgSessionImages.Images.SetKeyName(6, "server.png");
            this.imgSessionImages.Images.SetKeyName(7, "server_add.png");
            this.imgSessionImages.Images.SetKeyName(8, "server_delete.png");
            this.imgSessionImages.Images.SetKeyName(9, "server_error.png");
            this.imgSessionImages.Images.SetKeyName(10, "server_go.png");
            // 
            // tabRecentSessions
            // 
            this.tabRecentSessions.Location = new System.Drawing.Point(4, 4);
            this.tabRecentSessions.Name = "tabRecentSessions";
            this.tabRecentSessions.Size = new System.Drawing.Size(623, 448);
            this.tabRecentSessions.TabIndex = 2;
            this.tabRecentSessions.Text = "Recent";
            this.tabRecentSessions.UseVisualStyleBackColor = true;
            // 
            // conMenuSession
            // 
            this.conMenuSession.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startSessionToolStripMenuItem,
            this.startColoredToolStripMenuItem,
            this.toolStripMenuItem1,
            this.editSessionToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem2,
            this.refreshSessionToolStripMenuItem});
            this.conMenuSession.Name = "conMenuSession";
            this.conMenuSession.Size = new System.Drawing.Size(156, 148);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 6);
            // 
            // startSessionToolStripMenuItem
            // 
            this.startSessionToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.server_go;
            this.startSessionToolStripMenuItem.Name = "startSessionToolStripMenuItem";
            this.startSessionToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.startSessionToolStripMenuItem.Text = "Start Session";
            // 
            // startColoredToolStripMenuItem
            // 
            this.startColoredToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.color_wheel;
            this.startColoredToolStripMenuItem.Name = "startColoredToolStripMenuItem";
            this.startColoredToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.startColoredToolStripMenuItem.Text = "Start Colored";
            // 
            // editSessionToolStripMenuItem
            // 
            this.editSessionToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.server_edit;
            this.editSessionToolStripMenuItem.Name = "editSessionToolStripMenuItem";
            this.editSessionToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.editSessionToolStripMenuItem.Text = "Edit";
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.server_edit1;
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // refreshSessionToolStripMenuItem
            // 
            this.refreshSessionToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.arrow_refresh;
            this.refreshSessionToolStripMenuItem.Name = "refreshSessionToolStripMenuItem";
            this.refreshSessionToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.refreshSessionToolStripMenuItem.Text = "Refresh Session";
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
            this.conMenuSession.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabSessionAreas;
        private System.Windows.Forms.TabPage tabSessions;
        private System.Windows.Forms.TabPage tabPuttySessions;
        private System.Windows.Forms.TreeView trvSessions;
        private System.Windows.Forms.ImageList imgSessionImages;
        private System.Windows.Forms.TabPage tabRecentSessions;
        private System.Windows.Forms.ContextMenuStrip conMenuSession;
        private System.Windows.Forms.ToolStripMenuItem startSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startColoredToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem refreshSessionToolStripMenuItem;

    }
}