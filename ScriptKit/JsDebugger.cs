using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptKit
{
    public  class JsDebugger
    {
        private JsDebugger(JsRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        private JsRuntime jsRuntime;

       internal static JsDebugger CreateDebugger(JsRuntime jsRuntime)
        {
            return new JsDebugger(jsRuntime);
        }

        public void Start()
        {
            JsErrorCode jsErrorCode=  NativeMethods.JsDiagStartDebugging(this.jsRuntime.RuntimeHandle, OnDebugEvent, IntPtr.Zero);
            JsRuntimeException.ThrowIfHasError(jsErrorCode);
        }

        private void OnDebugEvent(JsDiagDebugEvent debugEvent, IntPtr eventData, IntPtr callbackState)
        {
            switch (debugEvent)
            {
                case JsDiagDebugEvent.JsDiagDebugEventSourceCompile:
                    break;
                case JsDiagDebugEvent.JsDiagDebugEventCompileError:
                    break;
                case JsDiagDebugEvent.JsDiagDebugEventBreakpoint:
                    break;
                case JsDiagDebugEvent.JsDiagDebugEventStepComplete:
                    break;
                case JsDiagDebugEvent.JsDiagDebugEventDebuggerStatement:
                    break;
                case JsDiagDebugEvent.JsDiagDebugEventAsyncBreak:
                    break;
                case JsDiagDebugEvent.JsDiagDebugEventRuntimeException:
                    break;
                default:
                    break;
            }
        }

        public void Stop()
        {
            NativeMethods.JsDiagStopDebugging(this.jsRuntime.RuntimeHandle, IntPtr.Zero);
        }



    }
}
