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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(twiSessions));
            this.tabSessionAreas = new System.Windows.Forms.TabControl();
            this.tabPersonal = new System.Windows.Forms.TabPage();
            this.trvSessions = new System.Windows.Forms.TreeView();
            this.imgSessionImages = new System.Windows.Forms.ImageList(this.components);
            this.tabTeam = new System.Windows.Forms.TabPage();
            this.trvTeam = new System.Windows.Forms.TreeView();
            this.tabPuttySessions = new System.Windows.Forms.TabPage();
            this.tabRecentSessions = new System.Windows.Forms.TabPage();
            this.trvRecentSessions = new System.Windows.Forms.TreeView();
            this.conMenuSession = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startColoredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.editSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conMenuFolder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.addSubfolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.conMenuTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addSessionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addSubfolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.renameToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.reloadOverviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortOverviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.expandAllFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conMenuSessionMissing = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeMissingSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeMissingSessionFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conMenuRecent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startSessionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.startColoredSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.transferSessionToPersonalSessionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadSessionLisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.removeSessionFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conMenuRemoveMissingRecent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeMissingSessionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.removeAllMissingSessionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.reloadSessionListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conMenuTeamSession = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startSessionToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.startSessionColoredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.transferSessionToPersonalListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSessionAreas.SuspendLayout();
            this.tabPersonal.SuspendLayout();
            this.tabTeam.SuspendLayout();
            this.tabRecentSessions.SuspendLayout();
            this.conMenuSession.SuspendLayout();
            this.conMenuFolder.SuspendLayout();
            this.conMenuTreeView.SuspendLayout();
            this.conMenuSessionMissing.SuspendLayout();
            this.conMenuRecent.SuspendLayout();
            this.conMenuRemoveMissingRecent.SuspendLayout();
            this.conMenuTeamSession.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSessionAreas
            // 
            this.tabSessionAreas.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabSessionAreas.Controls.Add(this.tabPersonal);
            this.tabSessionAreas.Controls.Add(this.tabTeam);
            this.tabSessionAreas.Controls.Add(this.tabPuttySessions);
            this.tabSessionAreas.Controls.Add(this.tabRecentSessions);
            this.tabSessionAreas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSessionAreas.Location = new System.Drawing.Point(0, 0);
            this.tabSessionAreas.Name = "tabSessionAreas";
            this.tabSessionAreas.SelectedIndex = 0;
            this.tabSessionAreas.Size = new System.Drawing.Size(631, 474);
            this.tabSessionAreas.TabIndex = 0;
            // 
            // tabPersonal
            // 
            this.tabPersonal.Controls.Add(this.trvSessions);
            this.tabPersonal.Location = new System.Drawing.Point(4, 4);
            this.tabPersonal.Name = "tabPersonal";
            this.tabPersonal.Padding = new System.Windows.Forms.Padding(3);
            this.tabPersonal.Size = new System.Drawing.Size(623, 448);
            this.tabPersonal.TabIndex = 0;
            this.tabPersonal.Text = "Personal";
            this.tabPersonal.UseVisualStyleBackColor = true;
            // 
            // trvSessions
            // 
            this.trvSessions.AllowDrop = true;
            this.trvSessions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvSessions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvSessions.ImageIndex = 0;
            this.trvSessions.ImageList = this.imgSessionImages;
            this.trvSessions.LabelEdit = true;
            this.trvSessions.Location = new System.Drawing.Point(3, 3);
            this.trvSessions.Name = "trvSessions";
            this.trvSessions.SelectedImageIndex = 0;
            this.trvSessions.Size = new System.Drawing.Size(617, 442);
            this.trvSessions.TabIndex = 0;
            this.trvSessions.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.trvSessions_AfterLabelEdit);
            this.trvSessions.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvSessions_BeforeCheck);
            this.trvSessions.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.trvSessions_ItemDrag);
            this.trvSessions.DragDrop += new System.Windows.Forms.DragEventHandler(this.trvSessions_DragDrop);
            this.trvSessions.DragEnter += new System.Windows.Forms.DragEventHandler(this.trvSessions_DragEnter);
            this.trvSessions.DoubleClick += new System.EventHandler(this.trvSessions_DoubleClick);
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
            // tabTeam
            // 
            this.tabTeam.Controls.Add(this.trvTeam);
            this.tabTeam.Location = new System.Drawing.Point(4, 4);
            this.tabTeam.Name = "tabTeam";
            this.tabTeam.Size = new System.Drawing.Size(623, 448);
            this.tabTeam.TabIndex = 3;
            this.tabTeam.Text = "Team";
            this.tabTeam.UseVisualStyleBackColor = true;
            // 
            // trvTeam
            // 
            this.trvTeam.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvTeam.ImageIndex = 0;
            this.trvTeam.ImageList = this.imgSessionImages;
            this.trvTeam.Location = new System.Drawing.Point(0, 0);
            this.trvTeam.Name = "trvTeam";
            this.trvTeam.SelectedImageIndex = 0;
            this.trvTeam.Size = new System.Drawing.Size(623, 448);
            this.trvTeam.TabIndex = 0;
            this.trvTeam.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvTeam_AfterSelect);
            this.trvTeam.DoubleClick += new System.EventHandler(this.trvTeam_DoubleClick);
            this.trvTeam.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trvTeam_MouseClick);
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
            // tabRecentSessions
            // 
            this.tabRecentSessions.Controls.Add(this.trvRecentSessions);
            this.tabRecentSessions.Location = new System.Drawing.Point(4, 4);
            this.tabRecentSessions.Name = "tabRecentSessions";
            this.tabRecentSessions.Size = new System.Drawing.Size(623, 448);
            this.tabRecentSessions.TabIndex = 2;
            this.tabRecentSessions.Text = "Recent";
            this.tabRecentSessions.UseVisualStyleBackColor = true;
            // 
            // trvRecentSessions
            // 
            this.trvRecentSessions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvRecentSessions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvRecentSessions.ImageIndex = 0;
            this.trvRecentSessions.ImageList = this.imgSessionImages;
            this.trvRecentSessions.Location = new System.Drawing.Point(0, 0);
            this.trvRecentSessions.Name = "trvRecentSessions";
            this.trvRecentSessions.SelectedImageIndex = 0;
            this.trvRecentSessions.Size = new System.Drawing.Size(623, 448);
            this.trvRecentSessions.TabIndex = 0;
            this.trvRecentSessions.DoubleClick += new System.EventHandler(this.trvRecentSessions_DoubleClick);
            this.trvRecentSessions.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trvRecentSessions_MouseClick);
            // 
            // conMenuSession
            // 
            this.conMenuSession.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startSessionToolStripMenuItem,
            this.startColoredToolStripMenuItem,
            this.toolStripMenuItem1,
            this.editSessionToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.conMenuSession.Name = "conMenuSession";
            this.conMenuSession.Size = new System.Drawing.Size(144, 120);
            // 
            // startSessionToolStripMenuItem
            // 
            this.startSessionToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.server_go;
            this.startSessionToolStripMenuItem.Name = "startSessionToolStripMenuItem";
            this.startSessionToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.startSessionToolStripMenuItem.Text = "Start Session";
            this.startSessionToolStripMenuItem.Click += new System.EventHandler(this.startSessionToolStripMenuItem_Click);
            // 
            // startColoredToolStripMenuItem
            // 
            this.startColoredToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.color_wheel;
            this.startColoredToolStripMenuItem.Name = "startColoredToolStripMenuItem";
            this.startColoredToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.startColoredToolStripMenuItem.Text = "Start Colored";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(140, 6);
            // 
            // editSessionToolStripMenuItem
            // 
            this.editSessionToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.server_edit;
            this.editSessionToolStripMenuItem.Name = "editSessionToolStripMenuItem";
            this.editSessionToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.editSessionToolStripMenuItem.Text = "Edit";
            this.editSessionToolStripMenuItem.Click += new System.EventHandler(this.editSessionToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.server_edit1;
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // conMenuFolder
            // 
            this.conMenuFolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSessionToolStripMenuItem,
            this.toolStripMenuItem3,
            this.addSubfolderToolStripMenuItem,
            this.renameToolStripMenuItem1,
            this.deleteToolStripMenuItem1});
            this.conMenuFolder.Name = "contextMenuFolder";
            this.conMenuFolder.Size = new System.Drawing.Size(151, 98);
            // 
            // addSessionToolStripMenuItem
            // 
            this.addSessionToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.server_add;
            this.addSessionToolStripMenuItem.Name = "addSessionToolStripMenuItem";
            this.addSessionToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.addSessionToolStripMenuItem.Text = "Add Session";
            this.addSessionToolStripMenuItem.Click += new System.EventHandler(this.addSessionToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(147, 6);
            // 
            // addSubfolderToolStripMenuItem
            // 
            this.addSubfolderToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.folder_add;
            this.addSubfolderToolStripMenuItem.Name = "addSubfolderToolStripMenuItem";
            this.addSubfolderToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.addSubfolderToolStripMenuItem.Text = "Add Subfolder";
            this.addSubfolderToolStripMenuItem.Click += new System.EventHandler(this.addSubfolderToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem1
            // 
            this.renameToolStripMenuItem1.Image = global::PuttyServerGUI2.Properties.Resources.folder_edit;
            this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
            this.renameToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.renameToolStripMenuItem1.Text = "Rename";
            this.renameToolStripMenuItem1.Click += new System.EventHandler(this.renameToolStripMenuItem1_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Image = global::PuttyServerGUI2.Properties.Resources.folder_delete;
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // conMenuTreeView
            // 
            this.conMenuTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSessionToolStripMenuItem1,
            this.addSubfolderToolStripMenuItem1,
            this.toolStripMenuItem5,
            this.renameToolStripMenuItem2,
            this.toolStripMenuItem6,
            this.reloadOverviewToolStripMenuItem,
            this.sortOverviewToolStripMenuItem,
            this.toolStripMenuItem4,
            this.expandAllFoldersToolStripMenuItem,
            this.collapseAllFoldersToolStripMenuItem});
            this.conMenuTreeView.Name = "conMenuTreeView";
            this.conMenuTreeView.Size = new System.Drawing.Size(176, 176);
            // 
            // addSessionToolStripMenuItem1
            // 
            this.addSessionToolStripMenuItem1.Image = global::PuttyServerGUI2.Properties.Resources.server_add;
            this.addSessionToolStripMenuItem1.Name = "addSessionToolStripMenuItem1";
            this.addSessionToolStripMenuItem1.Size = new System.Drawing.Size(175, 22);
            this.addSessionToolStripMenuItem1.Text = "Add Session";
            this.addSessionToolStripMenuItem1.Click += new System.EventHandler(this.addSessionToolStripMenuItem1_Click);
            // 
            // addSubfolderToolStripMenuItem1
            // 
            this.addSubfolderToolStripMenuItem1.Image = global::PuttyServerGUI2.Properties.Resources.folder_add;
            this.addSubfolderToolStripMenuItem1.Name = "addSubfolderToolStripMenuItem1";
            this.addSubfolderToolStripMenuItem1.Size = new System.Drawing.Size(175, 22);
            this.addSubfolderToolStripMenuItem1.Text = "Add Subfolder";
            this.addSubfolderToolStripMenuItem1.Click += new System.EventHandler(this.addSubfolderToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(172, 6);
            // 
            // renameToolStripMenuItem2
            // 
            this.renameToolStripMenuItem2.Image = global::PuttyServerGUI2.Properties.Resources.computer_edit;
            this.renameToolStripMenuItem2.Name = "renameToolStripMenuItem2";
            this.renameToolStripMenuItem2.Size = new System.Drawing.Size(175, 22);
            this.renameToolStripMenuItem2.Text = "Rename";
            this.renameToolStripMenuItem2.Click += new System.EventHandler(this.renameToolStripMenuItem2_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(172, 6);
            // 
            // reloadOverviewToolStripMenuItem
            // 
            this.reloadOverviewToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.arrow_refresh;
            this.reloadOverviewToolStripMenuItem.Name = "reloadOverviewToolStripMenuItem";
            this.reloadOverviewToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.reloadOverviewToolStripMenuItem.Text = "Reload Overview";
            this.reloadOverviewToolStripMenuItem.Click += new System.EventHandler(this.reloadOverviewToolStripMenuItem_Click);
            // 
            // sortOverviewToolStripMenuItem
            // 
            this.sortOverviewToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.text_list_numbers;
            this.sortOverviewToolStripMenuItem.Name = "sortOverviewToolStripMenuItem";
            this.sortOverviewToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.sortOverviewToolStripMenuItem.Text = "Sort Overview";
            this.sortOverviewToolStripMenuItem.Click += new System.EventHandler(this.sortOverviewToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(172, 6);
            // 
            // expandAllFoldersToolStripMenuItem
            // 
            this.expandAllFoldersToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.magnifier_zoom_in;
            this.expandAllFoldersToolStripMenuItem.Name = "expandAllFoldersToolStripMenuItem";
            this.expandAllFoldersToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.expandAllFoldersToolStripMenuItem.Text = "Expand all Folders";
            this.expandAllFoldersToolStripMenuItem.Click += new System.EventHandler(this.expandAllFoldersToolStripMenuItem_Click);
            // 
            // collapseAllFoldersToolStripMenuItem
            // 
            this.collapseAllFoldersToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.magifier_zoom_out;
            this.collapseAllFoldersToolStripMenuItem.Name = "collapseAllFoldersToolStripMenuItem";
            this.collapseAllFoldersToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.collapseAllFoldersToolStripMenuItem.Text = "Collapse all Folders";
            this.collapseAllFoldersToolStripMenuItem.Click += new System.EventHandler(this.collapseAllFoldersToolStripMenuItem_Click);
            // 
            // conMenuSessionMissing
            // 
            this.conMenuSessionMissing.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeMissingSessionToolStripMenuItem});
            this.conMenuSessionMissing.Name = "conMenuSessionMissing";
            this.conMenuSessionMissing.Size = new System.Drawing.Size(191, 26);
            // 
            // removeMissingSessionToolStripMenuItem
            // 
            this.removeMissingSessionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeMissingSessionFromListToolStripMenuItem});
            this.removeMissingSessionToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.error;
            this.removeMissingSessionToolStripMenuItem.Name = "removeMissingSessionToolStripMenuItem";
            this.removeMissingSessionToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.removeMissingSessionToolStripMenuItem.Text = "Session file is missing!";
            this.removeMissingSessionToolStripMenuItem.Click += new System.EventHandler(this.removeMissingSessionToolStripMenuItem_Click);
            // 
            // removeMissingSessionFromListToolStripMenuItem
            // 
            this.removeMissingSessionFromListToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.delete;
            this.removeMissingSessionFromListToolStripMenuItem.Name = "removeMissingSessionFromListToolStripMenuItem";
            this.removeMissingSessionFromListToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.removeMissingSessionFromListToolStripMenuItem.Text = "Remove missing session from list";
            this.removeMissingSessionFromListToolStripMenuItem.Click += new System.EventHandler(this.removeMissingSessionFromListToolStripMenuItem_Click);
            // 
            // conMenuRecent
            // 
            this.conMenuRecent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startSessionToolStripMenuItem1,
            this.startColoredSessionToolStripMenuItem,
            this.toolStripMenuItem7,
            this.transferSessionToPersonalSessionsToolStripMenuItem,
            this.reloadSessionLisToolStripMenuItem,
            this.toolStripMenuItem8,
            this.removeSessionFromListToolStripMenuItem});
            this.conMenuRecent.Name = "conMenuRecent";
            this.conMenuRecent.Size = new System.Drawing.Size(267, 126);
            // 
            // startSessionToolStripMenuItem1
            // 
            this.startSessionToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.startSessionToolStripMenuItem1.Image = global::PuttyServerGUI2.Properties.Resources.server_go;
            this.startSessionToolStripMenuItem1.Name = "startSessionToolStripMenuItem1";
            this.startSessionToolStripMenuItem1.Size = new System.Drawing.Size(266, 22);
            this.startSessionToolStripMenuItem1.Text = "Start session";
            this.startSessionToolStripMenuItem1.Click += new System.EventHandler(this.startSessionToolStripMenuItem1_Click);
            // 
            // startColoredSessionToolStripMenuItem
            // 
            this.startColoredSessionToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.color_wheel;
            this.startColoredSessionToolStripMenuItem.Name = "startColoredSessionToolStripMenuItem";
            this.startColoredSessionToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.startColoredSessionToolStripMenuItem.Text = "Start colored session";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(263, 6);
            // 
            // transferSessionToPersonalSessionsToolStripMenuItem
            // 
            this.transferSessionToPersonalSessionsToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.arrow_merge;
            this.transferSessionToPersonalSessionsToolStripMenuItem.Name = "transferSessionToPersonalSessionsToolStripMenuItem";
            this.transferSessionToPersonalSessionsToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.transferSessionToPersonalSessionsToolStripMenuItem.Text = "Transfer session to personal sessions";
            this.transferSessionToPersonalSessionsToolStripMenuItem.Click += new System.EventHandler(this.transferSessionToPersonalSessionsToolStripMenuItem_Click);
            // 
            // reloadSessionLisToolStripMenuItem
            // 
            this.reloadSessionLisToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.arrow_refresh;
            this.reloadSessionLisToolStripMenuItem.Name = "reloadSessionLisToolStripMenuItem";
            this.reloadSessionLisToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.reloadSessionLisToolStripMenuItem.Text = "Reload session list";
            this.reloadSessionLisToolStripMenuItem.Click += new System.EventHandler(this.reloadSessionLisToolStripMenuItem_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(263, 6);
            // 
            // removeSessionFromListToolStripMenuItem
            // 
            this.removeSessionFromListToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.delete;
            this.removeSessionFromListToolStripMenuItem.Name = "removeSessionFromListToolStripMenuItem";
            this.removeSessionFromListToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.removeSessionFromListToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.removeSessionFromListToolStripMenuItem.Text = "Remove session from list";
            this.removeSessionFromListToolStripMenuItem.Click += new System.EventHandler(this.removeSessionFromListToolStripMenuItem_Click);
            // 
            // conMenuRemoveMissingRecent
            // 
            this.conMenuRemoveMissingRecent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeMissingSessionToolStripMenuItem1,
            this.toolStripMenuItem9,
            this.removeAllMissingSessionsToolStripMenuItem,
            this.toolStripMenuItem10,
            this.reloadSessionListToolStripMenuItem});
            this.conMenuRemoveMissingRecent.Name = "conMenuRemoveMissingRecent";
            this.conMenuRemoveMissingRecent.Size = new System.Drawing.Size(223, 82);
            // 
            // removeMissingSessionToolStripMenuItem1
            // 
            this.removeMissingSessionToolStripMenuItem1.Image = global::PuttyServerGUI2.Properties.Resources.error;
            this.removeMissingSessionToolStripMenuItem1.Name = "removeMissingSessionToolStripMenuItem1";
            this.removeMissingSessionToolStripMenuItem1.Size = new System.Drawing.Size(222, 22);
            this.removeMissingSessionToolStripMenuItem1.Text = "Remove missing session";
            this.removeMissingSessionToolStripMenuItem1.Click += new System.EventHandler(this.removeMissingSessionToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(219, 6);
            // 
            // removeAllMissingSessionsToolStripMenuItem
            // 
            this.removeAllMissingSessionsToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.delete;
            this.removeAllMissingSessionsToolStripMenuItem.Name = "removeAllMissingSessionsToolStripMenuItem";
            this.removeAllMissingSessionsToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.removeAllMissingSessionsToolStripMenuItem.Text = "Remove all missing sessions";
            this.removeAllMissingSessionsToolStripMenuItem.Click += new System.EventHandler(this.removeAllMissingSessionsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(219, 6);
            // 
            // reloadSessionListToolStripMenuItem
            // 
            this.reloadSessionListToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.arrow_refresh;
            this.reloadSessionListToolStripMenuItem.Name = "reloadSessionListToolStripMenuItem";
            this.reloadSessionListToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.reloadSessionListToolStripMenuItem.Text = "Reload session list";
            this.reloadSessionListToolStripMenuItem.Click += new System.EventHandler(this.reloadSessionListToolStripMenuItem_Click);
            // 
            // conMenuTeamSession
            // 
            this.conMenuTeamSession.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startSessionToolStripMenuItem2,
            this.startSessionColoredToolStripMenuItem,
            this.toolStripMenuItem2,
            this.transferSessionToPersonalListToolStripMenuItem});
            this.conMenuTeamSession.Name = "conMenuTeamSession";
            this.conMenuTeamSession.Size = new System.Drawing.Size(243, 76);
            // 
            // startSessionToolStripMenuItem2
            // 
            this.startSessionToolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.startSessionToolStripMenuItem2.Image = global::PuttyServerGUI2.Properties.Resources.server_go;
            this.startSessionToolStripMenuItem2.Name = "startSessionToolStripMenuItem2";
            this.startSessionToolStripMenuItem2.Size = new System.Drawing.Size(242, 22);
            this.startSessionToolStripMenuItem2.Text = "Start Session";
            this.startSessionToolStripMenuItem2.Click += new System.EventHandler(this.startSessionToolStripMenuItem2_Click);
            // 
            // startSessionColoredToolStripMenuItem
            // 
            this.startSessionColoredToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.color_wheel;
            this.startSessionColoredToolStripMenuItem.Name = "startSessionColoredToolStripMenuItem";
            this.startSessionColoredToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.startSessionColoredToolStripMenuItem.Text = "Start Session Colored";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(239, 6);
            // 
            // transferSessionToPersonalListToolStripMenuItem
            // 
            this.transferSessionToPersonalListToolStripMenuItem.Image = global::PuttyServerGUI2.Properties.Resources.arrow_merge;
            this.transferSessionToPersonalListToolStripMenuItem.Name = "transferSessionToPersonalListToolStripMenuItem";
            this.transferSessionToPersonalListToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.transferSessionToPersonalListToolStripMenuItem.Text = "Transfer Session to Personal List";
            this.transferSessionToPersonalListToolStripMenuItem.Click += new System.EventHandler(this.transferSessionToPersonalListToolStripMenuItem_Click);
            // 
            // twiSessions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 474);
            this.Controls.Add(this.tabSessionAreas);
            this.Name = "twiSessions";
            this.Text = "Sessions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.twiSessions_FormClosing);
            this.Load += new System.EventHandler(this.twiSessions_Load);
            this.tabSessionAreas.ResumeLayout(false);
            this.tabPersonal.ResumeLayout(false);
            this.tabTeam.ResumeLayout(false);
            this.tabRecentSessions.ResumeLayout(false);
            this.conMenuSession.ResumeLayout(false);
            this.conMenuFolder.ResumeLayout(false);
            this.conMenuTreeView.ResumeLayout(false);
            this.conMenuSessionMissing.ResumeLayout(false);
            this.conMenuRecent.ResumeLayout(false);
            this.conMenuRemoveMissingRecent.ResumeLayout(false);
            this.conMenuTeamSession.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabSessionAreas;
        private System.Windows.Forms.TabPage tabPersonal;
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
        private System.Windows.Forms.ContextMenuStrip conMenuFolder;
        private System.Windows.Forms.ToolStripMenuItem addSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem addSubfolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip conMenuTreeView;
        private System.Windows.Forms.ToolStripMenuItem reloadOverviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortOverviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem expandAllFoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseAllFoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSubfolderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem addSessionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.TabPage tabTeam;
        private System.Windows.Forms.TreeView trvRecentSessions;
        private System.Windows.Forms.ContextMenuStrip conMenuSessionMissing;
        private System.Windows.Forms.ToolStripMenuItem removeMissingSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeMissingSessionFromListToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip conMenuRecent;
        private System.Windows.Forms.ToolStripMenuItem startSessionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem startColoredSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem transferSessionToPersonalSessionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem removeSessionFromListToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip conMenuRemoveMissingRecent;
        private System.Windows.Forms.ToolStripMenuItem removeMissingSessionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem removeAllMissingSessionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadSessionLisToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem reloadSessionListToolStripMenuItem;
        private System.Windows.Forms.TreeView trvTeam;
        private System.Windows.Forms.ContextMenuStrip conMenuTeamSession;
        private System.Windows.Forms.ToolStripMenuItem startSessionToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem startSessionColoredToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem transferSessionToPersonalListToolStripMenuItem;

    }
}