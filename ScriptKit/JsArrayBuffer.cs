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

    }
}
