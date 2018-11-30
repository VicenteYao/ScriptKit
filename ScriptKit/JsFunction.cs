using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace ScriptKit
{
    public class JsFunction:JsObject
    {
        private readonly IntPtr callbackState;

        public JsFunction(JsFunctionCallback functionCallback) : base(IntPtr.Zero)
        {
            IntPtr function = IntPtr.Zero;
            this.callbackState = GCHandle.ToIntPtr(GCHandle.Alloc(functionCallback, GCHandleType.Weak));
            JsErrorCode jsErrorCode = NativeMethods.JsCreateFunction(jsNativeFunction, this.callbackState, out function);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            this.Value = function;
            NativeMethods.JsSetObjectBeforeCollectCallback(function, this.callbackState, jsObjectBeforeCollectCallback);
        }


        private static JsNativeFunction jsNativeFunction = new JsNativeFunction(NativeFunction);
        private static JsObjectBeforeCollectCallback jsObjectBeforeCollectCallback = new JsObjectBeforeCollectCallback(HandleJsObjectBeforeCollectCallback);

        private static void HandleJsObjectBeforeCollectCallback(IntPtr obj, IntPtr callbackState)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(callbackState);
            gcHandle.Free();
        }


        internal JsFunction(IntPtr value) : base(value)
        {

        }

        private static  unsafe IntPtr NativeFunction(IntPtr calle, bool isConstructCall, IntPtr arguments, ushort argumentCount, IntPtr callbackState)
        {
            GCHandle funcGCHandle = GCHandle.FromIntPtr(callbackState);
            JsFunction objCalle = new JsFunction(calle);
            Span<IntPtr> argumentSpan = new Span<IntPtr>(arguments.ToPointer(), argumentCount);
            IntPtr[] values = argumentSpan.ToArray();
            JsValue[] jsValues = new JsValue[argumentCount-1];
            for (int i = 1; i < argumentCount; i++)
            {
                jsValues[i-1] = JsValue.FromIntPtr(values[i]);
            }
            JsValue self = FromIntPtr(values[0]);
            ReadOnlyCollection<JsValue> args = new ReadOnlyCollection<JsValue>(jsValues);

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
            IntPtr[] argArray = new IntPtr[arguments.Length];
            for (int i = 0; i < arguments.Length; i++)
            {
                argArray[i + 1] = arguments[i].Value;
            }
            fixed (IntPtr* pArgs = argArray)
            {
                JsErrorCode jsErrorCode = NativeMethods.JsConstructObject(this.Value, new IntPtr(pArgs), (ushort)argArray.Length, out result);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
            }
            return FromIntPtr(result);
        }




    }
}
