using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ScriptKit
{
    public delegate JsValue JsFunctionCallback(JsFunction calle, JsValue self, ReadOnlyCollection<JsValue> arguments);
}
