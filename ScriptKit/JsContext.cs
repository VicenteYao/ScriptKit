using System;
using System.IO;

namespace ScriptKit
{
    public class JsContext:JsRuntimeObject
    {
        internal JsContext(IntPtr ctx)
        {
            this.Value = ctx;
        }

        private static JsContext invalid;
        public static JsContext Invalid
        {
            get
            {

                if (invalid == null)
                {
                    invalid = new JsContext(IntPtr.Zero);
                }
                return invalid;
            }
        }

        public JsObject Global {
            get
            {
                IntPtr globelValueRef = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetGlobalObject(out globelValueRef);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return new JsObject(globelValueRef);
            }
        }

        public JsValue Null
        {
            get
            {
                IntPtr nullValue = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetNullValue(out nullValue);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return JsValue.FromIntPtr(nullValue);
            }
        }

        public JsBoolean True
        {
            get
            {
                IntPtr trueValue = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetTrueValue(out trueValue);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return new JsBoolean(trueValue);
            }
        }

        public JsBoolean False
        {
            get
            {
                IntPtr falseValue = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetFalseValue(out falseValue);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return new JsBoolean(falseValue);
            }
        }

        public JsValue Undefined
        {
            get
            {
                IntPtr undefinedValue = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetUndefinedValue(out undefinedValue);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return JsValue.FromIntPtr (undefinedValue);
            }
        }

        private static uint sourceContext;

        public JsValue Run(string scriptOrFilePath,bool isFile=false)
        {
            string source = null;
            sourceContext++;
            if (isFile)
            {
                if (File.Exists(scriptOrFilePath))
                {
                    source = File.ReadAllText(scriptOrFilePath);
                }
                else
                {
                    throw new FileNotFoundException(scriptOrFilePath);
                }
            }else
            {
                source = scriptOrFilePath;
            }
            JsString scriptString  = new JsString(scriptOrFilePath);
            JsString sourceUrl = new JsString("");
            IntPtr result = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsRun(scriptString.Value, new IntPtr(sourceContext), sourceUrl.Value, JsParseScriptAttributes.JsParseScriptAttributeNone, out result);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return JsValue.FromIntPtr(result);
        }


        public JsValue Run(JsArrayBuffer jsExternalArrayBuffer)
        {
            sourceContext++;
            IntPtr result = IntPtr.Zero;
            JsString sourceUrl = new JsString("");
            JsErrorCode jsErrorCode = NativeMethods.JsRun(jsExternalArrayBuffer.Value, new IntPtr(sourceContext), sourceUrl.Value, JsParseScriptAttributes.JsParseScriptAttributeArrayBufferIsUtf16Encoded, out result);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return JsValue.FromIntPtr(result);
        }

        public JsFunction Parse(string scriptOrFilePath, bool isFile = false)
        {
            string source = null;
            sourceContext++;
            if (isFile)
            {
                if (File.Exists(scriptOrFilePath))
                {
                    source = File.ReadAllText(scriptOrFilePath);
                }
                else
                {
                    throw new FileNotFoundException(scriptOrFilePath);
                }
            }
            else
            {
                source = scriptOrFilePath;
            }
            JsString scriptString = new JsString(scriptOrFilePath);
            JsString sourceUrl = new JsString("");
            IntPtr result = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsParse(scriptString.Value, new IntPtr(sourceContext), sourceUrl.Value, JsParseScriptAttributes.JsParseScriptAttributeNone, out result);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return JsValue.FromIntPtr(result) as JsFunction;
        }


    }
}
