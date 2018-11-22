using System;
using System.Text;

namespace ScriptKit
{
    public  class JsObject
    {
        public JsObject()
        {
            
        }

        private  JsObject(IntPtr value)
        {
            this.Value = value;
        }

        public static JsObject FromIntPtr(IntPtr value)
        {
            JsValueType jsValueType = JsValueType.JsUndefined;
            JsErrorCode jsErrorCode = NativeMethods.JsGetValueType(value, out jsValueType);
            JsException.ThrowIfHasError(jsErrorCode);
            JsObject jsObject = null;
            switch (jsValueType)
            {
                case JsValueType.JsUndefined:
                    jsObject = new JsObject(value);
                    break;
                case JsValueType.JsNull:
                    jsObject = new JsObject(value);
                    break;
                case JsValueType.JsNumber:
                    jsObject = new JsNumber(value);
                    break;
                case JsValueType.JsString:
                    jsObject = new JsString(value);
                    break;
                case JsValueType.JsBoolean:
                    jsObject = new JsBoolean(value);
                    break;
                case JsValueType.JsObject:
                    jsObject = new JsObject(value);
                    break;
                case JsValueType.JsFunction:
                    jsObject = new JsFunction(value);
                    break;
                case JsValueType.JsError:
                    jsObject = new JsError(value);
                    break;
                case JsValueType.JsArray:
                    jsObject = new JsArray(value);
                    break;
                case JsValueType.JsSymbol:
                    jsObject = new JsSymbol(value);
                    break;
                case JsValueType.JsArrayBuffer:
                    jsObject = new JsArrayBuffer(value);
                    break;
                case JsValueType.JsTypedArray:
                    jsObject = new JsTypedArray(value);
                    break;
                case JsValueType.JsDataView:
                    jsObject = new JsDataView(value);
                    break;
                default:
                    break;
            }
            return jsObject;
        }

        public static IntPtr ToIntPtr(JsObject jsObject)
        {
            if (jsObject == null)
            {
                return IntPtr.Zero;
            }
            return jsObject.Value;
        }


        protected internal IntPtr Value { get; protected set; }

        public  string ConverToString()
        {
            return this.ConvertToJsString().ToString();
        }
        public  JsNumber ConvertToJsNumber()
        {
            IntPtr value = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsConvertValueToNumber(this.Value, out value);
            JsException.ThrowIfHasError(jsErrorCode);
            return new JsNumber(value);
        }
        public  JsBoolean ConvertToJsBoolean()
        {
            IntPtr value = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsConvertValueToBoolean(this.Value, out value);
            JsException.ThrowIfHasError(jsErrorCode);
            return new JsBoolean(value);
        }
        public  JsString ConvertToJsString()
        {
            IntPtr value = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsConvertValueToString(this.Value, out value);
            JsException.ThrowIfHasError(jsErrorCode);
            return new JsString(value);
        }
        public JsObject ConverToJsObject()
        {
            if (this.ValueType == JsValueType.JsObject)
            {
                return this;
            }
            IntPtr value = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsConvertValueToObject(this.Value, out value);
            return new JsObject(value);
        }

        public JsObject Prototype
        {
            get
            {
                IntPtr prototypeObject;
                JsErrorCode jsErrorCode = NativeMethods.JsGetPrototype(this.Value, out prototypeObject);
                JsException.ThrowIfHasError(jsErrorCode);
                return new JsObject(prototypeObject);
            }
            set
            {
                JsErrorCode jsErrorCode = NativeMethods.JsSetPrototype(this.Value, value.Value);
                JsException.ThrowIfHasError(jsErrorCode);
            }
        }

        public JsContext Context
        {
            get
            {
                return JsRuntime.GetContextOfObject(this);
            }
        }

        public JsValueType ValueType
        {
            get
            {
                JsValueType jsValueType = JsValueType.JsUndefined;
                JsErrorCode jsErrorCode= NativeMethods.JsGetValueType(this.Value, out jsValueType);
                JsException.ThrowIfHasError(jsErrorCode);
                return jsValueType;
            }
        }

