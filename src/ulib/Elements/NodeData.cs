using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using ulib.Base;

namespace ulib.Elements
{
    /// <summary>
    /// 这里单独列出 Node 类中的数据定义
    /// 这样做的目的是明确这些字段在序列化和编辑器里属性编辑时的作用，不跟功能逻辑和其他的用于逻辑实现的字段混杂在一起
    /// 
    /// 另一个做法是独立出单独的 NodeData 类，但这样的话继承体系中的每一个类都需要一个对应的 Data 类，
    /// 增加了一层间接性，系统的复杂度就加大了。
    /// 
    /// Attribute 的含义：
    ///     [JsonIgnore]        是指该属性不存储到文件里（暗含的意思是，在读取并重建对象后，这个字段是需要被恢复的）
    ///     [Browsable(false)]  是指该属性不显示在属性编辑的列表中，即 AdvPropertyGrid 控件里
    /// 
    /// </summary>
    public partial class Node
    {
        #region 常用无副作用属性（正常地序列化存取，属性界面可正常编辑）

        [Category("Node")]
        [Description("名字 (可指定，但需要在当前路径下唯一)")]
        public string Name { get; set; }
        [Category("Node")]
        [Description("相对父节点的位置 (影响所有子节点)")]
        public Point Position { get; set; }
        [Category("Node")]
        [Description("尺寸")]
        public Size Size { get; set; }
        [Category("Node")]
        [Description("是否可见 (影响所有子节点)")]
        public bool Visible { get; set; }
        [Category("Node")]
        [Description("外部 Dock，用于描述与父节点的关系，是节点通用属性")]
        public DockType Dock { get; set; }
        [Category("Node")]
        [Description("水平方向上的内部对齐（非一般对齐用途，目前仅用在 TextNode 的内部对齐上）")]
        public AlignHori AlignH { get; set; }
        [Category("Node")]
        [Description("垂直方向上的内部对齐（非一般对齐用途，目前仅用在 TextNode 的内部对齐上）")]
        public AlignVert AlignV { get; set; }
        [Category("Node")]
        [Description("外部间距")]
        public int Margin { get; set; }
        [Category("Node")]
        [Description("标记 (可任意起，不影响正常逻辑)")]
        public string Tag { get; set; }

        #endregion

        #region 序列化属性（正常地序列化存取，属性界面只读或不可见）

        [ReadOnly(true)]
        public string __type_info__ { get { return GetType().Name; } }

        [Browsable(false)]
        public virtual List<Node> Children { get { return m_children; } }

        #endregion
    }
}
