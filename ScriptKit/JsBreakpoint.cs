using System;
namespace ScriptKit
{
    public class JsBreakpoint
    {
        public JsBreakpoint(uint scriptId,uint line,uint column)
        {
            this.ScriptId = scriptId;
            this.Line = line;
            this.Column = column;
        }

        public uint ScriptId { get; private set; }

        public uint Column { get; private set; }

        public uint Line { get; private set; }

        public uint BreakPointId { get; internal set; }


    }
}
