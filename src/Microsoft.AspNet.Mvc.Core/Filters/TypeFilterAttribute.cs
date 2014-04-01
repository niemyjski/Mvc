using System;
using System.Diagnostics;

namespace Microsoft.AspNet.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    [DebuggerDisplay("TypeFilter: Type={ImplementationType} Order={Order}")]
    public class TypeFilterAttribute : Attribute, ITypeFilter
    {
        public TypeFilterAttribute(Type type)
        {
            ImplementationType = type;
        }

        public int Order { get; set; }

        public Type ImplementationType
        {
            get;
            private set;
        }

        public object[] parameters
        {
            get;
            set;
        }
    }
}
