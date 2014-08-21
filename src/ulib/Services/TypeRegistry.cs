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
        public static void Init()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            foreach (Type t in asm.GetTypes())
            {
                if (t.IsSubclassOf(typeof(Node)) || t == typeof(Node))
                {
                    s_typeRegistry[t.Name] = t;
                }
            }
        }

        public static Type QueryType(string name)
        {
            Type t;
            s_typeRegistry.TryGetValue(name, out t);
            return t;
        }

        static Dictionary<string, Type> s_typeRegistry = new Dictionary<string,Type>();
    }
}
