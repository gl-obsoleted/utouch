namespace udesign
{
    partial class PreviewForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_btRefresh = new DevComponents.DotNetBar.ButtonX();
            this.m_btResAndroid = new DevComponents.DotNetBar.ButtonX();
            this.m_menuResDesktop = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_btResIOS = new DevComponents.DotNetBar.ButtonX();
            this.m_btResDesktop = new DevComponents.DotNetBar.ButtonX();
            this.m_resolutionLabel = new DevComponents.DotNetBar.LabelX();
            this.m_menuResIOS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuResAndroid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.m_menuResDesktop.SuspendLayout();
            this.m_menuResIOS.SuspendLayout();
            this.m_menuResAndroid.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(784, 562);
            this.splitContainer1.SplitterDistance = 35;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.m_btRefresh, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_btResAndroid, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_btResIOS, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_btResDesktop, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_resolutionLabel, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 35);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // m_btRefresh
            // 
            this.m_btRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.m_btRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.m_btRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_btRefresh.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.m_btRefresh.Location = new System.Drawing.Point(6, 6);
            this.m_btRefresh.Margin = new System.Windows.Forms.Padding(6);
            this.m_btRefresh.Name = "m_btRefresh";
            this.m_btRefresh.Size = new System.Drawing.Size(108, 23);
            this.m_btRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.m_btRefresh.TabIndex = 4;
            this.m_btRefresh.Text = "刷新";
            this.m_btRefresh.Click += new System.EventHandler(this.m_btRefresh_Click);
            // 
            // m_btResAndroid
            // 
            this.m_btResAndroid.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.m_btResAndroid.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.m_btResAndroid.ContextMenuStrip = this.m_menuResDesktop;
            this.m_btResAndroid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_btResAndroid.Image = global::udesign.Properties.Resources.os_android;
            this.m_btResAndroid.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.m_btResAndroid.Location = new System.Drawing.Point(326, 6);
            this.m_btResAndroid.Margin = new System.Windows.Forms.Padding(6);
            this.m_btResAndroid.Name = "m_btResAndroid";
            this.m_btResAndroid.Size = new System.Drawing.Size(88, 23);
            this.m_btResAndroid.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.m_btResAndroid.TabIndex = 3;
            this.m_btResAndroid.Text = "Android";
            this.m_btResAndroid.Click += new System.EventHandler(this.m_btResAndroid_Click);
            // 
            // m_menuResDesktop
            // 
            this.m_menuResDesktop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.m_menuResDesktop.Name = "contextMenuStrip1";
            this.m_menuResDesktop.Size = new System.Drawing.Size(98, 26);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.testToolStripMenuItem.Text = "test";
            // 
            // m_btResIOS
            // 
            this.m_btResIOS.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.m_btResIOS.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.m_btResIOS.ContextMenuStrip = this.m_menuResDesktop;
            this.m_btResIOS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_btResIOS.Image = global::udesign.Properties.Resources.os_ios;
            this.m_btResIOS.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.m_btResIOS.Location = new System.Drawing.Point(226, 6);
            this.m_btResIOS.Margin = new System.Windows.Forms.Padding(6);
            this.m_btResIOS.Name = "m_btResIOS";
            this.m_btResIOS.Size = new System.Drawing.Size(88, 23);
            this.m_btResIOS.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.m_btResIOS.TabIndex = 2;
            this.m_btResIOS.Text = "iOS";
            this.m_btResIOS.Click += new System.EventHandler(this.m_btResIOS_Click);
            // 
            // m_btResDesktop
            // 
            this.m_btResDesktop.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.m_btResDesktop.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.m_btResDesktop.ContextMenuStrip = this.m_menuResDesktop;
            this.m_btResDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_btResDesktop.Image = ((System.Drawing.Image)(resources.GetObject("m_btResDesktop.Image")));
            this.m_btResDesktop.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.m_btResDesktop.Location = new System.Drawing.Point(126, 6);
            this.m_btResDesktop.Margin = new System.Windows.Forms.Padding(6);
            this.m_btResDesktop.Name = "m_btResDesktop";
            this.m_btResDesktop.Size = new System.Drawing.Size(88, 23);
            this.m_btResDesktop.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.m_btResDesktop.TabIndex = 1;
            this.m_btResDesktop.Text = "桌面";
            this.m_btResDesktop.Click += new System.EventHandler(this.m_btResDesktop_Click);
            // 
            // m_resolutionLabel
            // 
            // 
            // 
            // 
            this.m_resolutionLabel.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.m_resolutionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_resolutionLabel.Location = new System.Drawing.Point(430, 3);
            this.m_resolutionLabel.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.m_resolutionLabel.Name = "m_resolutionLabel";
            this.m_resolutionLabel.Size = new System.Drawing.Size(130, 29);
            this.m_resolutionLabel.TabIndex = 5;
            this.m_resolutionLabel.Text = "<default_res>";
            // 
            // m_menuResIOS
            // 
            this.m_menuResIOS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.m_menuResIOS.Name = "contextMenuStrip1";
            this.m_menuResIOS.Size = new System.Drawing.Size(98, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(97, 22);
            this.toolStripMenuItem1.Text = "test";
            // 
            // m_menuResAndroid
            // 
            this.m_menuResAndroid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.m_menuResAndroid.Name = "contextMenuStrip1";
            this.m_menuResAndroid.Size = new System.Drawing.Size(98, 26);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(97, 22);
            this.toolStripMenuItem2.Text = "test";
            // 
            // PreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.splitContainer1);
            this.Name = "PreviewForm";
            this.Text = "预览窗口";
            this.Load += new System.EventHandler(this.GwenUnitTestForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.m_menuResDesktop.ResumeLayout(false);
            this.m_menuResIOS.ResumeLayout(false);
            this.m_menuResAndroid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.ButtonX m_btResAndroid;
        private DevComponents.DotNetBar.ButtonX m_btResIOS;
        private DevComponents.DotNetBar.ButtonX m_btResDesktop;
        private System.Windows.Forms.ContextMenuStrip m_menuResDesktop;
        private DevComponents.DotNetBar.ButtonX m_btRefresh;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private DevComponents.DotNetBar.LabelX m_resolutionLabel;
        private System.Windows.Forms.ContextMenuStrip m_menuResIOS;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip m_menuResAndroid;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}