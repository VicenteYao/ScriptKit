using System;
namespace ScriptKit
{
    public class JsBoolean:JsObject
    {
        public JsBoolean(bool value)
        {
            IntPtr booleanValue = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsBoolToBoolean(value, out booleanValue);
            JsException.ThrowIfHasError(jsErrorCode);
        }

        public JsBoolean(IntPtr value)
        {
            this.Value = value;
        }

        public bool ToBoolean()
        {
            bool value = false;
            JsErrorCode jsErrorCode = NativeMethods.JsBooleanToBool(this.Value, out value);
            JsException.ThrowIfHasError(jsErrorCode);
        }
    }
}
