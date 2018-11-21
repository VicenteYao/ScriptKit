using System;
namespace ScriptKit
{
    /// <summary>
    ///     The JavaScript type of a JsValueRef.
    /// </summary>
    public enum JsValueType
    {
        /// <summary>
        ///     The value is the <c>undefined</c> value.
        /// </summary>
        JsUndefined = 0,
        /// <summary>
        ///     The value is the <c>null</c> value.
        /// </summary>
        JsNull = 1,
        /// <summary>
        ///     The value is a JavaScript number value.
        /// </summary>
        JsNumber = 2,
        /// <summary>
        ///     The value is a JavaScript string value.
        /// </summary>
        JsString = 3,
        /// <summary>
        ///     The value is a JavaScript Boolean value.
        /// </summary>
        JsBoolean = 4,
        /// <summary>
        ///     The value is a JavaScript object value.
        /// </summary>
        JsObject = 5,
        /// <summary>
        ///     The value is a JavaScript function object value.
        /// </summary>
        JsFunction = 6,
        /// <summary>
        ///     The value is a JavaScript error object value.
        /// </summary>
        JsError = 7,
        /// <summary>
        ///     The value is a JavaScript array object value.
        /// </summary>
        JsArray = 8,
        /// <summary>
        ///     The value is a JavaScript symbol value.
        /// </summary>
        JsSymbol = 9,
        /// <summary>
        ///     The value is a JavaScript ArrayBuffer object value.
        /// </summary>
        JsArrayBuffer = 10,
        /// <summary>
        ///     The value is a JavaScript typed array object value.
        /// </summary>
        JsTypedArray = 11,
        /// <summary>
        ///     The value is a JavaScript DataView object value.
        /// </summary>
        JsDataView = 12,
    }
}
