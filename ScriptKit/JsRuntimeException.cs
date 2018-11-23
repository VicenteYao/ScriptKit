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

        public JsErrorCode ErrorCode { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void ThrowIfHasError(JsErrorCode jsErrorCode)
        {
            if (jsErrorCode != JsErrorCode.JsNoError)
            {
                throw new JsRuntimeException(jsErrorCode);
            }
        }
    }
}
