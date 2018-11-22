using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptKit
{
    public enum JsTypedArrayType
    {
        /// <summary>
        ///     An int8 array.
        /// </summary>
        JsArrayTypeInt8,
        /// <summary>
        ///     An uint8 array.
        /// </summary>
        JsArrayTypeUint8,
        /// <summary>
        ///     An uint8 clamped array.
        /// </summary>
        JsArrayTypeUint8Clamped,
        /// <summary>
        ///     An int16 array.
        /// </summary>
        JsArrayTypeInt16,
        /// <summary>
        ///     An uint16 array.
        /// </summary>
        JsArrayTypeUint16,
        /// <summary>
        ///     An int32 array.
        /// </summary>
        JsArrayTypeInt32,
        /// <summary>
        ///     An uint32 array.
        /// </summary>
        JsArrayTypeUint32,
        /// <summary>
        ///     A float32 array.
        /// </summary>
        JsArrayTypeFloat32,
        /// <summary>
        ///     A float64 array.
        /// </summary>
        JsArrayTypeFloat64
    }
}
