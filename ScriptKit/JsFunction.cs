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
            this.functionCallback = functionCallback;
            IntPtr function = IntPtr.Zero;
            this.functionCallbackGCHandle = GCHandle.Alloc(functionCallback, GCHandleType.Weak);
            JsErrorCode jsErrorCode = NativeMethods.JsCreateFunction(JsNativeFunction, GCHandle.ToIntPtr(this.functionCallbackGCHandle), out function);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            this.Value = function;
        }

        internal JsFunction(IntPtr value) : base(value)
        {

        }

        private Delegate functionCallback;
        private GCHandle functionCallbackGCHandle;


        private static unsafe IntPtr JsNativeFunction(IntPtr calle, bool isConstructCall, IntPtr arguments, ushort argumentCount, IntPtr callbackState)
        {
            GCHandle funcGCHandle = GCHandle.FromIntPtr(callbackState);
            JsFunction objCalle = new JsFunction(calle);
            Span<IntPtr> argumentSpan = new Span<IntPtr>(arguments.ToPointer(), argumentCount);
            IntPtr[] values = argumentSpan.ToArray();
            ReadOnlyCollection<JsValue> args =
                new ReadOnlyCollection<JsValue>(values.Skip(1).Select(p => JsValue.FromIntPtr(p)).ToArray());
            JsValue self = FromIntPtr(values[0]);
            JsFunctionCallback functionCallback = funcGCHandle.Target as JsFunctionCallback;
            JsValue result = functionCallback(objCalle, self, args);
            if (result == null)
            {
                return objCalle.Context.Null.Value;
            }
            return result.Value;
        }


        public unsafe JsValue Call(JsValue thisArgs,params JsValue[] arguments)
        {

            IntPtr result = IntPtr.Zero;
            IntPtr[] argArray = new IntPtr[1 + arguments.Length];
            argArray[0] = thisArgs.Value;
            for (int i = 0; i < arguments.Length; i++)
            {
                argArray[i + 1] = arguments[i].Value;
            }
            fixed (IntPtr* pArgs = argArray)
            {
                JsErrorCode jsErrorCode = NativeMethods.JsCallFunction(this.Value, new IntPtr(pArgs), (ushort)argArray.Length, out result);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
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
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
            }
            return FromIntPtr(result);
        }




    }
}
