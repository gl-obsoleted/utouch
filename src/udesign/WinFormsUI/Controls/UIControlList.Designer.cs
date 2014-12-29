namespace udesign.WinFormsUI.Controls
{
    partial class UIControlList
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Elements", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Controls", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Node");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("ImageNode");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("TextNode");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Button");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("CheckBox");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Label");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Image");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Grid");
            this.m_controlList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // m_controlList
            // 
            this.m_controlList.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "Elements";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "Controls";
            listViewGroup2.Name = "listViewGroup2";
            this.m_controlList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            listViewItem1.Group = listViewGroup1;
            listViewItem2.Group = listViewGroup1;
            listViewItem3.Group = listViewGroup1;
            listViewItem4.Group = listViewGroup2;
            listViewItem5.Group = listViewGroup2;
            listViewItem6.Group = listViewGroup2;
            listViewItem7.Group = listViewGroup2;
            listViewItem8.Group = listViewGroup2;
            this.m_controlList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8});
            this.m_controlList.Location = new System.Drawing.Point(0, 0);
            this.m_controlList.MultiSelect = false;
            this.m_controlList.Name = "m_controlList";
            this.m_controlList.Size = new System.Drawing.Size(252, 441);
            this.m_controlList.TabIndex = 1;
            this.m_controlList.UseCompatibleStateImageBehavior = false;
            this.m_controlList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.m_controlList_ItemDrag);
            // 
            // UIControlList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_controlList);
            this.Name = "UIControlList";
            this.Size = new System.Drawing.Size(252, 441);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView m_controlList;
    }
}
