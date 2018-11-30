using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptKit
{
    public abstract class JsRuntimeObject
    {
        protected JsRuntimeObject()
        {

        }


        protected internal IntPtr Value { get; protected set; }


        public uint AddRef()
        {
            uint refCount = 0;
            JsErrorCode jsErrorCode = NativeMethods.JsAddRef(this.Value, out refCount);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return refCount;
        }

        public uint Release()
        {
            uint refCount = 0;
            JsErrorCode jsErrorCode = NativeMethods.JsRelease(this.Value, out refCount);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return refCount;
        }



    }
}
