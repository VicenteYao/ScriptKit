using System;
namespace ScriptKit
{
    public class JsTypedArray:JsObject
    {
        internal JsTypedArray(IntPtr value)
        {
            this.Value = value;
        }

        public JsTypedArray(JsTypedArrayType jsTypedArrayType, uint elementLength)
        {
            IntPtr value = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsCreateTypedArray(jsTypedArrayType, IntPtr.Zero, 0, elementLength, out value);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            this.Value = value;
        }

        public JsTypedArray(JsTypedArrayType jsTypedArrayType,JsArray baseArray){
            IntPtr value = IntPtr.Zero;
            JsErrorCode jsErrorCode= NativeMethods.JsCreateTypedArray(jsTypedArrayType, baseArray.Value, 0, 0,out value);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            this.Value = value;
        }

        public JsTypedArray(JsTypedArrayType jsTypedArrayType, JsArrayBuffer baseArray,uint byteOffset,uint elementLength)
        {
            IntPtr value = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsCreateTypedArray(jsTypedArrayType, baseArray.Value, byteOffset, elementLength, out value);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            this.Value = value;
        }
    }
}
