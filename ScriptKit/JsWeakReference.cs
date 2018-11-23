using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptKit
{
    public class JsWeakReference
    {
        internal JsWeakReference(IntPtr value)
        {
            this.Value = value;
        }

        private IntPtr Value { get; set; }

        public JsObject Target
        {
            get
            {
                IntPtr targetValue = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetWeakReferenceValue(this.Value, out targetValue);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return new JsObject(targetValue);
            }
        }
    }
}
