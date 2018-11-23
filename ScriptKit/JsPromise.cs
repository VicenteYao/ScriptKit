using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptKit
{
    public class JsPromise:JsObject
    {
        public JsPromise()
        {
          
        }

        public JsPromiseState State
        {
            get
            {
                JsPromiseState jsPromiseState = JsPromiseState.JsPromiseStateFulfilled;
                JsErrorCode jsErrorCode = NativeMethods.JsGetPromiseState(this.Value, out jsPromiseState);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
                return jsPromiseState;
            }
        }

        public JsValue Result
        {
            get
            {
                IntPtr value = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetPromiseResult(this.Value, out value);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
                return FromIntPtr(value);
            }
        }

    }
}
