using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScriptKit
{
    public class JsDataView : JsObject
    {
        internal JsDataView(IntPtr value)
        {
            this.Value = value;
            
        }

        public JsDataView(JsArrayBuffer arrayBuffer) : this(arrayBuffer, 0, (uint)arrayBuffer.Length)
        {

        }

        public JsDataView(JsArrayBuffer jsArrayBuffer, uint byteOffset, uint byteLength)
        {
            if (jsArrayBuffer == null)
            {
                throw new ArgumentNullException(nameof(jsArrayBuffer));
            }
            IntPtr dataView = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsCreateDataView(jsArrayBuffer.Value, byteOffset, byteLength, out dataView);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            this.Value = dataView;
        }

        private uint length;
        private IntPtr buffer;


        private Stream stream;
        public unsafe Stream Stream
        {
            get
            {
                this.EnsureStorage();
                if (this.stream == null)
                {
                    this.stream = new UnmanagedMemoryStream((byte*)this.buffer.ToPointer(), this.length);
                }
                return this.stream;
            }
        }

        private void EnsureStorage()
        {
            if (this.buffer==IntPtr.Zero)
            {
                JsErrorCode jsErrorCode = NativeMethods.JsGetDataViewStorage(this.Value, out this.buffer, out length);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
            }

        }
    }
}
