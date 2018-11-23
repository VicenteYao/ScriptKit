using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
            JsErrorCode jsErrorCode = NativeMethods.JsDiagStartDebugging(this.jsRuntime.RuntimeHandle, OnDebugEvent, IntPtr.Zero);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
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


        public JsBreakpoint[] Breakpoints{
            get{
                IntPtr breakpoints = IntPtr.Zero;
                JsErrorCode jsErrorCode = NativeMethods.JsDiagGetBreakpoints(out breakpoints);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                JsArray breakpointsArray = JsValue.FromIntPtr(breakpoints) as JsArray;
                JsBreakpoint[] jsBreakpoints = new JsBreakpoint[breakpointsArray.Length];
                for (int i = 0; i < breakpointsArray.Length; i++)
                {
                    JsObject breakpoint = breakpointsArray[i] as JsObject;
                    uint breakpointId = (uint)breakpoint["breakpointId"].ConvertToJsNumber().ToInt32();
                    uint scriptId = (uint)breakpoint["scriptId"].ConvertToJsNumber().ToInt32();
                    uint line = (uint)breakpoint["line"].ConvertToJsNumber().ToInt32();
                    uint column = (uint)breakpoint["column"].ConvertToJsNumber().ToInt32();
                    jsBreakpoints[i] = new JsBreakpoint(scriptId, column, line);
                    jsBreakpoints[i].BreakPointId = breakpointId;
                }
                return jsBreakpoints;
            }
        }

        public JsBreakpoint SetBreakpoint(uint scriptId, uint lineNumber, uint columnNumber)
        {
            IntPtr breakpoint = IntPtr.Zero;
            JsErrorCode jsErrorCode = NativeMethods.JsDiagSetBreakpoint(scriptId, lineNumber, columnNumber, out breakpoint);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            JsObject breakpointObject = JsValue.FromIntPtr(breakpoint) as JsObject;
            uint breakpointId = (uint)breakpointObject["breakpointId"].ConvertToJsNumber().ToInt32();
            uint line = (uint)breakpointObject["line"].ConvertToJsNumber().ToInt32();
            uint column = (uint)breakpointObject["column"].ConvertToJsNumber().ToInt32();
            return new JsBreakpoint(scriptId, line, column)
            {
                BreakPointId = breakpointId,
            };

        }

        public void RemoveBreakpoint(JsBreakpoint breakpoint)
        {
            JsErrorCode jsErrorCode = NativeMethods.JsDiagRemoveBreakpoint(breakpoint.BreakPointId);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
        }


        public JsScript[] Scripts { get; private set; }





        public JsDiagBreakOnExceptionAttributes BreakOnExceptionAttribute
        {
            get
            {
                JsDiagBreakOnExceptionAttributes jsDiagBreakOnExceptionAttributes = JsDiagBreakOnExceptionAttributes.JsDiagBreakOnExceptionAttributeNone;
                JsErrorCode jsErrorCode = NativeMethods.JsDiagGetBreakOnException(this.jsRuntime.RuntimeHandle, out jsDiagBreakOnExceptionAttributes);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
                return jsDiagBreakOnExceptionAttributes;

            }
            set
            {

                JsErrorCode jsErrorCode = NativeMethods.JsDiagSetBreakOnException(this.jsRuntime.RuntimeHandle, value);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
            }
        }


        public void SetStepType(JsDiagStepType jsDiagStepType)
        {
            JsErrorCode jsErrorCode = NativeMethods.JsDiagSetStepType(jsDiagStepType);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
        }





    }
}
