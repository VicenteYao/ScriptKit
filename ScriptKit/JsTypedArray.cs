using System;
namespace ScriptKit
{
    public class JsTypedArray:JsObject
    {
        internal JsTypedArray(IntPtr value)
        {
            this.Value = value;
        }
    }
}
