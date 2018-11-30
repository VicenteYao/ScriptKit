using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ScriptKit
{
    public  class JsObject:JsValue
    {
        public JsObject()
        {
            IntPtr obj = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsCreateObject(out obj);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            this.Value = obj;
        }

        public JsObject(object externalData)
        {
            IntPtr obj = IntPtr.Zero;
            GCHandle externalDataGCHandle = GCHandle.Alloc(externalData, GCHandleType.Weak);
            JsErrorCode jsErrorCode = NativeMethods.JsCreateExternalObject(GCHandle.ToIntPtr(externalDataGCHandle),
                                                                           HandleJsFinalizeCallback,
                                                                           out obj);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            this.Value = obj;
        }

        private static void HandleJsFinalizeCallback(IntPtr data)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(data);
            gcHandle.Free();
        }


        public object ExternalData
        {
            get
            {
                IntPtr externalData = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetExternalData(this.Value, out externalData);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                GCHandle handle = GCHandle.FromIntPtr(externalData);
                return handle.Target;
            }
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
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return new JsObject(prototypeObject);
            }
            set
            {
                JsErrorCode jsErrorCode = NativeMethods.JsSetPrototype(this.Value, value.Value);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
            }
        }

        public string[] OwnPropertyNames
        {
            get
            {
                IntPtr propertyNames = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetOwnPropertyNames(this.Value, out propertyNames);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                JsArray jsArray = FromIntPtr(propertyNames) as JsArray;
                int length = jsArray.Length;
                string[] ownPropertyNames = new string[length];
                for (int i = 0; i < length; i++)
                {
                    ownPropertyNames[i] = jsArray[i].ConverToString();
                }
                return ownPropertyNames;
            }
        }

        public JsSymbol[] OwnPropertySymbols
        {

            get
            {
                IntPtr propertySymbols = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetOwnPropertySymbols(this.Value, out propertySymbols);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                JsArray jsArray = FromIntPtr(propertySymbols) as JsArray;
                int length = jsArray.Length;
                JsSymbol[] ownPropertySymbols = new JsSymbol[length];
                for (int i = 0; i < length; i++)
                {
                    ownPropertySymbols[i] = jsArray[i] as JsSymbol;
                }
                return ownPropertySymbols;
            }
        }

        public bool IsExtensible
        {
            get
            {
                bool isExtensible = false;
                JsErrorCode jsErrorCode = NativeMethods.JsGetExtensionAllowed(this.Value, out isExtensible);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return isExtensible;
            }
        }

        public void PreventExtension()
        {
            JsErrorCode jsErrorCode = NativeMethods.JsPreventExtension(this.Value);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
        }




        public JsValue this[string propertyName]
        {
            get
            {
                IntPtr propertyId= GetPropertyIdFromString(propertyName);
                IntPtr result = IntPtr.Zero;
                JsErrorCode jsErrorCode= NativeMethods.JsGetProperty(this.Value, propertyId, out result);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return FromIntPtr(result);
            }
            set
            {
                IntPtr propertyId = GetPropertyIdFromString(propertyName);
                JsErrorCode jsErrorCode = NativeMethods.JsSetProperty(this.Value, propertyId, value.Value, false);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
            }
        }

        public JsValue this[JsSymbol symbol]
        {
            get
            {
                IntPtr propertyId = GetPropertyIdFromSymbol(symbol);
                IntPtr result = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetProperty(this.Value, propertyId, out result);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return FromIntPtr(result);
            }
            set
            {
                IntPtr propertyId = GetPropertyIdFromSymbol(symbol);
                JsErrorCode jsErrorCode = NativeMethods.JsSetProperty(this.Value, propertyId, value.Value, false);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
            }
        }

        private unsafe static IntPtr GetPropertyIdFromString(string propertyName)
        {
            Span<byte> bytes = Encoding.UTF8.GetBytes(propertyName);
            IntPtr propertyId = IntPtr.Zero;
            fixed (byte* pBytes = bytes)
            {
                JsErrorCode jsErrorCode = NativeMethods.JsCreatePropertyId(new IntPtr(pBytes), new IntPtr(bytes.Length), out propertyId);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
            }
            return propertyId;
        }

        private static IntPtr GetPropertyIdFromSymbol(JsSymbol symbol){
            IntPtr propertyIdRef = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsGetPropertyIdFromSymbol(symbol.Value, out propertyIdRef);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return propertyIdRef;
        }

        public JsValue this[int index]
        {
            get
            {
                IntPtr result;
                JsNumber indexNumber = new JsNumber(index);
                JsErrorCode jsErrorCode = NativeMethods.JsGetIndexedProperty(this.Value, indexNumber.Value, out result);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return FromIntPtr(result);
            }
            set
            {
                JsNumber indexNumber = new JsNumber(index);
                JsErrorCode jsErrorCode = NativeMethods.JsSetIndexedProperty(this.Value, indexNumber.Value, value.Value);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
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
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
        }

        public void DeleteIndexedProperty(int index)
        {
            JsNumber indexNumber = new JsNumber(index);
            JsErrorCode jsErrorCode = NativeMethods.JsDeleteIndexedProperty(this.Value, indexNumber.Value);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
        }

        public bool InstanceOf(JsFunction jsFunction)
        {
            bool result = false;
            JsErrorCode jsErrorCode = NativeMethods.JsInstanceOf(this.Value, jsFunction.Value, out result);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return result;
        }

        public JsValue GetOwnPropertyDescriptor(string propertyName)
        {
            IntPtr propertyId = GetPropertyIdFromString(propertyName);
            IntPtr propertyDescriptor = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsGetOwnPropertyDescriptor(this.Value, propertyId, out propertyDescriptor);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return FromIntPtr(propertyDescriptor);
        }

        public JsWeakReference CreateWeakReference()
        {
            IntPtr weakRef = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsCreateWeakReference(this.Value, out weakRef);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return new JsWeakReference(weakRef);
        }

        public  ProxyProperties{

    }
}
