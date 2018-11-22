using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace ScriptKit
{
    public class JsConstructor:JsFunctionBase
    {
        public JsConstructor(Func<JsObject, ReadOnlyCollection<JsObject>, JsObject> func)
        {
            this.funcRef = func;
            IntPtr function = IntPtr.Zero;
            this.funcGCHandle = GCHandle.Alloc(func, GCHandleType.Weak);
            JsErrorCode jsErrorCode = NativeMethods.JsCreateFunction(JsFunction.JsNativeFunction, GCHandle.ToIntPtr(this.funcGCHandle), out function);
            JsException.ThrowIfHasError(jsErrorCode);
            this.Value = function; ;
        }

        private Delegate funcRef;
        private GCHandle funcGCHandle;

        public override JsObject Invoke(params JsObject[] arguments)
        {
            IntPtr result = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsConstructObject(this.Value, IntPtr.Zero, (ushort)arguments.Length, out result);
            JsException.ThrowIfHasError(jsErrorCode);
            return JsObject.FromIntPtr(result);
        }

    }
}
