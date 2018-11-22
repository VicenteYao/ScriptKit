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
                return contexts.FirstOrDefault(x => x.Value == currentContext).Key;
            }
            set
            {
                IntPtr ctx = IntPtr.Zero;
                if (contexts.TryGetValue(value, out ctx))
                {
                    NativeMethods.JsSetCurrentContext(ctx);
                }
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
        private static Dictionary<JsContext, IntPtr> contexts = new Dictionary<JsContext, IntPtr>();

        internal static JsContext GetContextOfObject(JsObject jsObject)
        {
            IntPtr context = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsGetContextOfObject(jsObject.Value, out context);
            JsException.ThrowIfHasError(jsErrorCode);
            JsContext jsContext = contexts.FirstOrDefault(x => x.Value == context).Key;
            return jsContext;
        }

        public JsContext CreateContext()
        {
            IntPtr ctx = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsCreateContext(this.runtime, out ctx);
            JsException.ThrowIfHasError(jsErrorCode);
            JsContext jsContext= new JsContext(ctx);
            contexts.Add(jsContext, ctx);
            return jsContext;
        }
    }
}
