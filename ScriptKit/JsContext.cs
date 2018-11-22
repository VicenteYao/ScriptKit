using System;
namespace ScriptKit
{
    public class JsContext
    {
        private IntPtr ctx;

        internal JsContext(IntPtr ctx)
        {
            this.ctx = ctx;
        }
        public JsObject Global {
            get
            {
                IntPtr globelValueRef = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetGlobalObject(out globelValueRef);
                JsException.ThrowIfHasError(jsErrorCode);
                return JsObject.FromIntPtr(globelValueRef);
            }
        }

       public JsObject Null
        {
            get
            {
                IntPtr nullValue = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetNullValue(out nullValue);
                JsException.ThrowIfHasError(jsErrorCode);
                return JsObject.FromIntPtr(nullValue);
            }
        }

        public JsObject True
        {
            get
            {
                IntPtr trueValue = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetTrueValue(out trueValue);
                JsException.ThrowIfHasError(jsErrorCode);
                return JsObject.FromIntPtr(trueValue);
            }
        }

        public JsObject False
        {
            get
            {
                IntPtr falseValue = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetFalseValue(out falseValue);
                JsException.ThrowIfHasError(jsErrorCode);
                return JsObject.FromIntPtr(falseValue);
            }
        }

        public JsObject Undefined
        {
            get
            {
                IntPtr undefinedValue = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetUndefinedValue(out undefinedValue);
                JsException.ThrowIfHasError(jsErrorCode);
                return JsObject.FromIntPtr(undefinedValue);
            }
        }

        public void Run(string script)
        {
            JsString scriptString = new JsString(script);
            IntPtr result = IntPtr.Zero;
            JsErrorCode jsErrorCode= NativeMethods.JsRun(scriptString.Value, IntPtr.Zero, scriptString.Value, JsParseScriptAttributes.JsParseScriptAttributeNone, out result);
            JsException.ThrowIfHasError(jsErrorCode);
        }
    }
}
