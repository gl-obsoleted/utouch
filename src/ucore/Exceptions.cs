using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ucore
{
    public class UCoreException : Exception
    {
        public UCoreException(string message) : base(message) { }
        public UCoreException(string message, Exception innerException) : base(message, innerException) { }
    }                          

    public class AssertionFailure : UCoreException
    {
        public AssertionFailure(string message) : base(message) { }
        public AssertionFailure(string message, Exception innerException) : base(message, innerException) { }
    }

    public class AppInitError_Fatal : UCoreException
    {
        public AppInitError_Fatal(string message) : base(message) { }
        public AppInitError_Fatal(string message, Exception innerException) : base(message, innerException) { }
    }
}
