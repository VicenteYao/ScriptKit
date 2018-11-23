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
            context.AddRef();
          
            runtime.CurrentContext.Global["print"] = new JsFunction((calle,self, arguments) =>
            {
                Console.WriteLine(string.Join(string.Empty, arguments));
                JsArrayBuffer jsArrayBuffer = new JsArrayBuffer(null);
                return null;
            });
            runtime.CurrentContext.Global["Worker"] = new JsFunction((calle, self, arguments) =>
            {

                return self;
            });
            runtime.CurrentContext.Global.CreateWeakReference().ToString();
            runtime.CurrentContext.Run("print(123)");
            runtime.CurrentContext.Run("let newWorker= Worker.bind(this); let b= newWorker(123);print(b)");

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