        public override string ToString()
        {
            return this.ConverToString();
        }

        public string[] OwnPropertyNames
        {
            get
            {
                IntPtr propertyNames = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetOwnPropertyNames(this.Value, out propertyNames);
                JsException.ThrowIfHasError(jsErrorCode);
                JsArray jsArray = JsObject.FromIntPtr(propertyNames) as JsArray;
                int length = jsArray["length"].ConvertToJsNumber().ToInt32();
                string[] ownPropertyNames = new string[length];
                for (int i = 0; i < length; i++)
                {
                    ownPropertyNames[i] = jsArray[i].ConverToString();
                }
                return ownPropertyNames;
            }
        }

        public JsObject this[string propertyName]
        {
            get
            {
                IntPtr propertyId= GetPropertyIdFromString(propertyName);
                IntPtr result = IntPtr.Zero;
                JsErrorCode jsErrorCode= NativeMethods.JsGetProperty(this.Value, propertyId, out result);
                JsException.ThrowIfHasError(jsErrorCode);
                return new JsObject(result);
            }
            set
            {
                IntPtr propertyId = GetPropertyIdFromString(propertyName);
                JsErrorCode jsErrorCode = NativeMethods.JsSetProperty(this.Value, propertyId, value.Value, false);
                JsException.ThrowIfHasError(jsErrorCode);
            }
        }

        private unsafe static IntPtr GetPropertyIdFromString(string propertyName)
        {
            Span<byte> bytes = Encoding.UTF8.GetBytes(propertyName);
            IntPtr propertyId = IntPtr.Zero;
            fixed (byte* pBytes = bytes)
            {
                JsErrorCode jsErrorCode = NativeMethods.JsCreatePropertyId(new IntPtr(pBytes), new IntPtr(bytes.Length), out propertyId);
                JsException.ThrowIfHasError(jsErrorCode);
            }
            return propertyId;
        }

        public JsObject this[int index]
        {
            get
            {
                IntPtr result;
                JsNumber indexNumber = new JsNumber(index);
                JsErrorCode jsErrorCode = NativeMethods.JsGetIndexedProperty(this.Value, indexNumber.Value, out result);
                JsException.ThrowIfHasError(jsErrorCode);
                return FromIntPtr(result);
            }
            set
            {
                JsNumber indexNumber = new JsNumber(index);
                JsErrorCode jsErrorCode = NativeMethods.JsSetIndexedProperty(this.Value, indexNumber.Value, value.Value);
                JsException.ThrowIfHasError(jsErrorCode);
            }
        }

        public void DefineProperty(string propertyName)
        {
            //IntPtr propertyId = GetPropertyIdFromString(propertyName);
            //NativeMethods.JsDefineProperty(this.Value,propertyId,)
        }

        public void DeleteProperty(string propertyName)
        {
            IntPtr propertyId = GetPropertyIdFromString(propertyName);
            bool result = false;
            JsErrorCode jsErrorCode = NativeMethods.JsDeleteProperty(this.Value, propertyId, false, out result);
            JsException.ThrowIfHasError(jsErrorCode);
        }

        public void DeleteIndexedProperty(int index)
        {
            JsNumber indexNumber = new JsNumber(index);
            JsErrorCode jsErrorCode = NativeMethods.JsDeleteIndexedProperty(this.Value, indexNumber.Value);
            JsException.ThrowIfHasError(jsErrorCode);
        }

        public static bool operator ==(JsObject left, JsObject right)
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
            JsException.ThrowIfHasError(jsErrorCode);
            return result;
        }

        public static bool operator !=(JsObject left, JsObject right)
        {
            return (left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is JsObject)
            {
                bool result = false;
                JsObject right = obj as JsObject;
                JsErrorCode jsErrorCode = NativeMethods.JsStrictEquals(this.Value, right.Value, out result);
                JsException.ThrowIfHasError(jsErrorCode);
                return result;
            }
            return false;
        }


    }
}
