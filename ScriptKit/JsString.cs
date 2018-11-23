using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace ScriptKit
{
    public class JsString:JsObject
    {
        public unsafe JsString(string str) : base(IntPtr.Zero)
        {
            ReadOnlySpan<char> stringSpan = str.AsSpan();
            IntPtr valueRef = IntPtr.Zero;
            fixed (char* pString = stringSpan)
            {
                JsErrorCode jsErrorCode = NativeMethods.JsCreateStringUtf16(new IntPtr(pString), new IntPtr(str.Length), out valueRef);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
            }
            this.Value = valueRef;
        }

        internal JsString(IntPtr stringValue) : base(IntPtr.Zero)
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
            char* buffer = stackalloc char[this.Length];
            JsErrorCode jsErrorCode = NativeMethods.JsCopyStringUtf16(this.Value, 0, this.Length, new IntPtr(buffer), out pWritten);
            JsRuntimeException.ThrowIfHasError(jsErrorCode);
            return new string(buffer, 0, pWritten.ToInt32());
        }

    }
}
