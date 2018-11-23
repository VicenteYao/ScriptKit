using System;
using System.Collections;

namespace ScriptKit
{
    public class JsArray : JsObject
    {
        public JsArray(params JsValue[] initValues):base(IntPtr.Zero)
        {
            IntPtr array = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsCreateArray((uint)initValues.Length, out array);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            this.Value = array;
            for (int i = 0; i < initValues.Length; i++)
            {
                this[i] = initValues[i];
            }

        }

        internal JsArray(IntPtr value) : base(value)
        {

        }


        public int Length
        {

            get
            {
                return (this["length"] as JsNumber).ToInt32();
            }
        }



    }
}
