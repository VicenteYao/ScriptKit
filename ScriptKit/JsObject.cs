using System;
using System.Text;

namespace ScriptKit
{
    public  class JsObject:JsValue
    {
        public JsObject()
        {
            IntPtr obj = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsCreateObject(out obj);
            JsRuntimeException.ThrowIfHasError(jsErrorCode);
            this.Value = obj;
        }

        internal JsObject(IntPtr value)
        {
            this.Value = value;
        }

        public JsContext Context
        {
            get
            {
                return JsRuntime.GetContextOfObject(this);
            }
        }

        public JsObject Prototype
        {
            get
            {
                IntPtr prototypeObject;
                JsErrorCode jsErrorCode = NativeMethods.JsGetPrototype(this.Value, out prototypeObject);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
                return new JsObject(prototypeObject);
            }
            set
            {
                JsErrorCode jsErrorCode = NativeMethods.JsSetPrototype(this.Value, value.Value);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
            }
        }

        public string[] OwnPropertyNames
        {
            get
            {
                IntPtr propertyNames = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetOwnPropertyNames(this.Value, out propertyNames);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
                JsArray jsArray = FromIntPtr(propertyNames) as JsArray;
                int length = jsArray["length"].ConvertToJsNumber().ToInt32();
                string[] ownPropertyNames = new string[length];
                for (int i = 0; i < length; i++)
                {
                    ownPropertyNames[i] = jsArray[i].ConverToString();
                }
                return ownPropertyNames;
            }
        }

        public JsValue this[string propertyName]
        {
            get
            {
                IntPtr propertyId= GetPropertyIdFromString(propertyName);
                IntPtr result = IntPtr.Zero;
                JsErrorCode jsErrorCode= NativeMethods.JsGetProperty(this.Value, propertyId, out result);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
                return FromIntPtr(result);
            }
            set
            {
                IntPtr propertyId = GetPropertyIdFromString(propertyName);
                JsErrorCode jsErrorCode = NativeMethods.JsSetProperty(this.Value, propertyId, value.Value, false);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
            }
        }

        private unsafe static IntPtr GetPropertyIdFromString(string propertyName)
        {
            Span<byte> bytes = Encoding.UTF8.GetBytes(propertyName);
            IntPtr propertyId = IntPtr.Zero;
            fixed (byte* pBytes = bytes)
            {
                JsErrorCode jsErrorCode = NativeMethods.JsCreatePropertyId(new IntPtr(pBytes), new IntPtr(bytes.Length), out propertyId);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
            }
            return propertyId;
        }

        public JsValue this[int index]
        {
            get
            {
                IntPtr result;
                JsNumber indexNumber = new JsNumber(index);
                JsErrorCode jsErrorCode = NativeMethods.JsGetIndexedProperty(this.Value, indexNumber.Value, out result);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
                return FromIntPtr(result);
            }
            set
            {
                JsNumber indexNumber = new JsNumber(index);
                JsErrorCode jsErrorCode = NativeMethods.JsSetIndexedProperty(this.Value, indexNumber.Value, value.Value);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
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
            JsRuntimeException.ThrowIfHasError(jsErrorCode);
        }

        public void DeleteIndexedProperty(int index)
        {
            JsNumber indexNumber = new JsNumber(index);
            JsErrorCode jsErrorCode = NativeMethods.JsDeleteIndexedProperty(this.Value, indexNumber.Value);
            JsRuntimeException.ThrowIfHasError(jsErrorCode);
        }

        public bool InstanceOf(JsFunction jsFunction)
        {
            bool result = false;
            JsErrorCode jsErrorCode = NativeMethods.JsInstanceOf(this.Value, jsFunction.Value, out result);
            JsRuntimeException.ThrowIfHasError(jsErrorCode);
            return result;
        }

        public JsValue GetOwnPropertyDescriptor(string propertyName)
        {
            IntPtr propertyId = GetPropertyIdFromString(propertyName);
            IntPtr propertyDescriptor = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsGetOwnPropertyDescriptor(this.Value, propertyId, out propertyDescriptor);
            JsRuntimeException.ThrowIfHasError(jsErrorCode);
            return FromIntPtr(propertyDescriptor);
        }

        public JsWeakReference CreateWeakReference()
        {
            IntPtr weakRef = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsCreateWeakReference(this.Value, out weakRef);
            JsRuntimeException.ThrowIfHasError(jsErrorCode);
            return new JsWeakReference(weakRef);
        }

    }
}
