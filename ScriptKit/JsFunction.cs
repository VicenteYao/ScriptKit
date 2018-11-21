using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Linq;

namespace ScriptKit
{
    public class JsFunction:JsObject
    {
        public JsFunction(Func<JsObject, ReadOnlyCollection<JsObject>, JsObject> func)
        {
            this.funcRef = func;
            this.methodHandle = GCHandle.Alloc(func, GCHandleType.Weak);
            JsErrorCode jsErrorCode = NativeMethods.JsCreateFunction(JsFunction.JsNativeFunction, GCHandle.ToIntPtr( this.methodHandle), out this.function);
            JsException.ThrowIfHasError(jsErrorCode);
            this.ValueRef = this.function;
        }

        private Delegate funcRef;
        private IntPtr function;
        private GCHandle methodHandle;




        static unsafe IntPtr JsNativeFunction(IntPtr calle, bool isConstructCall, IntPtr arguments, ushort argumentCount, IntPtr callbackState)
        {
            GCHandle methodGCHandle = GCHandle.FromIntPtr(callbackState);
            if (methodGCHandle.Target is Func<JsObject, ReadOnlyCollection<JsObject>, JsObject> func)
            {
                JsObject objCalle = JsObject.FromIntPtr(calle);
                Span<IntPtr> argumentSpan = new Span<IntPtr>((void*)arguments, argumentCount);
                ReadOnlyCollection<JsObject> args =
                 new ReadOnlyCollection<JsObject>(argumentSpan.ToArray().Select(p => JsObject.FromIntPtr(p)).ToArray());
                JsObject result = func(objCalle, args);
                return result.ValueRef;
            }
            return IntPtr.Zero;
        }



        public unsafe virtual JsObject Invoke(params JsObject[] arguments)
        {

            IntPtr result = IntPtr.Zero;
            IntPtr pArgs = Marshal.AllocHGlobal(IntPtr.Size * arguments.Length);
            JsErrorCode jsErrorCode = NativeMethods.JsCallFunction(this.function, pArgs, (ushort)arguments.Length, out result);
            JsException.ThrowIfHasError(jsErrorCode);

            return JsObject.FromIntPtr(result);
        }


    }
}
