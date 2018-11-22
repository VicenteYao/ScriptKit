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
            runtime.CurrentContext.Global["print"] = new JsFunction((arg1, arg2) =>
            {
                Console.WriteLine(string.Join(string.Empty, arg2.Skip(1)));
                return null;
            });
            runtime.CurrentContext.Global["Worker"] = new JsFunction((calle, arguments) =>
            {
                JsObject self = arguments[0] as JsObject;
                self[3] = new JsNumber(234);
                return self;
            });
            runtime.CurrentContext.Run("print(123)");
            runtime.CurrentContext.Run("let i= new Worker(123);print(i)");
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
