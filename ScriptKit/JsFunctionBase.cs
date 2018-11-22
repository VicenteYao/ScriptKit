using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ScriptKit
{
    public abstract class JsFunctionBase:JsObject
    {
        protected JsFunctionBase()
        {

        }

        protected static unsafe IntPtr JsNativeFunction(IntPtr calle, bool isConstructCall, IntPtr arguments, ushort argumentCount, IntPtr callbackState)
        {
            GCHandle funcGCHandle = GCHandle.FromIntPtr(callbackState);
            if (funcGCHandle.Target is Func<JsObject, ReadOnlyCollection<JsObject>, JsObject> func)
            {
                JsObject objCalle = JsObject.FromIntPtr(calle);
                Span<IntPtr> argumentSpan = new Span<IntPtr>((void*)arguments, argumentCount);
                ReadOnlyCollection<JsObject> args =
                 new ReadOnlyCollection<JsObject>(argumentSpan.ToArray().Select(p => JsObject.FromIntPtr(p)).ToArray());
                JsObject result = func(objCalle, args);
                if (result == null)
                {
                    return objCalle.Context.Null.Value;
                }
                return result.Value;
            }
            return IntPtr.Zero;
        }

        public abstract JsObject Invoke(params JsObject[] arguments);


    }
}
