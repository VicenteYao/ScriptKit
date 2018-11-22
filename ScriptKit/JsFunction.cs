using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Linq;

namespace ScriptKit
{
    public class JsFunction:JsFunctionBase
    {
        public JsFunction(Func<JsObject, ReadOnlyCollection<JsObject>, JsObject> func)
        {
            this.funcRef = func;
            IntPtr function = IntPtr.Zero;
            this.funcGCHandle = GCHandle.Alloc(func, GCHandleType.Weak);
            JsErrorCode jsErrorCode = NativeMethods.JsCreateFunction(JsFunction.JsNativeFunction, GCHandle.ToIntPtr(this.funcGCHandle), out function);
            JsException.ThrowIfHasError(jsErrorCode);
            this.Value = function; ;
        }

        internal JsFunction(IntPtr value)
        {
            this.Value = value;
        }

        private Delegate funcRef;
        private GCHandle funcGCHandle;

        public unsafe override JsObject Invoke(params JsObject[] arguments)
        {

            IntPtr result = IntPtr.Zero;
            Span<IntPtr> argSpan = arguments.Select(x => x.Value).ToArray();
            fixed(IntPtr* pArg = argSpan)
            {
                JsErrorCode jsErrorCode = NativeMethods.JsCallFunction(this.Value, new IntPtr(pArg), (ushort)arguments.Length, out result);
                JsException.ThrowIfHasError(jsErrorCode);
            }
            return JsObject.FromIntPtr(result);
        }

    }
}
