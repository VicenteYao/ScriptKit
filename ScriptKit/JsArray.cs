﻿using System;
using System.Collections;

namespace ScriptKit
{
    public class JsArray : JsObject
    {
        public JsArray(params JsObject[] initValues)
        {
            IntPtr array = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsCreateArray((uint)initValues.Length, out array);
            JsException.ThrowIfHasError(jsErrorCode);
            if (initValues != null)
            {
                for (int i = 0; i < initValues.Length; i++)
                {
                    this[i] = initValues[i];
                }
            }
        }

        internal JsArray(IntPtr value)
        {
            this.Value = value;
        }



    }
}
