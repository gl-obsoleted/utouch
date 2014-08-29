using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ulib.Elements;

namespace ulib.Controls
{
    public class Control : Node
    {
        [JsonIgnore]
        public override List<Node> Children { get { return m_children; } }

        [JsonIgnore]
        [Browsable(false)]
        [Description("这个属性能够避免子节点位置，尺寸 Dock 等被修改. 对于控件类型，默认总是打开。")]
        public override bool LockChildrenLayoutRecursively { get { return true; } }
    }
}
