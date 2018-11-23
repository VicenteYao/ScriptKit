using System;
namespace ScriptKit
{
    public class JsScript
    {
        public JsScript()
        {
        }

        public uint ScriptId { get; private set; }
        public string FileName { get; private set; }

        public uint LineCount { get; private set; }

        public uint SourceLength { get; private set; }

        public uint ParentScriptId { get; private set; }

        public string ScriptType { get; private set; }

        public string Source { get; private set; }
    }
}
