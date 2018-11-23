using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace ScriptKit
{
    public class JsString:JsObject
    {
        static JsString(){
            emptyStringCharArray=new char[]{'\0'};
        }
        private static char[] emptyStringCharArray;
        public unsafe JsString(string str):base(IntPtr.Zero)
        {
            char[] charArray=str==string.Empty?emptyStringCharArray:str.ToCharArray();
            IntPtr stringValue = IntPtr.Zero;
            fixed (char* pString = charArray)
            {
                JsErrorCode jsErrorCode = NativeMethods.JsCreateStringUtf16(new IntPtr(pString), new IntPtr(charArray.Length), out stringValue);
                JsRuntimeException.VerifyErrorCode(jsErrorCode);
            }
            this.Value = stringValue;
        }

        internal JsString(IntPtr stringValue) : base(stringValue)
        {

        }

        public int Length
        {
            get
            {
                int length = 0;
                NativeMethods.JsGetStringLength(this.Value, out length);
                return length;
            }
        }

        public unsafe override string ToString()
        {
            IntPtr pStr = IntPtr.Zero;
            IntPtr pWritten = IntPtr.Zero;
            char* buffer = stackalloc char[this.Length];
            JsErrorCode jsErrorCode = NativeMethods.JsCopyStringUtf16(this.Value, 0, this.Length, new IntPtr(buffer), out pWritten);
            JsRuntimeException.VerifyErrorCode(jsErrorCode);
            return new string(buffer, 0, pWritten.ToInt32());
        }

    }
}
