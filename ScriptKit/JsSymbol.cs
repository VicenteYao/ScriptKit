using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptKit
{
    public class JsSymbol : JsObject
    {
        internal JsSymbol(IntPtr value)
        {
            this.Value = value;
        }

    }
}
