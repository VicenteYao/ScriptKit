using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace ScriptKit
{
    public class JsArrayBuffer:JsObject
    {
        internal JsArrayBuffer(IntPtr value)
        {
            this.Value = value;
        }

        public JsArrayBuffer(uint length)
        {
            IntPtr arrayBuffer = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsCreateArrayBuffer(length, out arrayBuffer);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            this.Value = arrayBuffer;
        }

        public unsafe JsArrayBuffer(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            IntPtr externalArrayBuffer = IntPtr.Zero;
            fixed (byte* pData = data)
            {
                this.callbackState = GCHandle.ToIntPtr(GCHandle.Alloc(data, GCHandleType.Weak));
                JsErrorCode jsErrorCode = NativeMethods.JsCreateExternalArrayBuffer(new IntPtr(pData),
                                                          (uint)data.Length,
                                                          HandleJsFinalizeCallback,
                                                          this.callbackState,
                                                          out externalArrayBuffer);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);

            }
            this.Value = externalArrayBuffer;
        }

        private IntPtr callbackState { get; }

        static void HandleJsFinalizeCallback(IntPtr data)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(data);
            gcHandle.Free();
        }

        private void EnsureStorage()
        {
            if (this.buffer==IntPtr.Zero)
            {
                JsErrorCode jsErrorCode = NativeMethods.JsGetArrayBufferStorage(this.Value, out this.buffer, out this.length);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
            }

        }

        private IntPtr buffer;
        private uint length;

        private Stream stream;
        public unsafe Stream Stream
        {
            get
            {
                if (this.stream == null)
                {
                    this.EnsureStorage();
                    this.stream = new UnmanagedMemoryStream((byte*)this.buffer.ToPointer(), 0, this.length, FileAccess.ReadWrite);
                }
                return this.stream;
            }
        }

        public int Length
        {
            get
            {
                this.EnsureStorage();
                return (int)this.length;
            }
        }

    }
}
