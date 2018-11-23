using System;
namespace ScriptKit
{
    public class JsContext:JsRuntimeObject
    {
        internal JsContext(IntPtr ctx)
        {
            this.Value = ctx;
        }

        public JsObject Global {
            get
            {
                IntPtr globelValueRef = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetGlobalObject(out globelValueRef);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
                return new JsObject(globelValueRef);
            }
        }

        public JsValue Null
        {
            get
            {
                IntPtr nullValue = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetNullValue(out nullValue);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
                return JsValue.FromIntPtr(nullValue);
            }
        }

        public JsBoolean True
        {
            get
            {
                IntPtr trueValue = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetTrueValue(out trueValue);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
                return new JsBoolean(trueValue);
            }
        }

        public JsBoolean False
        {
            get
            {
                IntPtr falseValue = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetFalseValue(out falseValue);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
                return new JsBoolean(falseValue);
            }
        }

        public JsValue Undefined
        {
            get
            {
                IntPtr undefinedValue = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetUndefinedValue(out undefinedValue);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
                return JsValue.FromIntPtr (undefinedValue);
            }
        }

        private static uint sourceContext;

        public void Run(string script)
        {
            sourceContext++;
            JsString scriptString = new JsString(script);
            JsString sourceUrl = new JsString("");
            IntPtr result = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsRun(scriptString.Value, new IntPtr(sourceContext), sourceUrl.Value, JsParseScriptAttributes.JsParseScriptAttributeNone, out result);
            JsRuntimeException.ThrowIfHasError(jsErrorCode);
        }

        public void Run(JsExternalArrayBuffer jsExternalArrayBuffer)
        {
            sourceContext++;
            IntPtr result = IntPtr.Zero;
            JsString sourceUrl = new JsString("");
            JsErrorCode jsErrorCode = NativeMethods.JsRun(jsExternalArrayBuffer.Value, new IntPtr(sourceContext), sourceUrl.Value, JsParseScriptAttributes.JsParseScriptAttributeArrayBufferIsUtf16Encoded, out result);
            JsRuntimeException.ThrowIfHasError(jsErrorCode);
        }
    }
}
