using System;
namespace ScriptKit
{
    public class JsError:JsObject
    {
        internal JsError(IntPtr value)
        {
            this.Value = value;
        }

    }
}
