using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptKit
{
    public class PropertyDescriptor
    {


        public bool configurable { get; private set; }

        public bool enumerable { get; private set; }

        public JsObject value { get; private set; }

        public bool writeable { get; private set; }

        public JsFunction get { get; private set; }

        public JsFunction set { get; private set; }
    }
}
