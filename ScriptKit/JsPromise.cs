using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptKit
{
    public class JsPromise:JsObject
    {
        public JsPromise()
        {
            IntPtr promise = IntPtr.Zero;
            IntPtr resolve = IntPtr.Zero;
            IntPtr reject = IntPtr.Zero;
            NativeMethods.JsCreatePromise(out promise, out resolve, out reject);
            this.Resolve = new JsFunction(resolve);
            this.Reject = new JsFunction(reject);
        }


        public JsFunction Resolve { get; private set; }

        public JsFunction Reject { get; private set; }

        public JsPromiseState State
        {
            get
            {
                JsPromiseState jsPromiseState = JsPromiseState.JsPromiseStateFulfilled;
                JsErrorCode jsErrorCode = NativeMethods.JsGetPromiseState(this.Value, out jsPromiseState);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return jsPromiseState;
            }
        }

        public JsValue Result
        {
            get
            {
                IntPtr value = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetPromiseResult(this.Value, out value);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return FromIntPtr(value);
            }
        }

    }
}
