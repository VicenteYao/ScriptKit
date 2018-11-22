using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptKit
{
    public class JsRuntime
    {
        private JsRuntime(IntPtr runtime)
        {
            this.runtime = runtime;
        }

        private IntPtr runtime;



        public JsContext CurrentContext
        {
            get
            {
                IntPtr currentContext = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsGetCurrentContext(out currentContext);
                JsException.ThrowIfHasError(jsErrorCode);
                return contexts[currentContext];
            }
            set
            {
                NativeMethods.JsSetCurrentContext(value.Value);
            }

        }

        public static JsRuntime CreateRuntime()
        {
            IntPtr runtime = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsCreateRuntime(
            JsRuntimeAttributes.JsRuntimeAttributeEnableExperimentalFeatures |
                JsRuntimeAttributes.JsRuntimeAttributeEnableIdleProcessing |
             JsRuntimeAttributes.JsRuntimeAttributeDispatchSetExceptionsToDebugger, null, out runtime);
            JsException.ThrowIfHasError(jsErrorCode);
            return new JsRuntime(runtime);
        }

        [ThreadStatic]
        private static Dictionary<IntPtr, JsContext> contexts = new Dictionary<IntPtr, JsContext>();

        internal static JsContext GetContextOfObject(JsValue jsValue)
        {
            IntPtr context = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsGetContextOfObject(jsValue.Value, out context);
            JsException.ThrowIfHasError(jsErrorCode);
            JsContext jsContext = null;
            if (contexts.TryGetValue(context, out jsContext))
            {

            }
            return jsContext;
        }

        public JsContext CreateContext()
        {
            IntPtr ctx = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsCreateContext(this.runtime, out ctx);
            JsException.ThrowIfHasError(jsErrorCode);
            JsContext jsContext= new JsContext(ctx);
            contexts.Add(ctx, jsContext);
            return jsContext;
        }

        public void GarbageCollect()
        {
            JsErrorCode jsErrorCode = NativeMethods.JsCollectGarbage(this.runtime);
            JsException.ThrowIfHasError(jsErrorCode);
        }
    }
}
