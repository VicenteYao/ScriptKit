using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptKit
{
    public class JsRuntime : IDisposable
    {
        private JsRuntime(IntPtr runtimeHandle)
        {
            this.runtimeHandle = runtimeHandle;
        }

        private IntPtr runtimeHandle;

        internal IntPtr RuntimeHandle { get { return this.runtimeHandle; } }



        public bool IsEnabled
        {
            get
            {
                bool isDisabled = false;
                JsErrorCode jsErrorCode = NativeMethods.JsIsRuntimeExecutionDisabled(this.runtimeHandle, out isDisabled);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return !isDisabled;
            }
            set
            {
                JsErrorCode jsErrorCode = value ? NativeMethods.JsEnableRuntimeExecution(this.runtimeHandle) : NativeMethods.JsDisableRuntimeExecution(this.runtimeHandle);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
            }
        }

        private JsDebugger jsDebugger;
        public JsDebugger Debugger
        {
            get
            {
                if (this.jsDebugger == null)
                {
                    this.jsDebugger = JsDebugger.CreateDebugger(this);
                }
                return this.jsDebugger;
            }
        }




        public JsContext CurrentContext
        {
            get
            {
                IntPtr currentContext = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetCurrentContext(out currentContext);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return contexts[currentContext];
            }
            set
            {
                JsErrorCode jsErrorCode = NativeMethods.JsSetCurrentContext(value.Value);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
            }

        }

        public static JsRuntime CreateRuntime()
        {
            IntPtr runtimeHandle = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsCreateRuntime(
            JsRuntimeAttributes.JsRuntimeAttributeEnableExperimentalFeatures |
                JsRuntimeAttributes.JsRuntimeAttributeEnableIdleProcessing |
             JsRuntimeAttributes.JsRuntimeAttributeDispatchSetExceptionsToDebugger, null, out runtimeHandle);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return new JsRuntime(runtimeHandle);
        }

        [ThreadStatic]
        private static Dictionary<IntPtr, JsContext> contexts = new Dictionary<IntPtr, JsContext>();

        internal static JsContext GetContextOfObject(JsValue jsValue)
        {
            IntPtr context = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsGetContextOfObject(jsValue.Value, out context);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            JsContext jsContext = null;
            if (contexts.TryGetValue(context, out jsContext))
            {

            }
            return jsContext;
        }

        public JsContext CreateContext()
        {
            IntPtr ctx = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsCreateContext(this.runtimeHandle, out ctx);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            JsContext jsContext = new JsContext(ctx);
            contexts.Add(ctx, jsContext);
            return jsContext;
        }

        public void GarbageCollect()
        {
            JsErrorCode jsErrorCode = NativeMethods.JsCollectGarbage(this.runtimeHandle);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
        }

        public void Dispose()
        {
            JsErrorCode jsErrorCode = NativeMethods.JsDisposeRuntime(this.runtimeHandle);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
        }
    }
}
