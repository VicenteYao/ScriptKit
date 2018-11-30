using System;
using System.IO;

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

        private IntPtr buffer;
        private uint bufferLength;
        private int length;
        private JsTypedArrayType arrayType;
        private Stream stream;

        public int Length{
            get{
                return 0;
            }
        }

        public JsTypedArrayType ArrayType{
            get
            {
                this.EnsureStorage();
                return this.arrayType;
            }
        }

        private void EnsureStorage()
        {
            if (this.buffer == IntPtr.Zero)
            {
                JsErrorCode jsErrorCode = NativeMethods.JsGetTypedArrayStorage(this.Value, out this.buffer, out this.bufferLength, out this.arrayType, out length);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);

            }
        }

        public unsafe Stream Stream{
            get{
                this.EnsureStorage();
                if (this.stream==null)
                {
                    this.stream = new UnmanagedMemoryStream((byte*)this.buffer.ToPointer(), this.bufferLength);
                }
                return this.Stream;
            }
        }

    }
}
