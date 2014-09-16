namespace udesign
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
            this.m_menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.m_menuCut = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.testFramesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemGwenUnitTest = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuResForm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.m_menuOpenTestLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_tabControl = new System.Windows.Forms.TabControl();
            this.pageLayout = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pageControl = new System.Windows.Forms.TabPage();
            this.m_uiLayoutTree = new udesign.Controls.UILayoutTree();
            this.m_uiPropertyGrid = new udesign.Controls.UIObjectPropertyGrid();
            this.uiControlList2 = new udesign.WinFormsUI.Controls.UIControlList();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.m_tabControl.SuspendLayout();
            this.pageLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.pageControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
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
            this.m_menuSaveAs,
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
            this.m_menuNew.Size = new System.Drawing.Size(205, 22);
            this.m_menuNew.Text = "&New";
            this.m_menuNew.Click += new System.EventHandler(this.m_menuNew_Click);
            // 
            // m_menuOpen
            // 
            this.m_menuOpen.Name = "m_menuOpen";
            this.m_menuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.m_menuOpen.Size = new System.Drawing.Size(205, 22);
            this.m_menuOpen.Text = "&Open...";
            this.m_menuOpen.Click += new System.EventHandler(this.m_menuOpen_Click);
            // 
            // m_menuSave
            // 
            this.m_menuSave.Name = "m_menuSave";
            this.m_menuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.m_menuSave.Size = new System.Drawing.Size(205, 22);
            this.m_menuSave.Text = "&Save";
            this.m_menuSave.Click += new System.EventHandler(this.m_menuSave_Click);
            // 
            // m_menuSaveAs
            // 
            this.m_menuSaveAs.Name = "m_menuSaveAs";
            this.m_menuSaveAs.Size = new System.Drawing.Size(205, 22);
            this.m_menuSaveAs.Text = "Save &As...";
            this.m_menuSaveAs.Click += new System.EventHandler(this.m_menuSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(202, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.saveToolStripMenuItem.Text = "&Publish (暂未实现)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(202, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_menuUndo,
            this.m_menuRedo,
            this.toolStripSeparator4,
            this.m_menuCut,
            this.m_menuCopy,
            this.m_menuPaste,
            this.toolStripSeparator3,
            this.m_menuDelete});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(42, 21);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // m_menuUndo
            // 
            this.m_menuUndo.Name = "m_menuUndo";
            this.m_menuUndo.Size = new System.Drawing.Size(152, 22);
            this.m_menuUndo.Text = "&Undo";
            this.m_menuUndo.Click += new System.EventHandler(this.m_menuUndo_Click);
            // 
            // m_menuRedo
            // 
            this.m_menuRedo.Name = "m_menuRedo";
            this.m_menuRedo.Size = new System.Drawing.Size(152, 22);
            this.m_menuRedo.Text = "&Redo";
            this.m_menuRedo.Click += new System.EventHandler(this.m_menuRedo_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // m_menuCut
            // 
            this.m_menuCut.Name = "m_menuCut";
            this.m_menuCut.Size = new System.Drawing.Size(152, 22);
            this.m_menuCut.Text = "Cu&t";
            this.m_menuCut.Click += new System.EventHandler(this.m_menuCut_Click);
            // 
            // m_menuCopy
            // 
            this.m_menuCopy.Name = "m_menuCopy";
            this.m_menuCopy.Size = new System.Drawing.Size(152, 22);
            this.m_menuCopy.Text = "&Copy";
            this.m_menuCopy.Click += new System.EventHandler(this.m_menuCopy_Click);
            // 
            // m_menuPaste
            // 
            this.m_menuPaste.Name = "m_menuPaste";
            this.m_menuPaste.Size = new System.Drawing.Size(152, 22);
            this.m_menuPaste.Text = "&Paste";
            this.m_menuPaste.Click += new System.EventHandler(this.m_menuPaste_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // m_menuDelete
            // 
            this.m_menuDelete.Name = "m_menuDelete";
            this.m_menuDelete.Size = new System.Drawing.Size(152, 22);
            this.m_menuDelete.Text = "&Delete";
            this.m_menuDelete.Click += new System.EventHandler(this.m_menuDelete_Click);
            // 
            // testFramesToolStripMenuItem
            // 
            this.testFramesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemGwenUnitTest,
            this.m_menuResForm,
            this.toolStripSeparator5,
            this.m_menuOpenTestLayout});
            this.testFramesToolStripMenuItem.Name = "testFramesToolStripMenuItem";
            this.testFramesToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.testFramesToolStripMenuItem.Text = "&Test";
            // 
            // menuItemGwenUnitTest
            // 
            this.menuItemGwenUnitTest.Name = "menuItemGwenUnitTest";
            this.menuItemGwenUnitTest.Size = new System.Drawing.Size(264, 22);
            this.menuItemGwenUnitTest.Text = "Gwen UnitTest";
            this.menuItemGwenUnitTest.Click += new System.EventHandler(this.menuItemGwenUnitTest_Click);
            // 
            // m_menuResForm
            // 
            this.m_menuResForm.Name = "m_menuResForm";
            this.m_menuResForm.Size = new System.Drawing.Size(264, 22);
            this.m_menuResForm.Text = "ResForm";
            this.m_menuResForm.Click += new System.EventHandler(this.m_menuResForm_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(261, 6);
            // 
            // m_menuOpenTestLayout
            // 
            this.m_menuOpenTestLayout.Name = "m_menuOpenTestLayout";
            this.m_menuOpenTestLayout.Size = new System.Drawing.Size(264, 22);
            this.m_menuOpenTestLayout.Text = "Open Test Layout (test.ui_layout)";
            this.m_menuOpenTestLayout.Click += new System.EventHandler(this.m_menuOpenTestLayout_Click);
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
            this.m_tabControl.Controls.Add(this.pageLayout);
            this.m_tabControl.Controls.Add(this.pageControl);
            this.m_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabControl.Location = new System.Drawing.Point(0, 0);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.SelectedIndex = 0;
            this.m_tabControl.Size = new System.Drawing.Size(151, 515);
            this.m_tabControl.TabIndex = 0;
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
            // pageControl
            // 
            this.pageControl.Controls.Add(this.uiControlList2);
            this.pageControl.Location = new System.Drawing.Point(4, 22);
            this.pageControl.Name = "pageControl";
            this.pageControl.Padding = new System.Windows.Forms.Padding(3);
            this.pageControl.Size = new System.Drawing.Size(143, 489);
            this.pageControl.TabIndex = 2;
            this.pageControl.Text = "Control";
            this.pageControl.UseVisualStyleBackColor = true;
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
            // uiControlList2
            // 
            this.uiControlList2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiControlList2.Location = new System.Drawing.Point(3, 3);
            this.uiControlList2.Name = "uiControlList2";
            this.uiControlList2.Size = new System.Drawing.Size(137, 483);
            this.uiControlList2.TabIndex = 0;
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
            this.pageLayout.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.pageControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_menuOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem m_menuNew;
        private System.Windows.Forms.ToolStripMenuItem m_menuSave;
        private System.Windows.Forms.TabControl m_tabControl;
        private System.Windows.Forms.TabPage pageLayout;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private udesign.Controls.UILayoutTree m_uiLayoutTree;
        private udesign.Controls.UIObjectPropertyGrid m_uiPropertyGrid;
        private System.Windows.Forms.ToolStripMenuItem testFramesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemGwenUnitTest;
        private System.Windows.Forms.ToolStripMenuItem m_menuResForm;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_menuUndo;
        private System.Windows.Forms.ToolStripMenuItem m_menuRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem m_menuDelete;
        private System.Windows.Forms.TabPage pageControl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem m_menuOpenTestLayout;
        private System.Windows.Forms.ToolStripMenuItem m_menuSaveAs;
        private WinFormsUI.Controls.UIControlList uiControlList2;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem m_menuCut;
        private System.Windows.Forms.ToolStripMenuItem m_menuCopy;
        private System.Windows.Forms.ToolStripMenuItem m_menuPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

