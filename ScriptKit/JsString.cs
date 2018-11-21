using System;
using System.Runtime.InteropServices;
namespace ScriptKit
{
    public class JsString:JsObject
    {
        public JsString(string str)
        {
            IntPtr content = Marshal.StringToHGlobalUni(str);
            IntPtr valueRef = IntPtr.Zero;
            JsErrorCode jsErrorCode= NativeMethods.JsCreateStringUtf16(content, new IntPtr(str.Length), out valueRef);
            JsException.ThrowIfHasError(jsErrorCode);
            this.ValueRef = valueRef;
        }

    }
}
