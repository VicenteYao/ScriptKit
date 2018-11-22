using System;
namespace ScriptKit
{
    public class JsNumber:JsObject
    {
        public JsNumber(int intValue)
        {
            IntPtr value = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsIntToNumber(intValue, out value);
            JsException.ThrowIfHasError(jsErrorCode);
            this.Value = value;
        }

        public JsNumber(double doubleValue)
        {
            IntPtr value = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsDoubleToNumber(doubleValue, out value);
            JsException.ThrowIfHasError(jsErrorCode);
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
            JsException.ThrowIfHasError(jsErrorCode);
            return value;
        }

        public double ToDouble()
        {
            double value = 0;
            JsErrorCode jsErrorCode = NativeMethods.JsNumberToDouble(this.Value, out value);
            JsException.ThrowIfHasError(jsErrorCode);
            return value;
        }

        public static implicit operator double(JsNumber jsNumber)
        {
            return jsNumber.ToDouble();
        }

        public static implicit operator JsNumber(double value)
        {
            return new JsNumber(value);
        }

        public static implicit operator int(JsNumber jsNumber)
        {
            return jsNumber.ToInt32();
        }

        public static implicit operator JsNumber(int value)
        {
            return new JsNumber(value);
        }

    }
}
