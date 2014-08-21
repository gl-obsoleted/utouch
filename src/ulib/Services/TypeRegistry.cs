using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ulib.Elements;

namespace ulib
{
    public class TypeRegistry
    {
        public static TypeRegistry Instance = new TypeRegistry();

        public TypeRegistry()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            foreach (Type t in asm.GetTypes())
            {
                if (t.IsSubclassOf(typeof(Node)) || t == typeof(Node))
                {
                    m_typeRegistry[t.Name] = t;
                }
            }
        }

        public Type QueryType(string name)
        {
            Type t;
            m_typeRegistry.TryGetValue(name, out t);
            return t;
        }

        private Dictionary<string, Type> m_typeRegistry = new Dictionary<string,Type>();
    }
}
