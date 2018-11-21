using System;
using System.Text;

namespace ScriptKit
{
    public  class JsObject
    {
        public JsObject()
        {
            
        }

        private JsObject(IntPtr value)
        {
            this.ValueRef = value;
        }

        protected internal static JsObject FromIntPtr(IntPtr valueRef)
        {
            //IntPtr obj = IntPtr.Zero;
            //JsErrorCode jsErrorCode = NativeMethods.JsConvertValueToObject(valueRef, out obj);
            //JsException.ThrowIfHasError(jsErrorCode);
            return new JsObject(valueRef);
        }


        protected internal IntPtr ValueRef { get; protected set; }

        public  string ConvertoString()
        {
            return null;
        }
        public  JsNumber ConvertToNumber()
        {
            return null;
        }
        public  JsBoolean ConvertToBoolean()
        {
            return null;
        }
        public  JsString ConvertToString()
        {
            return null;
        }
        public  JsObject ConverToObject()
        {
            return null;
        }

        private JsObject _prototype;
        public JsObject prototype { get; set; }

        public JsValueType ValueType
        {
            get
            {
                JsValueType jsValueType = JsValueType.JsUndefined;
                JsErrorCode jsErrorCode= NativeMethods.JsGetValueType(this.ValueRef, out jsValueType);
                JsException.ThrowIfHasError(jsErrorCode);
                return jsValueType;
            }
        }

        public override string ToString()
        {
            return this.ConvertoString();
        }

        public JsObject this[string index]
        {
            get
            {
                IntPtr propertyId= GetPropertyIdFromString(index);
                IntPtr result = IntPtr.Zero;
                JsErrorCode jsErrorCode= NativeMethods.JsGetProperty(this.ValueRef, propertyId, out result);
                JsException.ThrowIfHasError(jsErrorCode);
                return new JsObject(result);
            }
            set
            {
                IntPtr propertyId = GetPropertyIdFromString(index);
                JsErrorCode jsErrorCode = NativeMethods.JsSetProperty(this.ValueRef, propertyId, value.ValueRef, false);
                JsException.ThrowIfHasError(jsErrorCode);
            }
        }

        private unsafe static IntPtr GetPropertyIdFromString(string index)
        {
            Span<byte> bytes = Encoding.UTF8.GetBytes(index);
            IntPtr propertyId = IntPtr.Zero;
            fixed (byte* b = bytes)
            {
                JsErrorCode jsErrorCode = NativeMethods.JsCreatePropertyId(new IntPtr(b), new IntPtr(bytes.Length), out propertyId);
                JsException.ThrowIfHasError(jsErrorCode);
            }
            return propertyId;
        }

        public JsObject this[int index]
        {
            get {
                return null;
            }
            set
            {

            }
        }


    }
}
