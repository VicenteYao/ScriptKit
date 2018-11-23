using System;
namespace ScriptKit
{
    public class JsBoolean:JsValue
    {
        public JsBoolean(bool value)
        {
            IntPtr booleanValue = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsBoolToBoolean(value, out booleanValue);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
        }

        internal JsBoolean(IntPtr value)
        {
            this.Value = value;
        }

        public bool ToBoolean()
        {
            bool value = false;
            JsErrorCode jsErrorCode = NativeMethods.JsBooleanToBool(this.Value, out value);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return value;
        }
    }
}
