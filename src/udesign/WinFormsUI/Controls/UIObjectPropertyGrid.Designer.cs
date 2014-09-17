namespace udesign.Controls
{
    partial class UIObjectPropertyGrid
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
            this.m_propertyGrid = new DevComponents.DotNetBar.AdvPropertyGrid();
            this.Position = new DevComponents.DotNetBar.PropertySettings();
            ((System.ComponentModel.ISupportInitialize)(this.m_propertyGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // m_propertyGrid
            // 
            this.m_propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_propertyGrid.GridLinesColor = System.Drawing.Color.WhiteSmoke;
            this.m_propertyGrid.HelpType = DevComponents.DotNetBar.ePropertyGridHelpType.Panel;
            this.m_propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.m_propertyGrid.Name = "m_propertyGrid";
            this.m_propertyGrid.Size = new System.Drawing.Size(332, 408);
            this.m_propertyGrid.TabIndex = 0;
            this.m_propertyGrid.Text = "advPropertyGrid1";
            this.m_propertyGrid.PropertyValueChanged += new System.ComponentModel.PropertyChangedEventHandler(this.m_propertyGrid_PropertyValueChanged);
            this.m_propertyGrid.PropertyValueChanging += new DevComponents.DotNetBar.PropertyValueChangingEventHandler(this.m_propertyGrid_PropertyValueChanging);
            this.m_propertyGrid.ValidatePropertyValue += new DevComponents.DotNetBar.ValidatePropertyValueEventHandler(this.m_propertyGrid_ValidatePropertyValue);
            // 
            // Position
            // 
            this.Position.DisplayName = "Position";
            this.Position.PropertyName = "Position";
            // 
            // UIObjectPropertyGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_propertyGrid);
            this.Name = "UIObjectPropertyGrid";
            this.Size = new System.Drawing.Size(332, 408);
            ((System.ComponentModel.ISupportInitialize)(this.m_propertyGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.AdvPropertyGrid m_propertyGrid;
        private DevComponents.DotNetBar.PropertySettings Position;



    }
}
