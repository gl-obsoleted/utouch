namespace udesign
{
    partial class UINineGridSettings
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.m_enable9gridHori = new System.Windows.Forms.CheckBox();
            this.m_9gridHoriPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.m_9gridHoriLeft = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_9gridHoriRight = new System.Windows.Forms.TextBox();
            this.m_btSetToDefaultHori = new System.Windows.Forms.Button();
            this.m_enable9gridVert = new System.Windows.Forms.CheckBox();
            this.m_9gridVertPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.m_9gridVertTop = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_9gridVertBottom = new System.Windows.Forms.TextBox();
            this.m_btSetToDefaultVert = new System.Windows.Forms.Button();
            this.tableLayoutPanel2.SuspendLayout();
            this.m_9gridHoriPanel.SuspendLayout();
            this.m_9gridVertPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.07166F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 321F));
            this.tableLayoutPanel2.Controls.Add(this.m_enable9gridHori, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.m_9gridHoriPanel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.m_enable9gridVert, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.m_9gridVertPanel, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.94805F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.05195F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(450, 87);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // m_enable9gridHori
            // 
            this.m_enable9gridHori.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_enable9gridHori.AutoSize = true;
            this.m_enable9gridHori.Location = new System.Drawing.Point(8, 16);
            this.m_enable9gridHori.Name = "m_enable9gridHori";
            this.m_enable9gridHori.Size = new System.Drawing.Size(108, 16);
            this.m_enable9gridHori.TabIndex = 1;
            this.m_enable9gridHori.Text = "九宫格水平拉伸";
            this.m_enable9gridHori.UseVisualStyleBackColor = true;
            this.m_enable9gridHori.CheckedChanged += new System.EventHandler(this.m_enable9gridHori_CheckedChanged);
            // 
            // m_9gridHoriPanel
            // 
            this.m_9gridHoriPanel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_9gridHoriPanel.Controls.Add(this.label2);
            this.m_9gridHoriPanel.Controls.Add(this.m_9gridHoriLeft);
            this.m_9gridHoriPanel.Controls.Add(this.label3);
            this.m_9gridHoriPanel.Controls.Add(this.m_9gridHoriRight);
            this.m_9gridHoriPanel.Controls.Add(this.m_btSetToDefaultHori);
            this.m_9gridHoriPanel.Enabled = false;
            this.m_9gridHoriPanel.Location = new System.Drawing.Point(127, 9);
            this.m_9gridHoriPanel.Name = "m_9gridHoriPanel";
            this.m_9gridHoriPanel.Size = new System.Drawing.Size(315, 31);
            this.m_9gridHoriPanel.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Border: Left";
            // 
            // m_9gridHoriLeft
            // 
            this.m_9gridHoriLeft.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_9gridHoriLeft.Location = new System.Drawing.Point(86, 4);
            this.m_9gridHoriLeft.Name = "m_9gridHoriLeft";
            this.m_9gridHoriLeft.Size = new System.Drawing.Size(50, 21);
            this.m_9gridHoriLeft.TabIndex = 3;
            this.m_9gridHoriLeft.Text = "0";
            this.m_9gridHoriLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_9gridHoriLeft.TextChanged += new System.EventHandler(this.m_TextChanged);
            this.m_9gridHoriLeft.Validating += new System.ComponentModel.CancelEventHandler(this.m_EditValidating);
            this.m_9gridHoriLeft.Validated += new System.EventHandler(this.m_EditValidated);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Right ";
            // 
            // m_9gridHoriRight
            // 
            this.m_9gridHoriRight.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_9gridHoriRight.Location = new System.Drawing.Point(189, 4);
            this.m_9gridHoriRight.Name = "m_9gridHoriRight";
            this.m_9gridHoriRight.Size = new System.Drawing.Size(50, 21);
            this.m_9gridHoriRight.TabIndex = 5;
            this.m_9gridHoriRight.Text = "0";
            this.m_9gridHoriRight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_9gridHoriRight.TextChanged += new System.EventHandler(this.m_TextChanged);
            this.m_9gridHoriRight.Validating += new System.ComponentModel.CancelEventHandler(this.m_EditValidating);
            this.m_9gridHoriRight.Validated += new System.EventHandler(this.m_EditValidated);
            // 
            // m_btSetToDefaultHori
            // 
            this.m_btSetToDefaultHori.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_btSetToDefaultHori.Location = new System.Drawing.Point(245, 3);
            this.m_btSetToDefaultHori.Name = "m_btSetToDefaultHori";
            this.m_btSetToDefaultHori.Size = new System.Drawing.Size(50, 23);
            this.m_btSetToDefaultHori.TabIndex = 6;
            this.m_btSetToDefaultHori.Text = "默认";
            this.m_btSetToDefaultHori.UseVisualStyleBackColor = true;
            this.m_btSetToDefaultHori.Click += new System.EventHandler(this.m_btSetToDefaultHori_Click);
            // 
            // m_enable9gridVert
            // 
            this.m_enable9gridVert.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_enable9gridVert.AutoSize = true;
            this.m_enable9gridVert.Location = new System.Drawing.Point(8, 55);
            this.m_enable9gridVert.Name = "m_enable9gridVert";
            this.m_enable9gridVert.Size = new System.Drawing.Size(108, 16);
            this.m_enable9gridVert.TabIndex = 1;
            this.m_enable9gridVert.Text = "九宫格垂直拉伸";
            this.m_enable9gridVert.UseVisualStyleBackColor = true;
            this.m_enable9gridVert.CheckedChanged += new System.EventHandler(this.m_enable9gridVert_CheckedChanged);
            // 
            // m_9gridVertPanel
            // 
            this.m_9gridVertPanel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_9gridVertPanel.Controls.Add(this.label4);
            this.m_9gridVertPanel.Controls.Add(this.m_9gridVertTop);
            this.m_9gridVertPanel.Controls.Add(this.label5);
            this.m_9gridVertPanel.Controls.Add(this.m_9gridVertBottom);
            this.m_9gridVertPanel.Controls.Add(this.m_btSetToDefaultVert);
            this.m_9gridVertPanel.Enabled = false;
            this.m_9gridVertPanel.Location = new System.Drawing.Point(127, 48);
            this.m_9gridVertPanel.Name = "m_9gridVertPanel";
            this.m_9gridVertPanel.Size = new System.Drawing.Size(315, 29);
            this.m_9gridVertPanel.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Border: Top ";
            // 
            // m_9gridVertTop
            // 
            this.m_9gridVertTop.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_9gridVertTop.Location = new System.Drawing.Point(86, 4);
            this.m_9gridVertTop.Name = "m_9gridVertTop";
            this.m_9gridVertTop.Size = new System.Drawing.Size(50, 21);
            this.m_9gridVertTop.TabIndex = 3;
            this.m_9gridVertTop.Text = "0";
            this.m_9gridVertTop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_9gridVertTop.TextChanged += new System.EventHandler(this.m_TextChanged);
            this.m_9gridVertTop.Validating += new System.ComponentModel.CancelEventHandler(this.m_EditValidating);
            this.m_9gridVertTop.Validated += new System.EventHandler(this.m_EditValidated);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(142, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "Bottom";
            // 
            // m_9gridVertBottom
            // 
            this.m_9gridVertBottom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_9gridVertBottom.Location = new System.Drawing.Point(189, 4);
            this.m_9gridVertBottom.Name = "m_9gridVertBottom";
            this.m_9gridVertBottom.Size = new System.Drawing.Size(50, 21);
            this.m_9gridVertBottom.TabIndex = 5;
            this.m_9gridVertBottom.Text = "0";
            this.m_9gridVertBottom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_9gridVertBottom.TextChanged += new System.EventHandler(this.m_TextChanged);
            this.m_9gridVertBottom.Validating += new System.ComponentModel.CancelEventHandler(this.m_EditValidating);
            this.m_9gridVertBottom.Validated += new System.EventHandler(this.m_EditValidated);
            // 
            // m_btSetToDefaultVert
            // 
            this.m_btSetToDefaultVert.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.m_btSetToDefaultVert.Location = new System.Drawing.Point(245, 3);
            this.m_btSetToDefaultVert.Name = "m_btSetToDefaultVert";
            this.m_btSetToDefaultVert.Size = new System.Drawing.Size(50, 23);
            this.m_btSetToDefaultVert.TabIndex = 7;
            this.m_btSetToDefaultVert.Text = "默认";
            this.m_btSetToDefaultVert.UseVisualStyleBackColor = true;
            this.m_btSetToDefaultVert.Click += new System.EventHandler(this.m_btSetToDefaultVert_Click);
            // 
            // UINineGridSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "UINineGridSettings";
            this.Size = new System.Drawing.Size(450, 87);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.m_9gridHoriPanel.ResumeLayout(false);
            this.m_9gridHoriPanel.PerformLayout();
            this.m_9gridVertPanel.ResumeLayout(false);
            this.m_9gridVertPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox m_enable9gridHori;
        private System.Windows.Forms.FlowLayoutPanel m_9gridHoriPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_9gridHoriLeft;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_9gridHoriRight;
        private System.Windows.Forms.Button m_btSetToDefaultHori;
        private System.Windows.Forms.CheckBox m_enable9gridVert;
        private System.Windows.Forms.FlowLayoutPanel m_9gridVertPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_9gridVertTop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox m_9gridVertBottom;
        private System.Windows.Forms.Button m_btSetToDefaultVert;

    }
}
