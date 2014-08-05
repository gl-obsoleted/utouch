namespace ui_designer_shell.Controls
{
    partial class UILayoutTree
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("<not_specified>");
            this.m_layoutTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // m_layoutTreeView
            // 
            this.m_layoutTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_layoutTreeView.Location = new System.Drawing.Point(0, 0);
            this.m_layoutTreeView.Name = "m_layoutTreeView";
            treeNode1.Name = "Root";
            treeNode1.Tag = "Root";
            treeNode1.Text = "<not_specified>";
            this.m_layoutTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.m_layoutTreeView.Size = new System.Drawing.Size(319, 347);
            this.m_layoutTreeView.TabIndex = 0;
            this.m_layoutTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_layoutTreeView_AfterSelect);
            // 
            // UILayoutTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_layoutTreeView);
            this.Name = "UILayoutTree";
            this.Size = new System.Drawing.Size(319, 347);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView m_layoutTreeView;
    }
}
