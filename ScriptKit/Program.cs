using System;
using System.Linq;

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
                Console.WriteLine(string.Join(string.Empty, arg2));
                return null;
            });
            runtime.CurrentContext.Global["Worker"] = new JsConstructor((calle, arguments) =>
            {
                JsObject self = arguments[0];
                self["test"] = 123;
                return null;
            });
            runtime.CurrentContext.Run("log(123)");
            runtime.CurrentContext.Run("new Worker(123)");
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
