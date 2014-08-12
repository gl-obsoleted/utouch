namespace ui_designer_shell
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkboxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testFramesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemGwenUnitTest = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuResForm = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_tabControl = new System.Windows.Forms.TabControl();
            this.pageProject = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.pageLayout = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.m_uiLayoutTree = new ui_designer_shell.Controls.UILayoutTree();
            this.m_uiPropertyGrid = new ui_designer_shell.Controls.UIObjectPropertyGrid();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.m_tabControl.SuspendLayout();
            this.pageProject.SuspendLayout();
            this.pageLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.panelsToolStripMenuItem,
            this.addToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.testFramesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuNew,
            this.m_menuOpen,
            this.m_menuSave,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // m_menuNew
            // 
            this.m_menuNew.Name = "m_menuNew";
            this.m_menuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.m_menuNew.Size = new System.Drawing.Size(155, 22);
            this.m_menuNew.Text = "&New";
            this.m_menuNew.Click += new System.EventHandler(this.m_menuNew_Click);
            // 
            // m_menuOpen
            // 
            this.m_menuOpen.Name = "m_menuOpen";
            this.m_menuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.m_menuOpen.Size = new System.Drawing.Size(155, 22);
            this.m_menuOpen.Text = "&Open";
            this.m_menuOpen.Click += new System.EventHandler(this.m_menuOpen_Click);
            // 
            // m_menuSave
            // 
            this.m_menuSave.Name = "m_menuSave";
            this.m_menuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.m_menuSave.Size = new System.Drawing.Size(155, 22);
            this.m_menuSave.Text = "&Save";
            this.m_menuSave.Click += new System.EventHandler(this.m_menuSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveToolStripMenuItem.Text = "&Publish";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(152, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            // 
            // panelsToolStripMenuItem
            // 
            this.panelsToolStripMenuItem.Name = "panelsToolStripMenuItem";
            this.panelsToolStripMenuItem.Size = new System.Drawing.Size(57, 21);
            this.panelsToolStripMenuItem.Text = "&Panels";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nodeToolStripMenuItem,
            this.imageToolStripMenuItem,
            this.textToolStripMenuItem,
            this.toolStripSeparator3,
            this.buttonToolStripMenuItem,
            this.checkboxToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(64, 21);
            this.addToolStripMenuItem.Text = "&Objects";
            // 
            // nodeToolStripMenuItem
            // 
            this.nodeToolStripMenuItem.Name = "nodeToolStripMenuItem";
            this.nodeToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.nodeToolStripMenuItem.Text = "Node";
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.imageToolStripMenuItem.Text = "Image";
            // 
            // textToolStripMenuItem
            // 
            this.textToolStripMenuItem.Name = "textToolStripMenuItem";
            this.textToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.textToolStripMenuItem.Text = "Text";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(130, 6);
            // 
            // buttonToolStripMenuItem
            // 
            this.buttonToolStripMenuItem.Name = "buttonToolStripMenuItem";
            this.buttonToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.buttonToolStripMenuItem.Text = "Button";
            // 
            // checkboxToolStripMenuItem
            // 
            this.checkboxToolStripMenuItem.Name = "checkboxToolStripMenuItem";
            this.checkboxToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.checkboxToolStripMenuItem.Text = "Checkbox";
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(62, 21);
            this.actionsToolStripMenuItem.Text = "&Actions";
            // 
            // testFramesToolStripMenuItem
            // 
            this.testFramesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemGwenUnitTest,
            this.m_menuResForm});
            this.testFramesToolStripMenuItem.Name = "testFramesToolStripMenuItem";
            this.testFramesToolStripMenuItem.Size = new System.Drawing.Size(86, 21);
            this.testFramesToolStripMenuItem.Text = "TestFrames";
            // 
            // menuItemGwenUnitTest
            // 
            this.menuItemGwenUnitTest.Name = "menuItemGwenUnitTest";
            this.menuItemGwenUnitTest.Size = new System.Drawing.Size(159, 22);
            this.menuItemGwenUnitTest.Text = "Gwen UnitTest";
            this.menuItemGwenUnitTest.Click += new System.EventHandler(this.menuItemGwenUnitTest_Click);
            // 
            // m_menuResForm
            // 
            this.m_menuResForm.Name = "m_menuResForm";
            this.m_menuResForm.Size = new System.Drawing.Size(159, 22);
            this.m_menuResForm.Text = "ResForm";
            this.m_menuResForm.Click += new System.EventHandler(this.m_menuResForm_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_tabControl);
            this.splitContainer1.Size = new System.Drawing.Size(784, 515);
            this.splitContainer1.SplitterDistance = 151;
            this.splitContainer1.TabIndex = 2;
            // 
            // m_tabControl
            // 
            this.m_tabControl.Controls.Add(this.pageProject);
            this.m_tabControl.Controls.Add(this.pageLayout);
            this.m_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabControl.Location = new System.Drawing.Point(0, 0);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.SelectedIndex = 0;
            this.m_tabControl.Size = new System.Drawing.Size(151, 515);
            this.m_tabControl.TabIndex = 0;
            // 
            // pageProject
            // 
            this.pageProject.Controls.Add(this.treeView1);
            this.pageProject.Location = new System.Drawing.Point(4, 22);
            this.pageProject.Name = "pageProject";
            this.pageProject.Padding = new System.Windows.Forms.Padding(3);
            this.pageProject.Size = new System.Drawing.Size(143, 489);
            this.pageProject.TabIndex = 0;
            this.pageProject.Text = "Project";
            this.pageProject.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(137, 483);
            this.treeView1.TabIndex = 0;
            // 
            // pageLayout
            // 
            this.pageLayout.Controls.Add(this.splitContainer2);
            this.pageLayout.Location = new System.Drawing.Point(4, 22);
            this.pageLayout.Name = "pageLayout";
            this.pageLayout.Padding = new System.Windows.Forms.Padding(3);
            this.pageLayout.Size = new System.Drawing.Size(143, 489);
            this.pageLayout.TabIndex = 1;
            this.pageLayout.Text = "Layout";
            this.pageLayout.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.m_uiLayoutTree);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.m_uiPropertyGrid);
            this.splitContainer2.Size = new System.Drawing.Size(137, 483);
            this.splitContainer2.SplitterDistance = 238;
            this.splitContainer2.TabIndex = 1;
            // 
            // m_uiLayoutTree
            // 
            this.m_uiLayoutTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_uiLayoutTree.Location = new System.Drawing.Point(0, 0);
            this.m_uiLayoutTree.Name = "m_uiLayoutTree";
            this.m_uiLayoutTree.Size = new System.Drawing.Size(137, 238);
            this.m_uiLayoutTree.TabIndex = 0;
            // 
            // m_uiPropertyGrid
            // 
            this.m_uiPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_uiPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.m_uiPropertyGrid.Name = "m_uiPropertyGrid";
            this.m_uiPropertyGrid.Size = new System.Drawing.Size(137, 241);
            this.m_uiPropertyGrid.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "UI Designer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.m_tabControl.ResumeLayout(false);
            this.pageProject.ResumeLayout(false);
            this.pageLayout.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_menuOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem buttonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkboxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem m_menuNew;
        private System.Windows.Forms.ToolStripMenuItem m_menuSave;
        private System.Windows.Forms.TabControl m_tabControl;
        private System.Windows.Forms.TabPage pageProject;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TabPage pageLayout;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ui_designer_shell.Controls.UILayoutTree m_uiLayoutTree;
        private ui_designer_shell.Controls.UIObjectPropertyGrid m_uiPropertyGrid;
        private System.Windows.Forms.ToolStripMenuItem testFramesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemGwenUnitTest;
        private System.Windows.Forms.ToolStripMenuItem m_menuResForm;
    }
}

