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

        private JsObject global;
        public JsObject Global {
            get
            {
                if (global==null)
                {
                    IntPtr globelValueRef = IntPtr.Zero;
                    JsErrorCode jsErrorCode = NativeMethods.JsGetGlobalObject(out globelValueRef);
                    JsException.ThrowIfHasError(jsErrorCode);
                    global = JsObject.FromIntPtr(globelValueRef);
                }
                return global;
            }
        }

        public void Run(string script)
        {
            JsString scriptString = new JsString(script);
            IntPtr result = IntPtr.Zero;
            JsErrorCode jsErrorCode= NativeMethods.JsRun(scriptString.ValueRef, IntPtr.Zero, scriptString.ValueRef, JsParseScriptAttributes.JsParseScriptAttributeNone, out result);
            JsException.ThrowIfHasError(jsErrorCode);
        }
    }
}
