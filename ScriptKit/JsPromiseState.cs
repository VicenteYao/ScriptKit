using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptKit
{
    public enum JsPromiseState
    {
        JsPromiseStatePending = 0x0,
        JsPromiseStateFulfilled = 0x1,
        JsPromiseStateRejected = 0x2
    }
}
