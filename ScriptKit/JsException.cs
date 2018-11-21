using System;
namespace ScriptKit
{
    public class JsException:Exception
    {
        public JsException(JsErrorCode jsErrorCode)
        {
            this.ErrorCode = jsErrorCode;
        }

        public JsErrorCode ErrorCode { get; set; }

        internal static void ThrowIfHasError(JsErrorCode jsErrorCode)
        {
            if (jsErrorCode != JsErrorCode.JsNoError)
            {
                throw new JsException(jsErrorCode);
            }
        }
    }
}
