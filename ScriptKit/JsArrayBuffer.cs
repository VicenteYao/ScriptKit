using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptKit
{
    public class JsArrayBuffer:JsObject
    {
        internal JsArrayBuffer(IntPtr value)
        {
            this.Value = value;
        }

        public JsArrayBuffer(byte[] buffer)
        {
            IntPtr result = IntPtr.Zero;
            uint byteLength = buffer == null ? 0 : (uint)buffer.Length;
            NativeMethods.JsCreateArrayBuffer(byteLength, out result);
        }

    }
}
