using System;
using System.Runtime.CompilerServices;

namespace ScriptKit
{
    public class JsRuntimeException:Exception
    {
        public JsRuntimeException(JsErrorCode jsErrorCode)
        {
            this.ErrorCode = jsErrorCode;
        }

        public JsErrorCode ErrorCode { get; private set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void VerifyErrorCode(JsErrorCode jsErrorCode)
        {
            if (jsErrorCode != JsErrorCode.JsNoError)
            {
                throw new JsRuntimeException(jsErrorCode);
            }
        }
    }
}
