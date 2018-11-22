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
            Marshal.FreeCoTaskMem(content);
            this.Value = valueRef;
        }

        internal JsString(IntPtr stringValue)
        {
            this.Value = stringValue;
        }

        public int Length
        {
            get
            {
                int length = 0;
                NativeMethods.JsGetStringLength(this.Value, out length);
                return length;
            }
        }


        public unsafe override string ToString()
        {
            IntPtr pStr = IntPtr.Zero;
            IntPtr pWritten = IntPtr.Zero;
            char[] buffer = new char[this.Length];
            fixed (char* pString = buffer)
            {
                JsErrorCode jsErrorCode = NativeMethods.JsCopyStringUtf16(this.Value, 0, this.Length, new IntPtr(pString), out pWritten);
                JsException.ThrowIfHasError(jsErrorCode);
                return new string(pString);
            }
        }

    }
}
