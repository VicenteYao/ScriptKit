using System;
namespace ScriptKit
{
    public delegate IntPtr JsNativeFunction(IntPtr calle, bool isConstructCall, IntPtr arguments, ushort argumentCount, IntPtr callbackState);
}
