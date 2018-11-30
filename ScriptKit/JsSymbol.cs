using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptKit
{
    public class JsSymbol : JsValue
    {

        public  JsSymbol(string description){
            IntPtr symbol = IntPtr.Zero;
            JsString jsString = new JsString(description);
            JsErrorCode jsErrorCode= NativeMethods.JsCreateSymbol(jsString.Value, out symbol);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            this.Value = symbol;
        }

        internal JsSymbol(IntPtr value)
        {
            this.Value = value;
        }

        

    }
}
