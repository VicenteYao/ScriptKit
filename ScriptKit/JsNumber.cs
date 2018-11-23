using System;
namespace ScriptKit
{
    public class JsNumber:JsValue
    {
        internal JsNumber(int intValue)
        {
            IntPtr value = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsIntToNumber(intValue, out value);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            this.Value = value;
        }

        public JsNumber(double doubleValue)
        {
            IntPtr value = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsDoubleToNumber(doubleValue, out value);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            this.Value = value;
        }

        internal JsNumber(IntPtr value)
        {
            this.Value = value;
        }

        public int ToInt32()
        {
            int value = 0;
            JsErrorCode jsErrorCode = NativeMethods.JsNumberToInt(this.Value, out value);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return value;
        }

        public double ToDouble()
        {
            double value = 0;
            JsErrorCode jsErrorCode = NativeMethods.JsNumberToDouble(this.Value, out value);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return value;
        }



    }
}
