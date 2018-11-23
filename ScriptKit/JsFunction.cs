using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Linq;

namespace ScriptKit
{
    public class JsFunction:JsObject
    {
        public JsFunction(JsFunctionCallback functionCallback) : base(IntPtr.Zero)
        {
            this.funcRef = functionCallback;
            IntPtr function = IntPtr.Zero;
            this.funcGCHandle = GCHandle.Alloc(functionCallback, GCHandleType.Weak);
            JsErrorCode jsErrorCode = NativeMethods.JsCreateFunction(JsNativeFunction, GCHandle.ToIntPtr(this.funcGCHandle), out function);
            JsRuntimeException.ThrowIfHasError(jsErrorCode);
            this.Value = function;
        }

        internal JsFunction(IntPtr value) : base(IntPtr.Zero)
        {
            this.Value = value;
        }

        private Delegate funcRef;
        private GCHandle funcGCHandle;


        private static unsafe IntPtr JsNativeFunction(IntPtr calle, bool isConstructCall, IntPtr arguments, ushort argumentCount, IntPtr callbackState)
        {
            GCHandle funcGCHandle = GCHandle.FromIntPtr(callbackState);
            if (funcGCHandle.Target is JsFunctionCallback functionCallback)
            {
                JsFunction objCalle = new JsFunction(calle);
                Span<IntPtr> argumentSpan = new Span<IntPtr>(arguments.ToPointer(), argumentCount);
                IntPtr[] values = argumentSpan.ToArray();
                ReadOnlyCollection<JsValue> args =
                    new ReadOnlyCollection<JsValue>(argumentSpan.ToArray().Skip(1).Select(p => JsValue.FromIntPtr(p)).ToArray());
                JsValue self =FromIntPtr(values[0]);
                JsValue result = functionCallback(objCalle, self, args);
                if (result == null)
                {
                    return objCalle.Context.Null.Value;
                }
                return result.Value;
            }
            return IntPtr.Zero;
        }


        public unsafe JsValue Invoke(params JsValue[] arguments)
        {

            IntPtr result = IntPtr.Zero;
            Span<IntPtr> argSpan = arguments.Select(x => x.Value).ToArray();
            fixed(IntPtr* pArg = argSpan)
            {
                JsErrorCode jsErrorCode = NativeMethods.JsCallFunction(this.Value, new IntPtr(pArg), (ushort)arguments.Length, out result);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
            }
            return FromIntPtr(result);
        }

        public unsafe JsValue Construct(params JsValue[] arguments)
        {

            IntPtr result = IntPtr.Zero;
            Span<IntPtr> argSpan = arguments.Select(x => x.Value).ToArray();
            fixed (IntPtr* pArg = argSpan)
            {
                JsErrorCode jsErrorCode = NativeMethods.JsConstructObject(this.Value, new IntPtr(pArg), (ushort)arguments.Length, out result);
                JsRuntimeException.ThrowIfHasError(jsErrorCode);
            }
            return FromIntPtr(result);
        }

    }
}
