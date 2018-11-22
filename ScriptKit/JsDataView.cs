using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptKit
{
    public class JsDataView : JsObject
    {
        internal JsDataView(IntPtr value)
        {
            this.Value = value;
        }

    }
}
