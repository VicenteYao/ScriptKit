using System;
using System.Collections.ObjectModel;

namespace ScriptKit
{
    public class JsConstructor:JsFunction
    {
        public JsConstructor(Func<JsObject, ReadOnlyCollection<JsObject>, JsObject> func) : base(func)
        {

        }

        public override JsObject Invoke(params JsObject[] arguments)
        {
            IntPtr result = IntPtr.Zero;
            NativeMethods.JsConstructObject(this.ValueRef, IntPtr.Zero, (ushort)arguments.Length, out result);
            return null;
        }

    }
}
