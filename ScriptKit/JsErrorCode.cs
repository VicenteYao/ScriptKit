using System;
namespace ScriptKit
{
    public enum JsErrorCode
    {
        /// <summary>
        ///     Success error code.
        /// </summary>
        JsNoError = 0,

        /// <summary>
        ///     Category of errors that relates to incorrect usage of the API itself.
        /// </summary>
        JsErrorCategoryUsage = 0x10000,
        /// <summary>
        ///     An argument to a hosting API was invalid.
        /// </summary>
        JsErrorInvalidArgument,
        /// <summary>
        ///     An argument to a hosting API was null in a context where null is not allowed.
        /// </summary>
        JsErrorNullArgument,
        /// <summary>
        ///     The hosting API requires that a context be current, but there is no current context.
        /// </summary>
        JsErrorNoCurrentContext,
        /// <summary>
        ///     The engine is in an exception state and no APIs can be called until the exception is
        ///     cleared.
        /// </summary>
        JsErrorInExceptionState,
        /// <summary>
        ///     A hosting API is not yet implemented.
        /// </summary>
        JsErrorNotImplemented,
        /// <summary>
        ///     A hosting API was called on the wrong thread.
        /// </summary>
        JsErrorWrongThread,
        /// <summary>
        ///     A runtime that is still in use cannot be disposed.
        /// </summary>
        JsErrorRuntimeInUse,
        /// <summary>
        ///     A bad serialized script was used, or the serialized script was serialized by a
        ///     different version of the Chakra engine.
        /// </summary>
        JsErrorBadSerializedScript,
        /// <summary>
        ///     The runtime is in a disabled state.
        /// </summary>
        JsErrorInDisabledState,
        /// <summary>
        ///     Runtime does not support reliable script interruption.
        /// </summary>
        JsErrorCannotDisableExecution,
        /// <summary>
        ///     A heap enumeration is currently underway in the script context.
        /// </summary>
        JsErrorHeapEnumInProgress,
        /// <summary>
        ///     A hosting API that operates on object values was called with a non-object value.
        /// </summary>
        JsErrorArgumentNotObject,
        /// <summary>
        ///     A script context is in the middle of a profile callback.
        /// </summary>
        JsErrorInProfileCallback,
        /// <summary>
        ///     A thread service callback is currently underway.
        /// </summary>
        JsErrorInThreadServiceCallback,
        /// <summary>
        ///     Scripts cannot be serialized in debug contexts.
        /// </summary>
        JsErrorCannotSerializeDebugScript,
        /// <summary>
        ///     The context cannot be put into a debug state because it is already in a debug state.
        /// </summary>
        JsErrorAlreadyDebuggingContext,
        /// <summary>
        ///     The context cannot start profiling because it is already profiling.
        /// </summary>
        JsErrorAlreadyProfilingContext,
        /// <summary>
        ///     Idle notification given when the host did not enable idle processing.
        /// </summary>
        JsErrorIdleNotEnabled,
        /// <summary>
        ///     The context did not accept the enqueue callback.
        /// </summary>
        JsCannotSetProjectionEnqueueCallback,
        /// <summary>
        ///     Failed to start projection.
        /// </summary>
        JsErrorCannotStartProjection,
        /// <summary>
        ///     The operation is not supported in an object before collect callback.
        /// </summary>
        JsErrorInObjectBeforeCollectCallback,
        /// <summary>
        ///     Object cannot be unwrapped to IInspectable pointer.
        /// </summary>
        JsErrorObjectNotInspectable,
        /// <summary>
        ///     A hosting API that operates on symbol property ids but was called with a non-symbol property id.
        ///     The error code is returned by JsGetSymbolFromPropertyId if the function is called with non-symbol property id.
        /// </summary>
        JsErrorPropertyNotSymbol,
        /// <summary>
        ///     A hosting API that operates on string property ids but was called with a non-string property id.
        ///     The error code is returned by existing JsGetPropertyNamefromId if the function is called with non-string property id.
        /// </summary>
        JsErrorPropertyNotString,
        /// <summary>
        ///     Module evaluation is called in wrong context.
        /// </summary>
        JsErrorInvalidContext,
        /// <summary>
        ///     Module evaluation is called in wrong context.
        /// </summary>
        JsInvalidModuleHostInfoKind,
        /// <summary>
        ///     Module was parsed already when JsParseModuleSource is called.
        /// </summary>
        JsErrorModuleParsed,
        /// <summary>
        ///     Argument passed to JsCreateWeakReference is a primitive that is not managed by the GC.
        ///     No weak reference is required, the value will never be collected.
        /// </summary>
        JsNoWeakRefRequired,
        /// <summary>
        ///     The <c>Promise</c> object is still in the pending state.
        /// </summary>
        JsErrorPromisePending,
        /// <summary>
        ///     Category of errors that relates to errors occurring within the engine itself.
        /// </summary>
        JsErrorCategoryEngine = 0x20000,
        /// <summary>
        ///     The Chakra engine has run out of memory.
        /// </summary>
        JsErrorOutOfMemory,
        /// <summary>
        ///     The Chakra engine failed to set the Floating Point Unit state.
        /// </summary>
        JsErrorBadFPUState,

        /// <summary>
        ///     Category of errors that relates to errors in a script.
        /// </summary>
        JsErrorCategoryScript = 0x30000,
        /// <summary>
        ///     A JavaScript exception occurred while running a script.
        /// </summary>
        JsErrorScriptException,
        /// <summary>
        ///     JavaScript failed to compile.
        /// </summary>
        JsErrorScriptCompile,
        /// <summary>
        ///     A script was terminated due to a request to suspend a runtime.
        /// </summary>
        JsErrorScriptTerminated,
        /// <summary>
        ///     A script was terminated because it tried to use <c>eval</c> or <c>function</c> and eval
        ///     was disabled.
        /// </summary>
        JsErrorScriptEvalDisabled,

        /// <summary>
        ///     Category of errors that are fatal and signify failure of the engine.
        /// </summary>
        JsErrorCategoryFatal = 0x40000,
        /// <summary>
        ///     A fatal error in the engine has occurred.
        /// </summary>
        JsErrorFatal,
        /// <summary>
        ///     A hosting API was called with object created on different javascript runtime.
        /// </summary>
        JsErrorWrongRuntime,

        /// <summary>
        ///     Category of errors that are related to failures during diagnostic operations.
        /// </summary>
        JsErrorCategoryDiagError = 0x50000,
        /// <summary>
        ///     The object for which the debugging API was called was not found
        /// </summary>
        JsErrorDiagAlreadyInDebugMode,
        /// <summary>
        ///     The debugging API can only be called when VM is in debug mode
        /// </summary>
        JsErrorDiagNotInDebugMode,
        /// <summary>
        ///     The debugging API can only be called when VM is at a break
        /// </summary>
        JsErrorDiagNotAtBreak,
        /// <summary>
        ///     Debugging API was called with an invalid handle.
        /// </summary>
        JsErrorDiagInvalidHandle,
        /// <summary>
        ///     The object for which the debugging API was called was not found
        /// </summary>
        JsErrorDiagObjectNotFound,
        /// <summary>
        ///     VM was unable to perform the request action
        /// </summary>
        JsErrorDiagUnableToPerformAction,
    }
 
}
