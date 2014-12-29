using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using ulib.Elements;

namespace ulib.Controls
{
    public class Grid : Node
    {
        public enum AlignType
        {
            Horizontally,
            Vertically,
        }

        public enum AlignHoriDirection
        {
            LeftToRight,
            RightToLeft,
        }

        public enum AlignVertDirection
        {
            TopDown,
            BottomUp,
        }

        [Category("Grid")]
        public AlignType Align { get; set; }

        [Category("Grid")]
        public AlignHoriDirection HoriDirection { get; set; }

        [Category("Grid")]
        public AlignVertDirection VertDirection { get; set; }

        public Grid()
            : base()
        {
            Size = new Size(200, 200);

            Align = AlignType.Horizontally;
            HoriDirection = AlignHoriDirection.LeftToRight;
            VertDirection = AlignVertDirection.TopDown;
            CellSize = new Size(60, 60);
            CellPadding = new Size(5, 5);
            RowCount = 3;
            ColumnCount = 3;
        }

        protected void OnLogicalSizeChanged()
        {
            m_logicalSize.Width = CellSize.Width * RowCount + CellPadding.Width * (RowCount - 1);
            m_logicalSize.Height = CellSize.Height * ColumnCount + CellPadding.Height * (ColumnCount - 1);
        }

        [Category("Grid")]
        public Size CellSize { get { return m_cellSize; } set { m_cellSize = value; OnLogicalSizeChanged(); } }
        private Size m_cellSize;

        [Category("Grid")]
        public Size CellPadding { get { return m_cellPadding; } set { m_cellPadding = value; OnLogicalSizeChanged(); } }
        private Size m_cellPadding;

        [Category("Grid")]
        public int RowCount { get { return m_rowCount; } set { m_rowCount = value; OnLogicalSizeChanged(); } }
        private int m_rowCount;

        [Category("Grid")]
        public int ColumnCount { get { return m_columnCount; } set { m_columnCount = value; OnLogicalSizeChanged(); } }
        private int m_columnCount;

        // 这个值是由 CellSize、CellPadding、RowCount 和 ColumnCount 计算而来，外界不可修改
        [JsonIgnore]
        [Browsable(false)]
        public override Size LogicalSize { get { return m_logicalSize; } }
        private Size m_logicalSize;
    }
}
