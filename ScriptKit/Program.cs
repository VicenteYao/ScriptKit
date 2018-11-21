using System;

namespace ScriptKit
{
    class Program
    {
        static void Main(string[] args)
        {
            JsRuntime runtime = JsRuntime.CreateRuntime();
            var context= runtime.CreateContext();
            runtime.CurrentContext = context;
            runtime.CurrentContext.Global["log"] = new JsFunction((arg1, arg2) =>
            {
                Console.WriteLine(arg2);
                return null;
            });
            runtime.CurrentContext.Run("log(123)");
            Console.WriteLine("Hello World!");
        }
    }
}
