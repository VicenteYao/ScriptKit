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

        private uint refCount;
        public uint RefCount { get { return this.refCount; } }

        public void AddRef()
        {
            NativeMethods.JsAddRef(this.Value, out this.refCount);
        }

        public void Release()
        {
            NativeMethods.JsRelease(this.Value, out this.refCount);
        }

    }
}
