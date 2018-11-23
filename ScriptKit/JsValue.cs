using System;
namespace ScriptKit
{
    public  class JsValue:JsRuntimeObject
    {
        protected  JsValue()
        {

        }

        private JsValue(IntPtr value)
        {
            this.Value = value;
        }

        public static JsValue FromIntPtr(IntPtr value)
        {
            JsValueType jsValueType = JsValueType.JsUndefined;
            JsErrorCode jsErrorCode = NativeMethods.JsGetValueType(value, out jsValueType);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            JsValue jsValue = null;
            switch (jsValueType)
            {
                case JsValueType.JsArray:
                    jsValue = new JsArray(value);
                    break;
                case JsValueType.JsArrayBuffer:
                    jsValue = new JsArrayBuffer(value);
                    break;
                case JsValueType.JsBoolean:
                    jsValue = new JsBoolean(value);
                    break;
                case JsValueType.JsDataView:
                    jsValue = new JsDataView(value);
                    break;
                case JsValueType.JsError:
                    jsValue = new JsError(value);
                    break;
                case JsValueType.JsFunction:
                    jsValue = new JsFunction(value);
                    break;
                case JsValueType.JsNull:
                    jsValue = new JsValue(value);
                    break;
                case JsValueType.JsNumber:
                    jsValue = new JsNumber(value);
                    break;
                case JsValueType.JsObject:
                    jsValue = new JsObject(value);
                    break;
                case JsValueType.JsString:
                    jsValue = new JsString(value);
                    break;
                case JsValueType.JsSymbol:
                    jsValue = new JsSymbol(value);
                    break;
                case JsValueType.JsTypedArray:
                    jsValue = new JsTypedArray(value);
                    break;
                case JsValueType.JsUndefined:
                    jsValue = new JsValue(value);
                    break;
                default:
                    break;
            }
            return jsValue;
        }


        public string ConverToString()
        {
            return this.ConvertToJsString().ToString();
        }
        public JsNumber ConvertToJsNumber()
        {
            IntPtr value = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsConvertValueToNumber(this.Value, out value);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return new JsNumber(value);
        }
        public JsBoolean ConvertToJsBoolean()
        {
            IntPtr value = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsConvertValueToBoolean(this.Value, out value);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return new JsBoolean(value);
        }
        public JsString ConvertToJsString()
        {
            IntPtr value = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsConvertValueToString(this.Value, out value);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return new JsString(value);
        }
        public JsObject ConverToJsObject()
        {
            IntPtr value = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsConvertValueToObject(this.Value, out value);
            return new JsObject(value);
        }

        public JsValueType ValueType
        {
            get
            {
                JsValueType jsValueType = JsValueType.JsUndefined;
                JsErrorCode jsErrorCode = NativeMethods.JsGetValueType(this.Value, out jsValueType);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return jsValueType;
            }
        }


        public static bool operator ==(JsValue left, JsValue right)
        {
            if (object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
            {
                return true;
            }
            if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null))
            {
                return false;
            }
            bool result = false;
            JsErrorCode jsErrorCode = NativeMethods.JsEquals(left.Value, right.Value, out result);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return result;
        }

        public static bool operator !=(JsValue left, JsValue right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is JsObject)
            {
                bool result = false;
                JsObject right = obj as JsObject;
                JsErrorCode jsErrorCode = NativeMethods.JsStrictEquals(this.Value, right.Value, out result);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return result;
            }
            return false;
        }

        public override string ToString()
        {
            return this.ConverToString();
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
