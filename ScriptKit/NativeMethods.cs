using            System;
using            System.Runtime.InteropServices;

namespace ScriptKit
{
    internal static class NativeMethods
    {
        private const string lib = "ChakraCore";

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsCreateRuntime(
            JsRuntimeAttributes attributes,
            JsThreadServiceCallback threadService,
            out IntPtr runtime);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsDisposeRuntime(
            IntPtr runtime);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsCollectGarbage(IntPtr runtime);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsGetRuntimeMemoryUsage(
    IntPtr runtime,
            out IntPtr memoryUsage);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsSetRuntimeMemoryLimit(
            IntPtr runtime,
            IntPtr memoryLimit);

        [DllImport(lib)]
        internal static extern JsErrorCode
    JsGetRuntimeMemoryLimit(
            IntPtr runtime,
            out IntPtr memoryLimit);



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
            out JsValueType type);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsGetPrototype(
            IntPtr obj,
            out IntPtr prototypeObject);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsSetPrototype(
            IntPtr obj,
            IntPtr prototypeObject);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsGetOwnPropertyNames(
        IntPtr obj,
        out IntPtr propertyNames);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsGetUndefinedValue(out IntPtr undefinedValue);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsGetNullValue(out IntPtr nullValue);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsGetTrueValue(out IntPtr trueValue);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsGetFalseValue(out IntPtr falseValue);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsGetContextOfObject(
            IntPtr obj,
            out IntPtr context);


        [DllImport(lib)]
        internal static extern JsErrorCode
        JsBoolToBoolean(
            bool value,
            out IntPtr booleanValue);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsBooleanToBool(
            IntPtr value,
            out bool boolValue);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsConvertValueToBoolean(
            IntPtr value,
            out IntPtr booleanValue);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsDoubleToNumber(
            double doubleValue,
            out IntPtr value);


        [DllImport(lib)]
        internal static extern JsErrorCode
        JsIntToNumber(
            int intValue,
            out IntPtr value);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsNumberToDouble(
            IntPtr value,
            out double doubleValue);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsNumberToInt(
            IntPtr value,
            out int intValue);


        [DllImport(lib)]
        internal static extern JsErrorCode
        JsConvertValueToNumber(
            IntPtr value,
            out IntPtr numberValue);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsGetStringLength(
            IntPtr stringValue,
            out int length);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsConvertValueToString(
            IntPtr value,
            out IntPtr stringValue);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsCopyStringUtf16(
            IntPtr value,
            int start,
            int length,
            IntPtr buffer,
            out IntPtr written);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsEquals(
            IntPtr object1,
            IntPtr object2,
            out bool result);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsStrictEquals(
            IntPtr object1,
            IntPtr object2,
            out bool result);

        [DllImport(lib)]
        internal static extern JsErrorCode
       JsCreateTypedArray(
           JsTypedArrayType arrayType,
           IntPtr baseArray,
           uint byteOffset,
           uint elementLength,
           out IntPtr result);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsGetIndexedProperty(
            IntPtr obj,
            IntPtr index,
            out IntPtr result);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsSetIndexedProperty(
           IntPtr obj,
           IntPtr index,
           IntPtr result);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsDeleteIndexedProperty(
            IntPtr obj,
            IntPtr index);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsDeleteProperty(
            IntPtr obj,
            IntPtr propertyId,
            bool useStrictRules,
            out bool result);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsDefineProperty(
             IntPtr obj,
             IntPtr propertyId,
             IntPtr propertyDescriptor,
             out bool result);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsCreateArray(
            uint length,
            out IntPtr result);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsCreateObject(out IntPtr obj);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsInstanceOf(
            IntPtr obj,
            IntPtr constructor,
            out bool result);
        [DllImport(lib)]
        internal static extern JsErrorCode
        JsGetOwnPropertyDescriptor(
            IntPtr obj,
            IntPtr propertyId,
            out IntPtr propertyDescriptor);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsAddRef(
            IntPtr obj,
            out uint count);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsRelease(
        IntPtr obj,
        out uint count);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsCreateWeakReference(
        IntPtr value,
        out IntPtr weakRef);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsGetWeakReferenceValue(
        IntPtr weakRef,
        out IntPtr value);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsCreateExternalArrayBuffer(
        IntPtr data,
        uint byteLength,
        JsFinalizeCallback finalizeCallback,
        IntPtr callbackState,
        out IntPtr result);

        [DllImport(lib)]
        internal static extern JsErrorCode
        JsCreateArrayBuffer(
        uint byteLength,
        out IntPtr result);


        [DllImport(lib)]
        internal static extern JsErrorCode
            JsHasException(
                out bool hasException);


        [DllImport(lib)]
        internal static extern JsErrorCode
            JsGetAndClearException(
                out IntPtr exception);


        [DllImport(lib)]
        internal static extern JsErrorCode
            JsSetException(
                IntPtr exception);


        [DllImport(lib)]
        internal static extern JsErrorCode
            JsDisableRuntimeExecution(
                IntPtr runtime);

        [DllImport(lib)]
        internal static extern JsErrorCode
    JsEnableRuntimeExecution(
                IntPtr runtime);

        [DllImport(lib)]
        internal static extern JsErrorCode
    JsIsRuntimeExecutionDisabled(
        IntPtr runtime,
        out bool isDisabled);

        [DllImport(lib)]
        internal static extern JsErrorCode
            JsGetPromiseState(
                 IntPtr promise,
                out JsPromiseState state);


        [DllImport(lib)]
        internal static extern JsErrorCode
            JsGetPromiseResult(
                IntPtr promise,
                out IntPtr result);

        [DllImport(lib)]
        internal static extern JsErrorCode
            JsCreatePromise(
                out IntPtr promise,
                    out IntPtr PtrresolveFunction,
                   out IntPtr PtrrejectFunction);

        [DllImport(lib)]
        internal static extern JsErrorCode
    JsDiagStartDebugging(
        IntPtr runtimeHandle,
         JsDiagDebugEventCallback debugEventCallback,
        IntPtr callbackState);


        [DllImport(lib)]
        internal static extern JsErrorCode
            JsDiagStopDebugging(
                IntPtr runtimeHandle,
                IntPtr callbackState);



        [DllImport(lib)]
        internal static extern JsErrorCode
            JsDiagRequestAsyncBreak(
                IntPtr runtimeHandle);
    }
}
