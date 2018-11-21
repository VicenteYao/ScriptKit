using System;
using System.Runtime.InteropServices;

namespace ScriptKit
{
    internal static class NativeMethods
    {

        private const string lib = "libChakraCore.dylib";

        [DllImport(lib)]
        internal static extern JsErrorCode
JsCreateRuntime(
    JsRuntimeAttributes attributes,
    JsThreadServiceCallback threadService,
    out IntPtr runtime);

        [DllImport(lib)]
        internal static extern JsErrorCode
    JsCreateContext(
         IntPtr runtime,
        out IntPtr newContext);



        [DllImport(lib)]
        internal static extern JsErrorCode
    JsGetCurrentContext(
        out IntPtr currentContext);


        [DllImport(lib)]
        internal static extern JsErrorCode
            JsSetCurrentContext(
                 IntPtr context);


        [DllImport(lib)]
        internal static extern JsErrorCode
        JsCreateFunction(
             JsNativeFunction nativeFunction,
             IntPtr callbackState,
             out IntPtr function);

        [DllImport(lib)]
        internal static extern JsErrorCode

        JsCallFunction(
         IntPtr function,
        IntPtr arguments,
        ushort argumentCount,
        out IntPtr result);


        [DllImport(lib)]
        internal static extern JsErrorCode
        JsConstructObject(
        IntPtr function,
        IntPtr arguments,
        ushort argumentCount,
        out IntPtr result);

        [DllImport(lib)]
        internal static extern JsErrorCode
    JsConvertValueToObject(
        IntPtr value,
        out IntPtr obj);

        [DllImport(lib)]
        internal static extern JsErrorCode
JsRun(
     IntPtr script,
    IntPtr sourceContext,
    IntPtr sourceUrl,
     JsParseScriptAttributes parseAttributes,
    out IntPtr result);


        [DllImport(lib)]
        internal static extern JsErrorCode
    JsCreateStringUtf16(
        IntPtr content,
       IntPtr length,
        out IntPtr value);

        [DllImport(lib)]
        internal static extern JsErrorCode
JsSetProperty(
     IntPtr obj,
     IntPtr propertyId,
     IntPtr value,
     bool useStrictRules);


        [DllImport(lib)]
        internal static extern JsErrorCode
        JsCreatePropertyId(
    IntPtr name,
   IntPtr length,
   out IntPtr propertyId);

        [DllImport(lib)]
        internal static extern JsErrorCode
JsGetProperty(
    IntPtr obj,
    IntPtr propertyId,
    out IntPtr value);

        [DllImport(lib)]
        internal static extern JsErrorCode
JsGetGlobalObject(
    out IntPtr globalObject);

        [DllImport(lib)]
        internal static extern JsErrorCode
JsGetValueType(
    IntPtr value,
    out  JsValueType type);
    }
}
